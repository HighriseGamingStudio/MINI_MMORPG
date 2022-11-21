using UnityEngine;
using UnityEngine.InputSystem;

public enum ControlType
{
    PointerFree,
    PointerRotY,
    PointerRotXY
}

public class InputController : MonoBehaviour
{
    private InputActions _input;
    private Camera _camera;

    [Header("Settings")]
    [SerializeField] private ControlType inputType;

    [Header("Move")]
    public bool MoveIsActive;
    public Vector2 MoveInput = Vector2.zero;

    [Header("Roll")]
    public bool RollIsActive = false;
    public int RollCount = 0;

    [Header("Look")]
    public bool LookIsActive;
    public Vector2 LookInput = Vector2.zero;

    [Header("Pointer Free")]
    [SerializeField] private LayerMask _groundLayer;
    public Vector2 PointerInput = Vector2.zero;
    public Vector3 PointerPosition = Vector3.zero;
 
    private void Awake()
    {
        _input = new InputActions();

        if (inputType == ControlType.PointerFree)
        {
            _camera = GetComponentInChildren<Camera>();
        }
    }

    private void Update()
    {
        if (inputType == ControlType.PointerFree)
        {
            SetPointerPosition();
        }
    }

    private void OnEnable()
    {
        _input.Global.Enable();

        _input.Global.Move.performed += SetMove;
        _input.Global.Move.canceled += SetMove;

        _input.Global.Jump.started += SetJump;
        _input.Global.Jump.canceled += SetJump;

        if (inputType == ControlType.PointerFree)
        {
            _input.PointerFree.Enable();

            _input.PointerFree.Pointer.performed += SetPointer;
            _input.PointerFree.Pointer.canceled += SetPointer;

            _input.PointerFree.Look.performed += SetLook;
            _input.PointerFree.Look.canceled += SetLook;
        }

    }

    private void OnDisable()
    {
        _input.Global.Move.performed -= SetMove;
        _input.Global.Move.canceled -= SetMove;

        _input.Global.Jump.started -= SetJump;
        _input.Global.Jump.canceled -= SetJump;

        _input.Global.Disable();

        if (inputType == ControlType.PointerFree)
        {
            _input.PointerFree.Pointer.performed -= SetPointer;
            _input.PointerFree.Pointer.canceled -= SetPointer;

            _input.PointerFree.Look.performed -= SetLook;
            _input.PointerFree.Look.canceled -= SetLook;

            _input.PointerFree.Disable();
        }
    }

    private void SetMove(InputAction.CallbackContext ctx)
    {
        MoveInput = ctx.ReadValue<Vector2>();
        MoveIsActive = ctx.performed;
    }

    private void SetJump(InputAction.CallbackContext ctx)
    {
        RollIsActive = ctx.started;
        if(RollIsActive)
            RollCount++;
    }


    private void SetPointer(InputAction.CallbackContext ctx)
    {
        PointerInput = ctx.ReadValue<Vector2>();
    }

    private void SetLook(InputAction.CallbackContext ctx)
    {
        LookInput = ctx.ReadValue<Vector2>();
        LookIsActive = ctx.performed;
    }

    private void SetPointerPosition()
    {
        Ray rc = _camera.ScreenPointToRay(PointerInput); ;
        if (Physics.Raycast(rc, out RaycastHit hit, float.MaxValue, _groundLayer))
        {
            PointerPosition = hit.point;
        }
    }

}
