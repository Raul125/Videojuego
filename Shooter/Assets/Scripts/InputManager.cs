using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerInput _playerInput;
    private PlayerInput.OnFootActions _onFoot;

    private PlayerMotor _playerMotor;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _onFoot = _playerInput.OnFoot;
    }

    private void Start()
    {
        _playerMotor = GetComponent<PlayerMotor>();
    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        _playerMotor.ProcessMove(_onFoot.Movement.ReadValue<Vector2>());
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
