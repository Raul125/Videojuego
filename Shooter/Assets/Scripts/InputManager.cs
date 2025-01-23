using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerInput _playerInput;
    private PlayerInput.OnFootActions _onFoot;

    private PlayerMotor _playerMotor;
    private PlayerLook _playerLook;

    private void Awake()
    {
        _playerInput = new PlayerInput();

        _onFoot = _playerInput.OnFoot;

        _onFoot.Jump.performed += _ => _playerMotor.Jump();
        _onFoot.Crouch.performed += _ => _playerMotor.Crouch();
        _onFoot.Sprint.performed += _ => _playerMotor.Sprint();
    }

    private void Start()
    {
        _playerMotor = GetComponent<PlayerMotor>();
        _playerLook = GetComponent<PlayerLook>();
    }

    private void FixedUpdate()
    {
        _playerMotor.ProcessMove(_onFoot.Movement.ReadValue<Vector2>());
    }

    private void LateUpdate()
    {
        _playerLook.ProcessLook(_onFoot.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        _onFoot.Enable();
    }

    private void OnDisable()
    {
        _onFoot.Disable();
    }
}
