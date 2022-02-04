using UnityEngine;

namespace StudentScript
{
    public class StudentAnimatorController : MonoBehaviour
    {
        private Animator _animator;
        private static readonly int s_StudentRun = Animator.StringToHash("StudentRun");
        private static readonly int s_StudentAngry = Animator.StringToHash("StudentAngry");
        private static readonly int s_StudentStumble = Animator.StringToHash("StudentStumble");

        private void Awake()
        {
            _animator = gameObject.GetComponent<Animator>();
        }

        private void Start()
        {
            StudentRunAnimation(true);
        }

        private void StudentRunAnimation(bool setActive)
        {
            _animator.SetBool(s_StudentRun, setActive);
        }

        private void StudentAngryAnimationSetActive()
        {
            _animator.SetBool(s_StudentAngry, true);
        }

        private void StudentAngryAnimationSetPassive()
        {
            _animator.SetBool(s_StudentAngry, false);
            StudentRunAnimation(false);
        }

        private void StudentStumbleAnimationActive()
        {
            _animator.SetBool(s_StudentStumble, true);
        }

        public void StudentStumbleAnimationPassive()
        {
            //using by animator event system 
            _animator.SetBool(s_StudentStumble, false);
        }

    }
}