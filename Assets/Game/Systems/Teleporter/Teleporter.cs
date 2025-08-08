using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] Transform _player;
    [SerializeField] Transform worldCamFollow;
    [SerializeField] Transform portalPrefab;
    List<Transform> portalInstances = new();
    [SerializeField] GameObject teleportVisual;
    [SerializeField] int charges = 0;
    int maxCharges = 3;

    public bool teleporterActive = false;
    bool hasBeenTeleported = false;

    [Header("SFX")]
    [SerializeField] AudioSource sfxSource;
    [SerializeField] AudioClip launchSFX;
    [SerializeField] AudioClip activateSFX;
    [SerializeField] AudioClip deactivateSFX;

    private void OnEnable()
    {
        Events.Health.GainCharge += AddCharge;

        Events.Level.LoopComplete += ReturnPlayerHome;
    }
    private void OnDisable()
    {
        Events.Health.GainCharge -= AddCharge;

        Events.Level.LoopComplete -= ReturnPlayerHome;
    }
    void Start()
    {
        Events.Health.ChargesChanged?.Invoke(charges);
    }
    private void Update()
    {
        if (!teleporterActive)
            return;

        if (Input.GetMouseButtonDown(1))
        {
            TeleportPlayer();
        }
    }


    void AddCharge(int i)
    {
        charges += i;
        if (charges >= maxCharges)
            charges = maxCharges;

        Events.Health.ChargesChanged?.Invoke(charges);
    }

    public void ActivateTeleportMode()
    {
        if (charges == maxCharges)
        {
            sfxSource.PlayOneShot(activateSFX);
            Events.Level.TeleportMode?.Invoke();
            teleporterActive = true;
            teleportVisual.SetActive(true);
        }
        else
        {
            SFXManager.instance.CancelPathSFX();
            Events.Level.NotEnoughCharges?.Invoke();
        }
    }
    public void DeactivateTeleportMode()
    {
        sfxSource.PlayOneShot(deactivateSFX);
        Events.Level.EndTeleportMode?.Invoke();
        teleporterActive = false;
        teleportVisual.SetActive(false);
    }

    void TeleportPlayer()
    {
        Transform portalInstance = Instantiate(portalPrefab, _player.position, Quaternion.identity);
        portalInstances.Add(portalInstance);

        sfxSource.PlayOneShot(launchSFX);

        portalInstance = Instantiate(portalPrefab, transform.position, Quaternion.identity);
        portalInstances.Add(portalInstance);
        
        _player.position = transform.position;
        worldCamFollow.position = transform.position;

        Events.Level.InitiateTeleport?.Invoke();

        charges = 0;
        Events.Health.ChargesChanged?.Invoke(charges);

        teleportVisual.SetActive(false);
        teleporterActive = false;
        hasBeenTeleported = true;
    }

    void ReturnPlayerHome()
    {
        if (hasBeenTeleported)
        {
            foreach (Transform portalInstance in portalInstances)
            {
                Destroy(portalInstance.gameObject);
            }
            _player.position = new Vector2(8, 8);
            Events.Level.InitiateTeleport?.Invoke();
            hasBeenTeleported = false;
        }
    }
}
