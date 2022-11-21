using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SafeZone : MonoBehaviour
{
    [SerializeField] private Silhouette silhouettePrefab;
    private CameraSettings cameraSettings;
    private Silhouette silhouetteInstance;
    [SerializeField] private float distance = 5;


    void Start()
    {
        cameraSettings = GameManager.Instance.GetCameraSettings();
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (!other.attachedRigidbody.GetComponent<Player>())
        {
            return;
        }
        if (!silhouetteInstance)
        {
            Player player = GameObject.FindObjectOfType<Player>();
            cameraSettings.FollowAndTargetPlayer();
            silhouetteInstance = Instantiate<Silhouette>(silhouettePrefab, GetRandomPosition(player.transform.position), Quaternion.identity);
            silhouetteInstance.twisted = true;
        }
    }
    public Vector2 GetRandomPosition(Vector2 origin)
    {
        Vector2 randomDirection = Random.insideUnitSphere * distance;
        randomDirection += origin;
        NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, distance, LayerMask.NameToLayer("UnsafeZone"));
        return hit.position;
    }
}
