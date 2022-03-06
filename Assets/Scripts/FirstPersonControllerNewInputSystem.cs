using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Input;
using UnityEngine.UI;

public class FirstPersonControllerNewInputSystem : MonoBehaviour
{
    // Customizable
    [Range(0f, 100f)] public float mouseSensitivity;
    public float walkSpeed;
    public float runSpeed;
    
    // Script specific
    private SimpleGameActions _simpleGameActions;
    
    private float _horizontal, _vertical;
    private Vector2 _mouseReading;

    private float _moveSpeed = 10f;

    private Rigidbody _player;
    private Camera _camera;

    private Quaternion _deltaRotation;
    private Vector3 _deltaPosition;

    private void Awake()
    {
        // QualitySettings.vSyncCount = 1;
        // Application.targetFrameRate = 30;
        _simpleGameActions = new SimpleGameActions();

        _simpleGameActions.Player.Sprint.performed += OnSprint;
    }

    private void Update()
    {
        _mouseReading = _simpleGameActions.Player.Look.ReadValue<Vector2>();
        
        _camera.transform.Rotate(-Vector3.right * _mouseReading.y * mouseSensitivity * Time.deltaTime);
        
        _deltaRotation = Quaternion.Euler(Vector3.up * _mouseReading.x * mouseSensitivity * Time.fixedDeltaTime);
        _player.MoveRotation(_player.rotation * _deltaRotation);
        
        // _camera.transform.Rotate(-Vector3.right * _mouseReading.x * mouseSensitivity * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        var input = _simpleGameActions.Player.Move.ReadValue<Vector2>();
        _player.AddForce(new Vector3(input.x, 0, input.y) * _moveSpeed, ForceMode.Force);
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        _player = GetComponent<Rigidbody>();
        _camera = Camera.main;
    }

    private void OnSprint(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;

        Debug.Log("Sprint!");
    }

    public void OnEnable()
    {
        _simpleGameActions.Enable();
    }

    public void OnDisable()
    {
        _simpleGameActions.Disable();
    }
}