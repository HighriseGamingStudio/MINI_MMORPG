//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Movement1 : MonoBehaviour
//{
//    [Header("Components")]
//    [SerializeField] Transform _playerModel;
//    InputController _inputController;
//    CharacterController _characterController;
//    Camera _camera;

//    [Header("Look")]
//    [SerializeField] private float _lookSpeedX = 1;
//    //[SerializeField] private float _lookSpeedY = 1;

//    [Header("Move")]
//    [SerializeField] private float _moveSpeed = 6;
//    [SerializeField] private float _rotationSpeed = 6;
//    private Vector3 _moveDirection = Vector3.zero;
//    private Vector3 _rotateDirection = Vector3.zero;

//    [Header("Roll")]
//    [SerializeField] private bool _rollActive = false;
//    [SerializeField] private float _rollSpeed = 10f;
//    [SerializeField] private float _rollLerp = .3f;
//    [SerializeField] private Vector3 _rollDirection = Vector3.zero;
//    //[SerializeField] private bool _allowMoveWithJump = false;
//    //[SerializeField] private bool _jumpActive = false;
//    //[SerializeField] private float _jumpForceY = 10f;
//    //[SerializeField] private float _jumpForceX = 50f;
//    //[SerializeField] private float _continuedJumpForceY = .5f;
//    //[SerializeField] private float _airTimeMax = 2f;
//    //[SerializeField] private float _airTimeMin = 1f;
//    //[SerializeField] private float _airTimer = 0f;
//    //[SerializeField] private float _gravity = 3.14f;
//    //private Vector3 _jumpDirection = Vector3.zero;

//    [Header("Debug")]
//    [SerializeField] private Vector3 _velocity = Vector3.zero;


//    private void Awake()
//    {
//        _inputController = GetComponent<InputController>();
//        _characterController = GetComponent<CharacterController>();
//        _camera = GetComponentInChildren<Camera>();
//    }

//    //private void Start()
//    //{
//    //    _jumpForceX = _moveSpeed * 2;
//    //}

//    // Update is called once per frame
//    void FixedUpdate()
//    {
//        Look();

//        if (!_rollActive)
//            Move();
//        if (_inputController._JumpCount > 0)
//            Jump();

//        RotatePlayer();
//    }

//    private void Look()
//    {
//        float rotX = _inputController._LookInput.x * _lookSpeedX * Time.deltaTime;
//        _camera.transform.RotateAround(transform.position, transform.up, rotX);

//        //float rotY = _inputController._LookInput.y * _lookSpeedY * Time.deltaTime;
//        //_camera.transform.RotateAround(transform.position, transform.right, rotY);
//    }

//    private void Move()
//    {
//        Vector3 relativeForward = new Vector3(_camera.transform.forward.x, 0f, _camera.transform.forward.z).normalized;
//        Vector3 relativeRight = new Vector3(_camera.transform.right.x, 0f, _camera.transform.right.z).normalized;

//        Vector3 forwardDirection = _inputController._MoveInput.y * relativeForward;
//        Vector3 rightDirection = _inputController._MoveInput.x * relativeRight;

//        _moveDirection = forwardDirection + rightDirection;
//        _characterController.Move(_moveDirection * _moveSpeed * Time.fixedDeltaTime);

//    }

//    private void Jump()
//    {
//        Vector3 rawMovement = (_moveDirection != Vector3.zero) ? _moveDirection.normalized : _playerModel.forward;
//        _rollDirection = Vector3.MoveTowards(transform.position, _rollDirection, _rollSpeed * Time.deltaTime);

//        //Vector3 finalMovement = transform.TransformVector(_currentMovement);
//        //_characterController.Move(finalMovement * _jumpForceX * Time.deltaTime);
//    }

//    //private float Gravity()
//    //{
//    //    if(!_characterController.isGrounded)
//    //    {
//    //        return _gravity;
//    //    }

//    //    return .2f;
//    //}

//    private void RotatePlayer()
//    {
//        if (_rotateDirection != _moveDirection && _moveDirection.magnitude > 0)
//        {
//            Quaternion rot = Quaternion.LookRotation(_moveDirection, Vector3.up);
//            _playerModel.rotation = Quaternion.RotateTowards(_playerModel.rotation, rot, _rotationSpeed * Time.deltaTime);
//            _rotateDirection = _playerModel.forward;
//        }
//    }
//}
