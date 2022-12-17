using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;

[RequireComponent(typeof(BoxCollider2D))]
public class CameraTransition : MonoBehaviour
{
    [SerializeField] private float orthoSize = 5;
    [SerializeField] private Transform lookAt;
    [SerializeField] private UnityEvent afterEnter;
    [SerializeField] private UnityEvent afterExit;
    private CinemachineVirtualCamera cVirtualCamera;

    private void Start()
    {
        cVirtualCamera = GameObject.FindWithTag("CameraSettings").GetComponent<CinemachineVirtualCamera>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (Collider2DValidation.IsPlayer(other))
        {
            cVirtualCamera.m_Lens.OrthographicSize = orthoSize;
            cVirtualCamera.Follow = lookAt;
            cVirtualCamera.LookAt = lookAt;
            afterEnter?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (Collider2DValidation.IsPlayer(other))
        {
            afterExit?.Invoke();
        }
    }
}