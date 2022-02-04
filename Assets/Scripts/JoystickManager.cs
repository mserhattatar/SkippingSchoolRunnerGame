using AssetStoreOriginals.Joystick_Pack.Scripts.Base;
using UnityEngine;

public class JoystickManager : MonoBehaviour
{
    private static Joystick _joystick;
    protected static float JoystickHorizontal;

    private void Awake()
    {
        _joystick = FindObjectOfType<Joystick>();
    }

    private void FixedUpdate()
    {
        JoystickHorizontal = _joystick.Horizontal;
    }
}