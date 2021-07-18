using Cinemachine;
using UnityEngine;

public class CineMachineManager : MonoBehaviour
{
    private float _shakeTimer;
    private bool _stopShake;
    private CinemachineBasicMultiChannelPerlin _cineMPerlin;
    private Vector3 _cM1Position, _cM1Rotation, _cM2Position, _cM2Rotation;

    public CinemachineVirtualCamera cineMachine1;
    public CinemachineVirtualCamera cineMachine2;

    private void Start()
    {
        CameraTransform(false);
        _shakeTimer = 0f;
        _cineMPerlin = cineMachine2.GetComponent<CinemachineVirtualCamera>()
            .GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        CanvasController.TimerDelegate += SwitchCamera1To2;
        StudentCollisionController.ObstacleCollisionDelegate += ShakeCamera;
        StudentCollisionController.TeacherCollisionDelegate += SwitchCamera2To1;
    }

    private void Update()
    {
        if (!_stopShake) return;
        ResetCameraShake();
    }

    private void CameraTransform(bool get)
    {
        var cM1Transform = cineMachine1.transform;
        var cM2Transform = cineMachine2.transform;
        
        if (get)
        {
            cM1Transform.position = _cM1Position;
            cM1Transform.eulerAngles = _cM1Rotation;
            cM2Transform.position = _cM2Position;
            cM2Transform.eulerAngles = _cM2Rotation;
        }
        else
        {
            _cM1Position = cM1Transform.position;
            _cM1Rotation = cM1Transform.eulerAngles;
            _cM2Position = cM2Transform.position;
            _cM2Rotation = cM2Transform.eulerAngles;
        }
       
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
        CameraTransform(true);
    }

    private void SwitchCamera2To1()
    {
        cineMachine1.gameObject.SetActive(true);
        cineMachine2.Priority = 1;
        cineMachine1.Priority = 2;
        cineMachine2.gameObject.SetActive(false);
        CameraTransform(true);
    }
}