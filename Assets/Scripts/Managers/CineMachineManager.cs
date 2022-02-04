using Cinemachine;
using UnityEngine;

public class CineMachineManager : MonoBehaviour
{
    private ComponentContainer _myComponent;

    private float _shakeTimer;
    private bool _stopShake;
    private CinemachineBasicMultiChannelPerlin _cineMPerlin;

    public CinemachineVirtualCamera cineMachine1;
    public CinemachineVirtualCamera cineMachine2;

    public void Initialize(ComponentContainer componentContainer)
    {
        _myComponent = componentContainer;
    }

    private void OnEnable()
    {
        _shakeTimer = 0f;
        _cineMPerlin = cineMachine2.GetComponent<CinemachineVirtualCamera>()
            .GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
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