using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class HumanoidLandInput : MonoBehaviour
{
    InputActions_old _input;

    [Header("Move Input")]
    [SerializeField] private bool HoldToRun = true;
    public bool MoveIsActive;
    public bool RunIsActive;
    public Vector2 MoveInput = Vector2.zero;

    [Header("Look Input")]
    public bool InvertMouseY = true;
    public bool InvertScroll = false;
    public float ZoomCameraInput  { get; private set; } = 0.0f;
    public bool ChangeCameraInput { get; private set; } = false;
    public Vector2 LookInput = Vector2.zero;
    

    private void OnEnable()
    {
        _input = new InputActions_old();
        _input.HumanoidLand.Enable();

        _input.HumanoidLand.Move.performed += SetMove;
        _input.HumanoidLand.Move.canceled += SetMove;

        _input.HumanoidLand.Run.started += SetRun;
        _input.HumanoidLand.Run.canceled += SetRun;

        _input.HumanoidLand.Look.performed += SetLook;
        _input.HumanoidLand.Look.canceled += SetLook;

        _input.HumanoidLand.ZoomCamera.started += SetZoomCamera;
        _input.HumanoidLand.ZoomCamera.canceled += SetZoomCamera;
    }

    private void OnDisable()
    {
        _input.HumanoidLand.Move.performed -= SetMove;
        _input.HumanoidLand.Move.canceled -= SetMove;

        _input.HumanoidLand.Run.started -= SetRun;
        _input.HumanoidLand.Run.canceled -= SetRun;

        _input.HumanoidLand.Look.performed -= SetLook;
        _input.HumanoidLand.Look.canceled -= SetLook;

        _input.HumanoidLand.ZoomCamera.started -= SetZoomCamera;
        _input.HumanoidLand.ZoomCamera.canceled -= SetZoomCamera;

        _input.HumanoidLand.Disable();
    }

    private void Update()
    {
        ChangeCameraInput = _input.HumanoidLand.ChangeCamera.WasPerformedThisFrame();
    }

    private void SetMove(InputAction.CallbackContext ctx)
    {
        MoveInput = ctx.ReadValue<Vector2>();
        MoveIsActive = ctx.performed;
    }

    private void SetRun(InputAction.CallbackContext ctx)
    {
        if(HoldToRun) { RunIsActive = ctx.started; }
        else { if(ctx.started) { RunIsActive = !RunIsActive; } }
    }

    private void SetLook(InputAction.CallbackContext ctx)
    {
        LookInput = ctx.ReadValue<Vector2>();
    }

    private void SetZoomCamera(InputAction.CallbackContext ctx)
    {
        ZoomCameraInput = ctx.ReadValue<float>();
    }
}
