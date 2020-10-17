﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator _animator;

    private CharacterController _characterController;

    [SerializeField]
    public FixedJoystick Controller;

    public float Speed = 5.0f, jumpspeed = 5.0f;

    public GameObject ClimbStair;

    public float RotationSpeed = 240.0f;

    private float Gravity = 20.0f;

    public bool climbArea = false;

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

        float h = Controller.Horizontal;
        float v = Controller.Vertical;

        // Calculate the forward vector
        Vector3 camForward_Dir = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 move = v * camForward_Dir + h * Camera.main.transform.right;

        if (move.magnitude > 1f) move.Normalize();

        // Calculate the rotation for the player
        move = transform.InverseTransformDirection(move);

        // Get Euler angles
        float turnAmount = Mathf.Atan2(move.x, move.z);

        transform.Rotate(0, turnAmount *  RotationSpeed * Time.deltaTime, 0);

        if (_characterController.isGrounded)
        {
            _animator.SetFloat("Velocity", move.magnitude);
            //Debug.Log(_animator.GetFloat("Velocity"));
            _moveDir = transform.forward * move.magnitude;
            _animator.ResetTrigger("jump");
            _animator.ResetTrigger("Hang");

            _moveDir *= Speed;

        }
        if(Input.GetKey("space") && _characterController.isGrounded)
        {
            if (climbArea)
            {
                this.transform.LookAt(ClimbStair.transform);
                transform.rotation = Quaternion.Euler (0, transform.rotation.y, 0);
                _animator.SetTrigger("Hang");                
            }
            else
            {
                _animator.SetTrigger("jump");
            }
            //_moveDir.y = jumpspeed;
        }
        if (!climbArea)
        {
            _moveDir.y -= Gravity * Time.deltaTime;
        }
        else
        {
            if (!_characterController.isGrounded)
            {
                _moveDir.y -= Gravity * Time.deltaTime;
            }
        }
        _characterController.Move(_moveDir * Time.deltaTime);

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "ClimbArea")
        {
            climbArea = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "ClimbArea")
        {
            _animator.ResetTrigger("Hang");
            climbArea = false;
        }


    }
}
