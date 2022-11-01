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
            otherRb.transform.position = SmoothTransition(otherRb);
        }
        else
        {
            PortalHandler.Instance.CanTeleport = true;
        }
    }

    private Vector2 SmoothTransition(Rigidbody2D otherRb)
    {

        Vector3 oldPos = otherRb.transform.position;
        Vector3 oldCameraPos = otherRb.GetComponentInChildren<CinemachineVirtualCamera>().transform.position;

        Vector3 pos = (this.transform.position - oldPos);
        pos.y = -pos.y;

        Vector3 newPos = pos + exitPoint.transform.position;

        SmoothFollowTarget(otherRb.GetComponentInChildren<CinemachineVirtualCamera>(), oldCameraPos, newPos, oldPos);
        return newPos;
    }

    private void SmoothFollowTarget(CinemachineVirtualCamera otherCinemachine, Vector3 oldCameraPos, Vector3 newPos, Vector3 oldPos)
    {
        CinemachineFramingTransposer cmFramingTransposer = otherCinemachine.GetCinemachineComponent<CinemachineFramingTransposer>();
        Vector3 pos = cmFramingTransposer.transform.position + (oldPos - oldCameraPos);
        pos.z = -10;
        cmFramingTransposer.ForceCameraPosition(pos, Quaternion.identity);
    }

}
