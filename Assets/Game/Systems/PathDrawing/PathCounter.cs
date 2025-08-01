using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PathCounter : MonoBehaviour
{
    TMP_Text tmpComponent;
    MeshRenderer meshRenderer;

    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask fogLayer;

    void Awake()
    {
        tmpComponent = GetComponent<TMP_Text>();
        meshRenderer = GetComponent<MeshRenderer>();

        SetSortLayerToFog();
    }
    void OnEnable()
    {
        Events.Level.StartLoop += SetSortLayerToGround;

        Events.Level.ReachedNextTile += DestroySelf;

        Events.Level.LoopComplete += SetSortLayerToFog;
    }
    void OnDisable()
    {
        Events.Level.StartLoop -= SetSortLayerToGround;

        Events.Level.ReachedNextTile -= DestroySelf;

        Events.Level.LoopComplete -= SetSortLayerToFog;
    }
    // Update is called once per frame
    public void UpdateText(string _text)
    {
        tmpComponent.text = _text;
    }

    void SetSortLayerToGround()
    {
        meshRenderer.sortingLayerName = "Ground";
    }
    void SetSortLayerToFog()
    {
        meshRenderer.sortingLayerName = "Fog";
    }

    void DestroySelf(Vector3Int _playerTilePos)
    {
        Vector3Int m_tilePos = LevelManager.instance.GetWorldTilemap().WorldToCell(transform.position);
        float d = Vector3Int.Distance(m_tilePos, _playerTilePos);
        if (d < 1)
        {
            Destroy(gameObject);
        }
    }
}
