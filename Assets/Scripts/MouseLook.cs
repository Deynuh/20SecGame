using UnityEngine;

public class MouseLook : MonoBehaviour
{
    private float mouseSensitivity = 500f;
    private float currentVerticalRotation;
    private float currentHorizontalRotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // locks cursor to center of screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
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
