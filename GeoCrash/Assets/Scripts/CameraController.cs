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

    private Quaternion initialCameraRotation; // 記錄攝影機的初始旋轉
    private Quaternion targetTiltRotation;    // 目標傾斜旋轉
    private float tiltRotationSpeed;          // 傾斜旋轉速度
    private bool isTilting = false;           // 標記是否正在傾斜
    private bool shouldRebounce = false;      // 標記是否需要回彈

    void Awake()
    {
        idealScale = cinemachineVirtualCamera.m_Lens.OrthographicSize; //5
        cinemachinePerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachinePerlin.m_AmplitudeGain = 0f; // 將強度設為 0
    }

    // Update is called once per frame
    void Update()
    {
        if (shakeTimer > 0) // 震動邏輯
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

        if(isZooming == true){ // 縮放邏輯
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
        

        if (isTilting) // 傾斜效果
        {
            cinemachineVirtualCamera.transform.rotation = Quaternion.Slerp(
                cinemachineVirtualCamera.transform.rotation,
                targetTiltRotation,
                tiltRotationSpeed * Time.deltaTime
            );

            // 如果已經接近目標角度，停止傾斜
            if (Quaternion.Angle(cinemachineVirtualCamera.transform.rotation, targetTiltRotation) < 0.05f) // 使用一個很小的容差
            {
                cinemachineVirtualCamera.transform.rotation = targetTiltRotation; // 精確設置為目標角度
                isTilting = false;

                // 如果需要回彈，則設定新的目標為初始角度，並再次開始傾斜
                if (shouldRebounce)
                {
                    targetTiltRotation = initialCameraRotation; // 回彈目標是初始角度
                    isTilting = true; // 重新開始傾斜 (回彈)
                    shouldRebounce = false; // 這次傾斜完成後不再回彈
                }
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

    public void TiltCamera(float tiltAngle, float tiltSpeed, float isRebounce){
        targetTiltRotation = Quaternion.Euler(
            cinemachineVirtualCamera.transform.eulerAngles.x,
            cinemachineVirtualCamera.transform.eulerAngles.y,
            tiltAngle
        );
        tiltRotationSpeed = tiltSpeed;
        isTilting = true;
        shouldRebounce = (isRebounce == 1.0f); // 根據參數設定是否回彈
        initialCameraRotation = cinemachineVirtualCamera.transform.rotation;
    }
}
