using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera Camera;
    private float _xRotation = 0f;

    public float XSensitivity = 30f;
    public float YSensitivity = 30f;

    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        _xRotation -= mouseY * Time.deltaTime * YSensitivity;
        _xRotation = Mathf.Clamp(_xRotation, -80f, 80f);

        Camera.transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        transform.Rotate(mouseX * Time.deltaTime * XSensitivity * Vector3.up);
    }
}
