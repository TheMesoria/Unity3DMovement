using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    // References

    // Customizable
    [Range(100f, 1000f)] public float mouseSensitivity;
    public float walkSpeed;
    public float runSpeed;

    // Script specific
    private float _horizontal, _vertical;
    private float _mouseX, _mouseY;
    
    private float moveSpeed;

    private Rigidbody _player;
    private Camera _camera;

    private Quaternion _deltaRotation;
    private Vector3 _deltaPosition;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        
        _player = GetComponent<Rigidbody>();
        _camera = Camera.main;
    }

    private void Update()
    {
        GetInputs();
        
        _camera.transform.Rotate(-Vector3.right * _mouseY * mouseSensitivity * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        _deltaRotation = Quaternion.Euler(Vector3.up * _mouseX * mouseSensitivity * Time.fixedDeltaTime);
        _player.MoveRotation(_player.rotation * _deltaRotation);

        var t = transform;
        _deltaPosition = (t.forward * _vertical + t.right * _horizontal) * moveSpeed * Time.fixedDeltaTime;
        _player.MovePosition(_player.position + _deltaPosition);
    }

    private void GetInputs()
    {
        // _mouseX = Input.GetAxis("Mouse X");
        // _mouseY = Input.GetAxis("Mouse Y");
        //
        // _horizontal = Input.GetAxis("Horizontal");
        // _vertical = Input.GetAxis("Vertical");
        //
        // moveSpeed = Input.GetButtonDown("Sprint") ? runSpeed : walkSpeed;
    }
}