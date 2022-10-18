using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CinemachineShake : MonoBehaviour
{
    private CinemachineVirtualCamera cinemachine;
    private CinemachineBasicMultiChannelPerlin cinemachineBMCP = null;
    private float shakeTimer = 0;
    // Start is called before the first frame update
    private void Start()
    {
        cinemachine = this.gameObject.GetComponent<CinemachineVirtualCamera>();
    }

    public void ShakeCamera(float intensity, float time)
    {
        cinemachineBMCP = cinemachine.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineBMCP.m_AmplitudeGain = intensity;
        shakeTimer = time;
    }

    // Update is called once per frame
    void Update()
    {
        if (shakeTimer > 0 && cinemachineBMCP)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0)
            {
                cinemachineBMCP.m_AmplitudeGain = 0;
            }
        }
    }
}
