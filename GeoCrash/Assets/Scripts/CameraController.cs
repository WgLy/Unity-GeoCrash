using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera cinemachineVirtualCamera;
    private CinemachineBasicMultiChannelPerlin cinemachinePerlin;

    private float shakeTimer;
    private float shakeTimerTotal;
    private float startingIntensity;

    private float idealScale; // 目標縮放大小
    private float zoomSpeed;   // 縮放速度
    private bool isZooming = false; // 標記是否正在進行縮放

    void Awake()
    {
        idealScale = cinemachineVirtualCamera.m_Lens.OrthographicSize; //5
        cinemachinePerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachinePerlin.m_AmplitudeGain = 0f; // 將強度設為 0
    }

    // Update is called once per frame
    void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0f)
            {
                // 震動結束
                cinemachinePerlin.m_AmplitudeGain = 0f; // 將強度設為 0
            }
            else
            {
                // 逐漸衰減震動強度
                cinemachinePerlin.m_AmplitudeGain = Mathf.Lerp(startingIntensity, 0f, 1 - (shakeTimer / shakeTimerTotal));
            }
        }

        if(isZooming == true){
            cinemachineVirtualCamera.m_Lens.OrthographicSize = Mathf.MoveTowards(
                cinemachineVirtualCamera.m_Lens.OrthographicSize,
                idealScale,
                zoomSpeed * Time.deltaTime
            );

            // 如果已經接近目標值，則停止縮放
            if (Mathf.Approximately(cinemachineVirtualCamera.m_Lens.OrthographicSize, idealScale))
            {
                isZooming = false;
            }
        }
        
    }

    public void ShakeCamera(float intensity, float time)
    {
        cinemachinePerlin.m_AmplitudeGain = intensity;
        startingIntensity = intensity;
        shakeTimerTotal = time;
        shakeTimer = time;
    }

    public void ChangeCameraScale(float scale, float speed)
    {
        idealScale = scale;
        zoomSpeed = speed;
        isZooming = true;
    }
}
