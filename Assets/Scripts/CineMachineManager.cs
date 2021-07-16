using Cinemachine;
using UnityEngine;

public class CineMachineManager : MonoBehaviour
{
    private float _shakeTimer;
    private bool _stopShake;
    private CinemachineBasicMultiChannelPerlin _cineMPerlin;

    public CinemachineVirtualCamera cineMachine1;
    public CinemachineVirtualCamera cineMachine2;

    private void Start()
    {
        _shakeTimer = 0f;
        _cineMPerlin = cineMachine2.GetComponent<CinemachineVirtualCamera>()
            .GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        CanvasController.TimerDelegate += SwitchCamera1To2;
        StudentCollisionController.obstacleCollisionDelegate += ShakeCamera;
        StudentCollisionController.teacherCollisionDelegate += SwitchCamera2To1;
    }

    private void Update()
    {
        if (!_stopShake) return;
        ResetCameraShake();
    }

    private void ShakeCamera()
    {
        _cineMPerlin.m_AmplitudeGain = 1;
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
            _cineMPerlin.m_AmplitudeGain = 0f;
            _stopShake = false;
        }
    }

    private void SwitchCamera1To2()
    {
        cineMachine2.gameObject.SetActive(true);
        cineMachine1.Priority = 1;
        cineMachine2.Priority = 2;
        cineMachine1.gameObject.SetActive(false);
    }

    private void SwitchCamera2To1()
    {
        cineMachine1.gameObject.SetActive(true);
        cineMachine2.Priority = 1;
        cineMachine1.Priority = 2;
        cineMachine2.gameObject.SetActive(false);
    }
}