using UnityEngine;
using UnityEngine.InputSystem.XInput;
using Debug = UnityEngine.Debug;

public class MovementController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Transform _playerModel;
    InputController _inputController;
    CharacterController _characterController;
    Camera _camera;

    [Header("Move")]
    [SerializeField] float _lookSpeed = 4f;
    [SerializeField] float _moveSpeed = 4f;
    [SerializeField] float _rotationSpeed = 5f;

    [Header("Roll")]
    [SerializeField] bool _rollActive = false;
    [SerializeField] [Range(0, 5)] float _rollSpeed = 3f;
    [SerializeField] [Range(0, 1)] float _rollBuffer = 0.2f;
    [SerializeField] float _rollDistance = 4f;
    private Vector3 _rollStart = Vector3.zero;

    [Header("Debug")]
    [SerializeField] private Vector3 _aimDirection = Vector3.zero;
    [SerializeField] private Vector3 _moveDirection = Vector3.zero;
    [SerializeField] private Vector3 _rollDirection = Vector3.zero;
    private Vector3 _rotateDirection = Vector3.zero;


    private void Awake()
    {
        _inputController = GetComponent<InputController>();
        _characterController = GetComponent<CharacterController>();
        _camera = GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        if(_inputController.LookInput != Vector2.zero)
            Look();

        Aim();
    }

    private void FixedUpdate()
    {
        if (_inputController.RollCount > 0)
            Roll();
        if (!_rollActive && _inputController.MoveInput != Vector2.zero)
            Move();

        RotatePlayer();
    }

    private void Look()
    {
        float rotX = _inputController.LookInput.x * _lookSpeed * 10f * Time.deltaTime;
        _camera.transform.RotateAround(transform.position, transform.up, rotX);
    }

    private void Aim()
    {
        Vector3 worldDirection = transform.InverseTransformDirection(_inputController.PointerPosition);
        worldDirection.y = 0f;
        _aimDirection = worldDirection.normalized;
    }

    private void Move()
    {
        Vector3 relativeForward = new Vector3(_camera.transform.forward.x, 0f, _camera.transform.forward.z).normalized;
        Vector3 relativeRight = new Vector3(_camera.transform.right.x, 0f, _camera.transform.right.z).normalized;

        Vector3 forwardDirection = _inputController.MoveInput.y * relativeForward;
        Vector3 rightDirection = _inputController.MoveInput.x * relativeRight;

        Vector3 destination = forwardDirection + rightDirection;

        _moveDirection = destination;
        _characterController.Move(_moveDirection * _moveSpeed * Time.fixedDeltaTime);
    }

    private void Roll()
    {
        if (!_rollActive) {
            _rollActive = true;
            _rollStart = transform.position;
            _rollDirection = (_moveDirection != Vector3.zero) ? _moveDirection : _playerModel.forward;
            _rollDirection.y = 0f;
            _rollDirection *= _rollDistance;
            _rollDirection = transform.TransformPoint(_rollDirection);
            TempCrouch(true);
        }

        float rollCheck = (transform.position - _rollDirection).magnitude;
        if (_rollActive && !(rollCheck > -_rollBuffer && rollCheck < _rollBuffer)) {
            transform.position = Vector3.Lerp(transform.position, _rollDirection, _rollSpeed * Time.fixedDeltaTime);
        }
        else {
            _rollActive = false;
            _inputController.RollCount = 0;
            _rollDirection = Vector3.zero;
            TempCrouch(false);
        }
    }

    private void TempCrouch(bool on)
    {
        if(on) {
            _playerModel.localScale = new Vector3(1,.5f,1);
            _playerModel.localPosition = new Vector3(0,.5f,0);
        }
        else {
            _playerModel.localScale = new Vector3(1,1,1);
            _playerModel.localPosition = new Vector3(0,1,0);
        }
    }

    private void RotatePlayer()
    {
        if (_rotateDirection != _moveDirection) {
            Quaternion rot = Quaternion.LookRotation(_moveDirection, Vector3.up);
            _playerModel.rotation = Quaternion.RotateTowards(_playerModel.rotation, rot, _rotationSpeed * 100f * Time.fixedDeltaTime);
            _rotateDirection = _playerModel.forward;
        }
    }

    private void OnDrawGizmos()
    {
        if(_rollActive)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_rollDirection, .2f);
        }

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, _aimDirection);
    }
}