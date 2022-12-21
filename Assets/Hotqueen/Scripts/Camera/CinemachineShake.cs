using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CinemachineShake : MonoBehaviour
{
    [SerializeField] CameraSettings cameraSettings;
    CinemachineBasicMultiChannelPerlin cinemachineComponent;
    private float shakeTimer = 0;

    public void ShakeCamera(float intensity, float time)
    {
        cinemachineComponent = cameraSettings.MainSettingCMVC.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineComponent.m_AmplitudeGain = intensity;
        shakeTimer = time;
    }

    // Update is called once per frame
    void Update()
    {
        if (shakeTimer > 0 && cinemachineComponent)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0)
            {
                cinemachineComponent.m_AmplitudeGain = 0;
            }
        }
    }
}
