using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController _characterController;
    private Vector3 _playerVelocity;
    private bool _isGrounded;

    public float Speed = 5f;
    public float Gravity = -9.81f;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        _isGrounded = _characterController.isGrounded;
    }

    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        _characterController.Move(transform.TransformDirection(moveDirection) * Speed * Time.deltaTime);
        _playerVelocity.y += Gravity * Time.deltaTime;
        if (_isGrounded && _playerVelocity.y < 0)
        {
            _playerVelocity.y = 0f;
        }

        _characterController.Move(_playerVelocity * Time.deltaTime);
    }
}
