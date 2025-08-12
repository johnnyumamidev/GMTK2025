using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Radar : MonoBehaviour
{
    [SerializeField] Transform currentTarget;
    [SerializeField] List<MissingPart> parts = new();
    float initialDistanceToTarget;
    [SerializeField] AudioSource sfxSource;
    [SerializeField] Animator _animator;
    [SerializeField] float animationSpeed, minAnimSpeed, maxAnimSpeed;

    private void OnEnable()
    {
        Events.Level.PartSpawned += AddSpawnedPart;
        Events.Level.CollectedMissingPart += RemovePartFromList;

        Events.Level.LevelGenerated += SetCurrentTarget;
    }
    private void OnDisable()
    {
        Events.Level.PartSpawned -= AddSpawnedPart;
        Events.Level.CollectedMissingPart -= RemovePartFromList;

        Events.Level.LevelGenerated -= SetCurrentTarget;
    }
    

    void Update()
    {
        if (currentTarget != null)
        {
            Vector2 dirToTarget = currentTarget.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(transform.forward, dirToTarget);
            transform.rotation = targetRotation;
        }


        //calculate animation speed based on distance to currently assigned missing part
        float currentDistance = Vector2.Distance(transform.position, currentTarget.position);
        animationSpeed = (initialDistanceToTarget - currentDistance) / initialDistanceToTarget * 2;
        animationSpeed = Mathf.Clamp(animationSpeed, minAnimSpeed, maxAnimSpeed);

        sfxSource.pitch = animationSpeed;
        _animator.speed = animationSpeed;
    }

    void AddSpawnedPart(MissingPart _part)
    {
        parts.Add(_part);
    }
    void SetCurrentTarget()
    {
        Dictionary<Transform, float> _partsByDistance = new();
        //get transforms from parts
        foreach (MissingPart part in parts)
        {
            Transform t = part.transform;
            float distance = Vector2.Distance(transform.position, t.position);
            _partsByDistance.Add(t, distance);
        }

        //determine which part is closest to the player by calculating their distance
        if (_partsByDistance.Count > 0)
        {
            float lowestDistance = _partsByDistance.Values.Min();
            Transform closestPart = _partsByDistance.FirstOrDefault(x => x.Value == lowestDistance).Key;

            currentTarget = closestPart;

            initialDistanceToTarget = lowestDistance;
        }

        //set that part to the assigned target

        //store distance to target
    }
    void RemovePartFromList(MissingPart _part)
    {
        parts.Remove(_part);
        SetCurrentTarget();
    }
}
