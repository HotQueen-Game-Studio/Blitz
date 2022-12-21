using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerSpawning : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Silhouette silhouettePrefab;
    [SerializeField] private Silhouette silhouetteInstance;
    [SerializeField] private Vector2 offset;
    private Action OnInvisible;

    private void Start()
    {
        OnInvisible += SpawnKiller;
    }

    private void Update()
    {
        if (!silhouetteInstance && !IsVisible())
        {
            OnInvisible?.Invoke();
        }

    }
    private void SpawnKiller()
    {
        var cs = GameObject.FindWithTag("CameraSettings").GetComponent<Cinemachine.CinemachineVirtualCamera>();
        Player player = GameObject.FindWithTag("Player").GetComponent<Player>();
        cs.Follow = player.transform;
        cs.LookAt = player.transform;
        silhouetteInstance = Instantiate(silhouettePrefab, (Vector2)this.transform.position + offset, Quaternion.identity);
        silhouetteInstance.twisted = true;
    }
    public bool IsVisible()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        return GeometryUtility.TestPlanesAABB(planes, spriteRenderer.bounds);
    }
}
