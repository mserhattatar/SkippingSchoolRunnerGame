using System.Collections;
using UnityEngine;

namespace StudentScript
{
    public class StudentMovementController : JoystickManager
    {
        private Transform _playerT;
        [SerializeField] private float _forwardSpeed, _movementSpeed;

        private bool _controlBlocked;
        private readonly int _laneNum = 1;
        private int targetX = -10;


        private void Start()
        {
            _playerT = transform;
        }

        private void FixedUpdate()
        {
            //StudentMovement();
            StudentMovement2();
        }

        private void StudentMovement()
        {
            var pPos = _playerT.position;


            var targetZ = pPos.z + _forwardSpeed;
            var targetX = pPos.x + JoystickHorizontal * _movementSpeed;

            var direction = new Vector3(x: targetX, pPos.y, targetZ);

            transform.position = Vector3.MoveTowards(pPos, direction, 15f * Time.deltaTime);
        }

        private void StudentMovement2()
        {
            var pPos = _playerT.position;

            if (!_controlBlocked)
            {
                if (JoystickHorizontal < 0 && (pPos.x > -11))
                {
                    _controlBlocked = true;
                    targetX += -1;
                    StartCoroutine(StopSlide());
                }

                if (JoystickHorizontal > 0 && (pPos.x < -9))
                {
                    _controlBlocked = true;
                    targetX += 1;
                    StartCoroutine(StopSlide());
                }
            }

            var targetZ = pPos.z + _forwardSpeed;
            //targetX = pPos.x + JoystickHorizontal * _movementSpeed;

            var direction = new Vector3(x: targetX, pPos.y, targetZ);

            transform.position = Vector3.MoveTowards(pPos, direction, 15f * Time.deltaTime);
        }

        private IEnumerator StopSlide()
        {
            yield return new WaitForSeconds(.5f);
            _controlBlocked = false;
        }
    }
}