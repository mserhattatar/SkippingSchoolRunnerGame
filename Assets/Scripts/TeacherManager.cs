using System;
using StudentScript;
using UnityEngine;

public class TeacherManager : JoystickManager
{
    private Animator _animator;
    private bool _isTeacherStarted;

    public GameObject studentPlayer;
    [HideInInspector] public float distance;

    private void Awake()
    {
        _animator = gameObject.GetComponent<Animator>();
    }

    private void OnEnable()
    {
        CanvasManager.TimerDelegate += StartTeacherRun;
        GameManager.ResetLevelDelegate += ResetTeacher;
        GameManager.ResetLevelDelegate += SetTeacherTag;
        StudentCollisionController.ObstacleCollisionDelegate += UpdateDistance;
        StudentCollisionController.TeacherCollisionDelegate += TeacherLookBackRotation;
        StudentCollisionController.TeacherCollisionDelegate += TeacherRunAnimationSetPassive;
        StudentCollisionController.TeacherCollisionDelegate += DeleteTeacherTag;
    }

    private void Start()
    {
        distance = -7f;
    }

    private void UpdateDistance()
    {
        distance += 1f;
    }

    private void StartTeacherRun()
    {
        _isTeacherStarted = true;
        TeacherRunAnimationSetActive();
    }

    private void FixedUpdate()
    {
        if (_isTeacherStarted)
            TeacherMovement();
    }

    private void TeacherMovement()
    {
        var studentPos = studentPlayer.transform.position;
        transform.position = Vector3.Lerp(transform.position, (studentPos + (Vector3.forward * distance)),
            4f * Time.deltaTime);
        if (transform.position.z >= studentPlayer.transform.position.z)
            distance = 1f;
    }

    private void TeacherLookBackRotation()
    {
        transform.rotation = Quaternion.Euler(0f, 180f, 0f);
    }

    private void TeacherRunAnimationSetActive()
    {
        _animator.SetBool("teacherRun", true);
    }

    private void TeacherRunAnimationSetPassive()
    {
        _animator.SetBool("teacherRun", false);
    }

    private void DeleteTeacherTag()
    {
        gameObject.tag = "Untagged";
    }

    private void SetTeacherTag()
    {
        gameObject.tag = "Teacher";
    }

    private void ResetTeacher()
    {
        transform.rotation = Quaternion.Euler(0f, 0, 0f);
        distance = -7f;
        _isTeacherStarted = false;
        TeacherRunAnimationSetPassive();
        transform.position = new Vector3(0f, 0.23f, -3.38f);
    }
}