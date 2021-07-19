using System;
using UnityEngine;

namespace StudentScript
{
    
    public class StudentMovementController : JoystickManager
    {
        public delegate void StudentMovementDelegate();
        public static StudentMovementDelegate StudentJumpDelegate;
        [HideInInspector] public bool stopPlayerMovement;
        
        private readonly float _forwardSpeed = 2.4f;
        private readonly float _movementSpeed = 1.7f;
        private float _targetY = 0.24f;
        private bool _isJumping;
        private bool _isPlayerRun;
        
        private void OnEnable()
        {
            CanvasManager.TimerDelegate += StartPlayerMovement;
            GameManager.ResetLevelDelegate += ResetMovement;
            StudentCollisionController.TeacherCollisionDelegate += StopStudentMovement;
        }

        private void FixedUpdate()
        {
            if (_isPlayerRun) StudentMovement();
        }

        private void LateUpdate()
        {
            if (!_isPlayerRun) return;
            StudentMovementRotatian();
            StudentJump();
        }

        private void StudentMovement()
        {
            var playerT = transform;
            var pPos = playerT.position;

            if (stopPlayerMovement)
            {
                if ((transform.position.y < 0.3f)) return;
                pPos = new Vector3(pPos.x, 0.24f, pPos.z);
                playerT.position = pPos;
                return;
            }

            var targetX = pPos.x + JoystickHorizontal * _movementSpeed;
            if (targetX <= -4.5f)
                targetX = -4.5f;
            else if (targetX >= 4.5f)
                targetX = 4.5f;
            var targetZ = pPos.z + _forwardSpeed;
            var direction = new Vector3(x: targetX, _targetY, targetZ);
            transform.position = Vector3.MoveTowards(pPos, direction, 15f * Time.deltaTime);
        }

        private void StudentJump()
        {
            var pPosY = transform.position.y;
            switch (_isJumping)
            {
                case false when JoystickVertical > 0.5f:
                    _targetY = 5f;
                    _isJumping = true;
                    StudentJumpDelegate();
                    break;
                case true when pPosY > 4.5f:
                    _targetY = 0.24f;
                    break;
                case true when pPosY < 0.26f:
                    _isJumping = false;
                    break;
            }
        }

        private void StudentMovementRotatian()
        {
            var playerT = transform;
            var direction = playerT.position + Vector3.right * (JoystickHorizontal * 30f);
            var lookRotation = Quaternion.LookRotation(direction);
            playerT.rotation = Quaternion.Slerp(playerT.rotation, lookRotation, 6f * Time.deltaTime);
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
            transform.position = new Vector3(0f, _targetY, 0f);
        }
    }
}