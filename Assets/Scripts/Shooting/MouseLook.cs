using UnityEngine;

public class MouseLook : MonoBehaviour
{
    private float mouseSensitivity = 500f;
    private float currentVerticalRotation;
    private float currentHorizontalRotation;
    private bool lookEnabled = false;

    public void EnableLook()
    {
        lookEnabled = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (!lookEnabled) return;

        // calculate new rotations
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // clamp vertical rotation
        currentVerticalRotation -= mouseY;
        currentVerticalRotation = Mathf.Clamp(currentVerticalRotation, -45f, 20f);

        // clamp horizontal rotation
        currentHorizontalRotation += mouseX;
        currentHorizontalRotation = Mathf.Clamp(currentHorizontalRotation, -90f, 90f);

        // apply rotations to transform
        transform.rotation = Quaternion.Euler(currentVerticalRotation, currentHorizontalRotation, 0f);
    }
}
