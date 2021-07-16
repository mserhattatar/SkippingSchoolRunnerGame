using System;
using Cinemachine;
using UnityEngine;

public class CineMachineManager : MonoBehaviour
{
    private float _shakeTimer;
    private bool _stopShake;
    private CinemachineBasicMultiChannelPerlin _cinemachineMultiChannelPerlin;
    
    public CinemachineVirtualCamera cinemachine1;
    public CinemachineVirtualCamera cinemachine2;
  
    private void Start()
    {
        _shakeTimer = 0f;
        _cinemachineMultiChannelPerlin = cinemachine2.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        CanvasController.timerDelegate += SwitchCamera1To2;
        StudentCollisionController.obstacleCollisionDelegate += ShakeCamera;
        StudentCollisionController.teacherCollisionDelegate += SwitchCamera2To1;
    }

    private void Update()
    {
        if(!_stopShake) return;
        ResetCameraShake();
    }

    private void ShakeCamera()
    {
        _cinemachineMultiChannelPerlin.m_AmplitudeGain = 1;
        _shakeTimer = 0.5f;
        _stopShake = true;
    }

    private void ResetCameraShake()
    { 
        if (_shakeTimer > 0f)
            _shakeTimer -= Time.deltaTime;
        else if (_shakeTimer < 0f)
        {
            _shakeTimer = 0f;
            _cinemachineMultiChannelPerlin.m_AmplitudeGain = 0f;
            _stopShake = false;
        }
    }

    private void SwitchCamera1To2()
    {
        cinemachine2.gameObject.SetActive(true);
        cinemachine1.Priority = 1;
        cinemachine2.Priority = 2;
        cinemachine1.gameObject.SetActive(false);
    }

    private void SwitchCamera2To1()
    {
        cinemachine1.gameObject.SetActive(true);
        cinemachine2.Priority = 1;
        cinemachine1.Priority = 2;
        cinemachine2.gameObject.SetActive(false);
    }
}
