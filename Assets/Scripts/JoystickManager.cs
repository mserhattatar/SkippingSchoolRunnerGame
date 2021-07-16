using UnityEngine;
public class JoystickManager : MonoBehaviour
{
    private static Joystick _joystick;
    protected static float joystickHorizontal;
    protected static float joystickVertical;

    private void Awake()
    {
        _joystick = FindObjectOfType<Joystick>();
    }
    private void FixedUpdate()
    {
        joystickHorizontal = _joystick.Horizontal;
        joystickVertical = _joystick.Vertical;
    }

    protected static bool CheckJoystickHorizontal()
    {
        return (joystickHorizontal > 0.3 || joystickHorizontal < -0.3);
    }
}
