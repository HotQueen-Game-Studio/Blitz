using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Portal : MonoBehaviour
{
    [SerializeField] private Transform exitPoint;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Rigidbody2D otherRb = other.attachedRigidbody;
        Debug.Log("Collided");
        if (otherRb.transform.tag == "Player" && PortalHandler.Instance.CanTeleport)
        {
            Debug.Log("Entered the portal");
            PortalHandler.Instance.CanTeleport = false;
            DisableCinemachineSoftZone(otherRb.GetComponentInChildren<CinemachineVirtualCamera>());
            Vector3 newPos = new Vector3();

            // if (teleportX)
            // {
            //     newPos.x = exitPoint.transform.position.x;
            // }
            // if (teleportY)
            // {
            //     newPos.y = exitPoint.transform.position.y;
            // }

            otherRb.transform.position = newPos;
        }
        else
        {
            EnableCinemachineSoftZone(otherRb.GetComponentInChildren<CinemachineVirtualCamera>());
            PortalHandler.Instance.CanTeleport = true;
        }
    }

    private void DisableCinemachineSoftZone(CinemachineVirtualCamera otherCinemachine)
    {
        otherCinemachine.GetCinemachineComponent<CinemachineFramingTransposer>().m_SoftZoneHeight = 0;
        otherCinemachine.GetCinemachineComponent<CinemachineFramingTransposer>().m_SoftZoneWidth = 0;
    }
    private void EnableCinemachineSoftZone(CinemachineVirtualCamera otherCinemachine)
    {
        otherCinemachine.GetCinemachineComponent<CinemachineFramingTransposer>().m_SoftZoneHeight = 0.8f;
        otherCinemachine.GetCinemachineComponent<CinemachineFramingTransposer>().m_SoftZoneWidth = 0.8f;
    }
}
