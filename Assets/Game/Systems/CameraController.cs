using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject worldCam;
    CinemachineVirtualCamera worldVirtualCam;
    [SerializeField] float zoomSpeed;
    [SerializeField] GameObject playerFollowCam;

    void OnEnable()
    {
        worldVirtualCam = worldCam.GetComponent<CinemachineVirtualCamera>();
        Events.Level.StartLoop += ShiftToPlayer;
        Events.Level.LoopComplete += ShiftToWorld;
    }
    void OnDisable()
    {
        Events.Level.StartLoop -= ShiftToPlayer;
        Events.Level.LoopComplete -= ShiftToWorld;
    }
    void Update()
    {
        worldVirtualCam.m_Lens.OrthographicSize = Mathf.Clamp(worldVirtualCam.m_Lens.OrthographicSize, 2, 20);
        //zoom in world cam using mouse wheel
        if (worldCam.activeSelf)
            worldVirtualCam.m_Lens.OrthographicSize -= Input.mouseScrollDelta.y * zoomSpeed * Time.deltaTime;
    }

    void ShiftToPlayer()
    {
        playerFollowCam.SetActive(true);
    }
    void ShiftToWorld()
    {
        playerFollowCam.SetActive(false);
    }
}
