using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSettings : MonoBehaviour
{
    private CinemachineVirtualCamera mainSettingCMVC;
    public CinemachineVirtualCamera MainSettingCMVC { get { return mainSettingCMVC; } }
    [SerializeField] private CinemachineShake cinemachineShake;

    private void Start()
    {
        mainSettingCMVC = this.transform.Find("MainSetting").GetComponent<CinemachineVirtualCamera>();
    }

    public void SetTarget(Transform target)
    {
        mainSettingCMVC.LookAt = target;
    }
    public void FollowTarget(Transform target)
    {
        mainSettingCMVC.Follow = target;
    }
    public void FollowAndTargetPlayer()
    {
        Player player = Transform.FindObjectOfType<Player>();
        SetTarget(player.transform);
        FollowTarget(player.transform);
    }
    public void ShakeCamera(float intensity, float time)
    {
        cinemachineShake.ShakeCamera(intensity, time);
    }
}
