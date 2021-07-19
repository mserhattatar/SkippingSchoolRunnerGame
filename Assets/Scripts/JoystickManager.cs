using AssetStoreOriginals.Joystick_Pack.Scripts.Base;
using UnityEngine;

public class JoystickManager : MonoBehaviour
{
    private static Joystick _joystick;
    protected static float JoystickHorizontal;
    protected static float JoystickVertical;

    private void Awake()
    {
        _joystick = FindObjectOfType<Joystick>();
    }

    private void FixedUpdate()
    {
        JoystickHorizontal = _joystick.Horizontal;
        JoystickVertical = _joystick.Vertical;
    }

    protected static bool CheckJoystickHorizontal()
    {
        return (JoystickHorizontal > 0.3 || JoystickHorizontal < -0.3);
    }
}