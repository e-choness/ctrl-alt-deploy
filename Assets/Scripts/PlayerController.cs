using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidbody = null;

    private float _movmentX = 0.0f;

    private float _movementY = 0.0f;
    // Start is called before the first frame update
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    
    // 
    void OnMove(InputValue movementValue)
    {
        var movementVector = movementValue.Get<Vector2>();
        _movmentX = movementVector.x;
        _movementY = movementVector.y;
        // Debug.Log(message: string.Format("{0} {1}", _movmentX, _movementY));
    }
    
    // Update is called once per frame
    private void FixedUpdate()
    {
        var movementVector3 = new Vector3(_movmentX, 0.0f, _movementY);
        _rigidbody.AddForce(movementVector3);
    }
 
}
