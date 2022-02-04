using System.Collections;
using UnityEngine;

public class StudentMovementController : JoystickManager
{
    private ComponentContainer _myComponent;

    private Transform _playerT;

    private bool _controlBlocked;
    private const int LaneNum = 2;
    private const float TargetPosY = 0.245f;
    private const float ForwardSpeed = 2.4f;
    private int _targetX = 0;

    public void Initialize(ComponentContainer componentContainer)
    {
        _myComponent = componentContainer;
    }

    private void OnEnable()
    {
        //CanvasManager.TimerDelegate += StartPlayerMovement;
        //GameManager.ResetLevelDelegate += ResetMovement;
        //StudentCollisionController.TeacherCollisionDelegate += StopStudentMovement;
    }

    private void Start()
    {
        _playerT = transform;
    }

    private void FixedUpdate()
    {
        StudentMovement();
    }

    private void StudentMovement()
    {
        var pPos = _playerT.position;

        if (!_controlBlocked)
        {
            //left
            if (JoystickHorizontal < 0 && (pPos.x > -LaneNum))
            {
                StartCoroutine(StopSlide());
                _targetX += -LaneNum;
            }

            else if (JoystickHorizontal > 0 && (pPos.x < LaneNum))
            {
                StartCoroutine(StopSlide());
                _targetX += LaneNum;
            }
        }

        var targetZ = pPos.z + ForwardSpeed;
        var direction = new Vector3(x: _targetX, TargetPosY, targetZ);
        transform.position = Vector3.MoveTowards(pPos, direction, 15f * Time.deltaTime);
    }

    private IEnumerator StopSlide()
    {
        _controlBlocked = true;
        yield return new WaitForSeconds(.2f);
        _controlBlocked = false;
    }
}