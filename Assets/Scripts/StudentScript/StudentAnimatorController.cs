using UnityEngine;

namespace StudentScript
{
    public class StudentAnimatorController : MonoBehaviour
    {
        private Animator _animator;
        private static readonly int StudentRun = Animator.StringToHash("StudentRun");
        private static readonly int StudentAngry = Animator.StringToHash("StudentAngry");
        private static readonly int StudentStumble = Animator.StringToHash("StudentStumble");
        private static readonly int StudentJump = Animator.StringToHash("StudentJump");

        private void Awake()
        {
            _animator = gameObject.GetComponent<Animator>();
        }

        private void OnEnable()
        {
            
            CanvasManager.TimerDelegate += StudentRunAnimationSetActive;
            GameManager.ResetLevelDelegate += StudentAngryAnimationSetPassive;
            StudentCollisionController.ObstacleCollisionDelegate += StudentStumbleAnimationActive;
            StudentCollisionController.TeacherCollisionDelegate += StudentAngryAnimationSetActive;
            StudentMovementController.StudentJumpDelegate += StudentJumpAnimationActive;
        }

        private void StudentRunAnimationSetActive()
        {
            StudentRunAnimation(true);
        }

        private void StudentRunAnimation(bool setActive)
        {
            _animator.SetBool(StudentRun, setActive);
        }

        private void StudentAngryAnimationSetActive()
        {
            _animator.SetBool(StudentAngry, true);
        }

        private void StudentAngryAnimationSetPassive()
        {
            _animator.SetBool(StudentAngry, false);
            StudentRunAnimation(false);
        }

        private void StudentStumbleAnimationActive()
        {
            _animator.SetBool(StudentStumble, true);
        }

        public void StudentStumbleAnimationPassive()
        {
            //using by animator event system 
            _animator.SetBool(StudentStumble, false);
        }

        private void StudentJumpAnimationActive()
        {
            _animator.SetBool(StudentJump ,true);
        }
        public void StudentJumpAnimationPassive()
        {
            //using by animator event system 
            _animator.SetBool(StudentJump ,false);
        }
    }
}