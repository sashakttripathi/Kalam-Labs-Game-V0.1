using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Animator _animator;

    private CharacterController _characterController;

    [SerializeField]
    public FixedJoystick Controller;

    public GripMeter gripMeter;

    public float Speed = 5.0f, jumpspeed = 5.0f;

    //public GameObject ClimbFinalPos;

    public float RotationSpeed = 240.0f;

    private float Gravity = 20.0f;

    public bool climbArea = false, jump = false;
    private bool fall = false, griploose = false, hang = true, tryclimb = false, climb = false;

    private Vector3 _moveDir = Vector3.zero;

    // Use this for initialization
    void Start()
    {
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get Input for axis
        /*        float h = Input.GetAxis("Horizontal");
                float v = Input.GetAxis("Vertical");
        */

        /*float h = Controller.Horizontal;
        float v = Controller.Vertical;


        // Calculate the forward vector
        Vector3 camForward_Dir = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 move = v * camForward_Dir + h * Camera.main.transform.right;

        if (move.magnitude > 1f) move.Normalize();

        // Calculate the rotation for the player
        move = transform.InverseTransformDirection(move);

        // Get Euler angles
        float turnAmount = Mathf.Atan2(move.x, move.z);

        transform.Rotate(0, turnAmount * RotationSpeed * Time.deltaTime, 0);

        if (_characterController.isGrounded)
        {
            //Debug.Log("character controller grounded");
            _animator.SetFloat("Velocity", move.magnitude);
            _moveDir = transform.forward * move.magnitude;
            //_animator.ResetTrigger("jump");
            _animator.ResetTrigger("Hang");
            _animator.ResetTrigger("Idle");
            _moveDir *= Speed;
        }
        if ((Input.GetKey("space") || jump) && _characterController.isGrounded)
        {
            if (climbArea)
            {
                transform.rotation = Quaternion.Euler(0, 150, 0);
                _animator.SetTrigger("Hang");
            }
            else
            {
                //_animator.SetTrigger("jump");
                transform.rotation = Quaternion.Euler(0, 150, 0);
                _animator.SetTrigger("Hang");
                //_moveDir.y = jumpspeed;
            }
            jump = false;
        }
        if (!climbArea)
        {
            //Debug.Log("gravity applied");
            _moveDir.y -= Gravity * Time.deltaTime;
        }
        if (Input.GetKey("q"))
        {
            _animator.SetTrigger("Idle");
        }
        _characterController.Move(_moveDir * Time.deltaTime);
        */
        _animator.ResetTrigger("Idle");
        _animator.ResetTrigger("Hang");
        float CR = gripMeter.currentReading;
        float LT = gripMeter.LowerThreshold;
        float UT = gripMeter.UpperThreshold;
        float RoD = gripMeter.rateDecrease;
        while (true)
        {
            if (hang)
            {
                if (CR > LT)
                {
                    Debug.Log("H to TC");
                    _animator.SetTrigger("Hang");
                    hang = false; tryclimb = true;
                }
                else if (CR < LT)
                {
                    Debug.Log("H to GL");
                    _animator.SetTrigger("Idle");
                    hang = false; griploose = true;
                }
                break;
            }

            if (griploose)
            {
                if (CR > LT)
                {
                    Debug.Log("GL to H");
                    _animator.SetTrigger("Hang");
                    griploose = false; hang = true;
                }
                else if (CR < RoD)
                {
                    Debug.Log("GL to Fall");
                    _animator.SetTrigger("Idle");
                    griploose = false; fall = true;
                }
                break;
            }

            if (tryclimb)
            {
                if (CR < LT)
                {
                    Debug.Log("TC to H");
                    _animator.SetTrigger("Idle");
                    tryclimb = false; hang = true;
                }
                else if (CR > UT)
                {
                    Debug.Log("TC to C");
                    _animator.SetTrigger("Hang");
                    tryclimb = false; climb = true;
                }
                break;
            }
            else
            {
                break;
            }
        }
                
    }

    public void Jump()
    {
        jump = true;
    }

    public void callHang()
    {
        _animator.SetTrigger("Hang");
    }

}
