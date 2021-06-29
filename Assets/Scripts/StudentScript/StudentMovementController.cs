using UnityEngine;
public class StudentMovementController : JoystickManager
{
    [HideInInspector]
    public bool stopPlayerMovement;
    
    [SerializeField] private float forwardSpeed = 2.4f;
    [SerializeField] private float movementSpeed = 1.7f;
    private float _targetY;
    private bool _isJumping;
    private bool _stopPos;
    private bool _isPlayerRun;
   
    
    private void Awake()
    {
        _targetY = 0.24f;
        _isPlayerRun = false;
    }

    private void Start()
    {
        CanvasController.timerDelegate += StudentMovement;
        GameManager.resetLevelDelegate += ResetMovement;
        StudentCollisionController.teacherCollisionDelegate += StopStudentMovement;
    }
    private void FixedUpdate()
    {
        if(_isPlayerRun)
            StudentMovement();
    }
    private void StudentMovement()
    {
        if (!_isPlayerRun)
            _isPlayerRun = true;
        if (stopPlayerMovement)
        {
            if (_stopPos || !(transform.position.y > 0.3f)) return;
            transform.position = new Vector3(transform.position.x, 0.24f, transform.position.z);
            _stopPos = true;
            return;
        }
        var pPos = transform.position;
        
        var targetX = pPos.x + joystickHorizontal * movementSpeed;
        if (targetX <= -4.5f)
            targetX = -4.5f;
        else if (targetX >= 4.5f)
            targetX = 4.5f;

        var targetZ = pPos.z + forwardSpeed;
        var direction = new Vector3(x: targetX, _targetY, targetZ);
        StudentJump();
        transform.position = Vector3.MoveTowards (pPos, direction ,15f * Time.deltaTime);
        StudentMovementRotatian();
    }

    private void StudentJump()
    {
        var positionYPos = transform.position.y;
        if (!_isJumping && joystickVertical > 0.5f)
        {
            _targetY = 4.5f;
            _isJumping = true;
        }

        if (_isJumping && positionYPos > 4f)
            _targetY = 0.24f;
        
        if (_isJumping && positionYPos < 0.26f)
            _isJumping = false;
    }

    private void StudentMovementRotatian()
    {
        var direction = gameObject.transform.position + Vector3.right * (joystickHorizontal * 30f);
        var lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 6f * Time.deltaTime);
    }

    private void StopStudentMovement()
    {
        stopPlayerMovement = true;
    }

    private void ResetMovement()
    {
        _isPlayerRun = false;
        stopPlayerMovement = false;
        transform.position = new Vector3(0f, 0.245f, 0f);
    }
}
