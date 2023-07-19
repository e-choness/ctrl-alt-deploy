using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 1.0f;
    public float jumpHeight = 1.0f;
    
    private Rigidbody _rigidbody = null;
    private float _movementX = 0.0f;
    private float _movementZ = 0.0f;
    private float _movementY = 0.0f;

    private bool _isGrounded = true;
    
    // Start is called before the first frame update
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    
    // Get the movement data
    void OnMove(InputValue movementValue)
    {
        var movementVector = movementValue.Get<Vector2>();
        _movementX = movementVector.x;
        _movementZ = movementVector.y;
        // Debug.Log(message: string.Format("{0} {1}", _movmentX, _movementY));
    }

    // void OnJump(InputValue jumpValue)
    // {
    //     var jumpVector = jumpValue.Get<Vector3>();
    //     _movementY = jumpVector.z;
    // }
    
    // Update is called once per frame, all physics calculations are better here
    private void FixedUpdate()
    {
        MoveBall();
        // JumpBall();
    }

    void MoveBall()
    {
        var movementVector3 = new Vector3(_movementX, 0.0f, _movementZ);
        _rigidbody.AddForce(movementVector3 * movementSpeed);
    }

    // void JumpBall()
    // {
    //     var movementVector3 = new Vector3(0.0f, _movementY, 0.0f);
    //     _rigidbody.AddForce(movementVector3 * jumpHeight);
    // }
 
}
