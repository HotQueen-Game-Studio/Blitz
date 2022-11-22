using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSettings : MonoBehaviour
{
    private CinemachineVirtualCamera mainSettingCMVC;
    public CinemachineVirtualCamera MainSettingCMVC { get { return mainSettingCMVC; } }
    [SerializeField] private CinemachineShake cinemachineShake;
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject credits;

    private void Start()
    {
        mainSettingCMVC = this.transform.Find("MainSetting").GetComponent<CinemachineVirtualCamera>();
    }

    public void LookAt(Transform target)
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
        LookAt(player.transform.Find("Sight/Offset"));
        FollowTarget(player.transform.Find("Sight/Offset"));
    }

    public void FollowAndTargetCredits()
    {
        credits.SetActive(true);
        LookAt(credits.transform);
        FollowTarget(credits.transform);
    }

    public void FollowAndTargetMenu()
    {
        menu.SetActive(true);
        LookAt(menu.transform);
        FollowTarget(menu.transform);
    }

    public void ShakeCamera(float intensity, float time)
    {
        cinemachineShake.ShakeCamera(intensity, time);
    }
}
