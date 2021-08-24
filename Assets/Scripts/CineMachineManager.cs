using Cinemachine;
using StudentScript;
using UnityEngine;

public class CineMachineManager : MonoBehaviour
{
    private float _shakeTimer;
    private bool _stopShake;
    private CinemachineBasicMultiChannelPerlin _cineMPerlin;
    private Vector3 _cM1Position, _cM1Rotation, _cM2Position, _cM2Rotation;

    public CinemachineVirtualCamera cineMachine1;
    public CinemachineVirtualCamera cineMachine2;

    private void OnEnable()
    {
        _shakeTimer = 0f;
        _cineMPerlin = cineMachine2.GetComponent<CinemachineVirtualCamera>()
            .GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        CanvasManager.SwitchCameraDelegate += SwitchCamera1To2;
        StudentCollisionController.ObstacleCollisionDelegate += ShakeCamera;
        StudentCollisionController.TeacherCollisionDelegate += SwitchCamera2To1;
    }

    private void LateUpdate()
    {
        if (!_stopShake) return;
        ResetCameraShake();
    }

    private void ShakeCamera()
    {
        _cineMPerlin.m_AmplitudeGain = 1.3f;
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
        cineMachine1.Priority = 1;
        cineMachine2.Priority = 2;
    }

    private void SwitchCamera2To1()
    {
        cineMachine2.Priority = 1;
        cineMachine1.Priority = 2;
    }
}