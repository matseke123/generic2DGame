using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerScript : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private float _jumpCooldown = 3f;
    [SerializeField] private float _dashCooldown = 5f;
    [SerializeField] private float _dashDuration = 1f;
    [SerializeField] private float _dashSpeed = 10f;
    [SerializeField] private float _gravity = -9.81f;
    private float _verticalVelocity = 0f;
    private Vector3 _move = new Vector3(0, 0, 0);
    private CharacterController _characterController;
    
    public float _moveInput;
    public bool _jumpInput;
    public bool _dashInput;

    public float _jumpTimer = 0f;
    private float _dashTimer = 0f;
    private float _dashDurationTimer = 0f;

    [SerializeField] public GameObject _platform;
    private 


    enum PlayerState 
    {
        Idle,
        Walking,
        Dashing
    }
    PlayerState _playerState;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _playerState = PlayerState.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimers();
        AssignActions();
        CheckInputs();
        CheckGravity();
        ApplyPlatformMovement();
        MoveCC();

        if (_moveInput != 0) Debug.Log("Moving");
        Debug.Log(_jumpInput);
        Debug.Log(_playerState);
    }



    private void ApplyPlatformMovement()
    {
        if(_platform != null)
        {
            _move.x += _platform.GetComponent<ParentPlatformLogic>().PlatformVelocity.x;
            _move.y += _platform.GetComponent<ParentPlatformLogic>().PlatformVelocity.y;
        }
    }

    private void UpdateTimers()
    {
        if(_jumpTimer <= _jumpCooldown)
        {
            _jumpTimer += Time.deltaTime;
        }
        if(_dashTimer <= _dashCooldown)
        {
            _dashTimer += Time.deltaTime;
        }
        if(_playerState == PlayerState.Dashing && _dashDurationTimer < _dashDuration)
        {
            _dashDurationTimer += Time.deltaTime;
        }
    }

    private void AssignActions()
    {
        if(_playerState != PlayerState.Dashing)
        {
            _moveInput = InputSystem.actions.FindAction("Move").ReadValue<float>();
        }       
        _jumpInput = InputSystem.actions.FindAction("Jump").triggered;
        _dashInput = InputSystem.actions.FindAction("Dash").triggered;
    }
    private void CheckInputs()
    {
        CheckJump();
        CheckMove();
        CheckDash();

    }

    private void CheckDash()
    {
        if(_dashInput == true && _dashTimer > _dashCooldown && _move.x != 0)
        {
            _playerState = PlayerState.Dashing;

        }
        if(_playerState == PlayerState.Dashing)
        {
            _move.x = _dashSpeed * _moveInput;
            _verticalVelocity = 0;
        }
        if(_dashDurationTimer >= _dashDuration)
        {
            _dashDurationTimer = 0f;
            _dashTimer = 0f;
            _playerState = PlayerState.Idle;
        }
    }

    private void CheckJump()
    {
        if (_characterController.isGrounded == true && _jumpInput && _jumpTimer >= _jumpCooldown && _playerState != PlayerState.Dashing)
        {
            _verticalVelocity = _jumpForce;
            _jumpTimer = 0f; 
        }
    }
    private void CheckMove()
    {
        if(_playerState == PlayerState.Dashing && _dashDurationTimer < _dashDuration)
        {
            return;
        }
        _move.x = _moveInput * _moveSpeed;
        if(_moveInput != 0 && _playerState != PlayerState.Dashing)
        {
            _playerState = PlayerState.Walking;
        }
        if(_moveInput == 0 && _characterController.isGrounded && _playerState != PlayerState.Dashing)
        {
            _playerState = PlayerState.Idle;
        }
    }
    private void CheckGravity()
    {
        if(!_characterController.isGrounded && _playerState != PlayerState.Dashing)
        {
            _verticalVelocity += _gravity * Time.deltaTime;
        }

        _move.y = _verticalVelocity;

    }
    private void MoveCC()
    {
        _characterController.Move(_move * Time.deltaTime);
    }




}
