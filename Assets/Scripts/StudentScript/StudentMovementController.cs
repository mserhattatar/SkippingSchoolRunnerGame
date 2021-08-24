using System;
using UnityEngine;

namespace StudentScript
{
    public class StudentMovementController : JoystickManager
    {
        public delegate void StudentMovementDelegate();

        public static StudentMovementDelegate StudentJumpDelegate;
        [HideInInspector] public bool stopPlayerMovement;

        private Transform _playerT;
        private float _forwardSpeed, _movementSpeed, _targetY, _timeCount;
        private bool _isJumping, _isPlayerRun;
        
        private void OnEnable()
        {
            CanvasManager.TimerDelegate += StartPlayerMovement;
            GameManager.ResetLevelDelegate += ResetMovement;
            StudentCollisionController.TeacherCollisionDelegate += StopStudentMovement;
        }

        private void Start()
        {
            _playerT = transform;
            _forwardSpeed = 2.4f;
            _movementSpeed = 1.7f;
            _targetY = 0.24f;
            _timeCount = 0.0f;
        }

        private void FixedUpdate()
        {
            if (_isPlayerRun) StudentMovement();
        }

        private void StudentMovement()
        {
            var pPos = _playerT.position;

            if (stopPlayerMovement)
            {
                if ((transform.position.y < 0.3f)) return;
                pPos = new Vector3(pPos.x, 0.24f, pPos.z);
                _playerT.position = pPos;
                return;
            }

            StudentMovementRotatian(_playerT);
            StudentJump(pPos.y);

            var targetZ = pPos.z + _forwardSpeed;
            var targetX = pPos.x + JoystickHorizontal * _movementSpeed;
            if (targetX <= -4.5f)
                targetX = -4.5f;
            else if (targetX >= 4.5f)
                targetX = 4.5f;
            var direction = new Vector3(x: targetX, _targetY, targetZ);

            transform.position = Vector3.MoveTowards(pPos, direction, 15f * Time.deltaTime);
        }

        private void StudentJump(float pPosY)
        {
            switch (_isJumping)
            {
                case false when JoystickVertical > 0.5f:
                    _targetY = 6f;
                    _isJumping = true;
                    StudentJumpDelegate();
                    break;
                case true when pPosY > 4f :
                    _targetY = 0.24f;
                    break;
                case true when pPosY < 0.3f:
                    _isJumping = false;
                    break;
            }
        }

        private void StudentMovementRotatian(Transform playerT)
        {
            var direction = playerT.position + Vector3.right * (JoystickHorizontal * 10f * _timeCount);
            var lookRotation = Quaternion.LookRotation(direction);
            playerT.rotation = Quaternion.Slerp(playerT.rotation, lookRotation, _timeCount);
            _timeCount += Time.deltaTime;
        }

        private void StartPlayerMovement()
        {
            _isPlayerRun = true;
        }

        private void StopStudentMovement()
        {
            stopPlayerMovement = true;
        }

        private void ResetMovement()
        {
            _isPlayerRun = false;
            stopPlayerMovement = false;
            _targetY = 0.24f;
            _timeCount = 0f;
            transform.position = new Vector3(0f, _targetY, 0f);
        }
    }
}