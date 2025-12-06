using UnityEngine;

public class MouseLook : MonoBehaviour
{
    private float mouseSensitivity = 800f;
    private float currentVerticalRotation;
    private float currentHorizontalRotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // locks cursor to center of screen
        Cursor.lockState = CursorLockMode.Locked;

        //initialize rotation values
        currentVerticalRotation = transform.rotation.eulerAngles.x;
        currentHorizontalRotation = transform.rotation.eulerAngles.y;

        // if (currentVerticalRotation > 180f)
        //     currentVerticalRotation -= 360f;
        // if (currentHorizontalRotation > 180f)
        //     currentHorizontalRotation -= 360f;
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
        currentHorizontalRotation = Mathf.Clamp(currentHorizontalRotation, -180f, 0f);

        // apply rotations to transform
        transform.rotation = Quaternion.Euler(currentVerticalRotation, currentHorizontalRotation, 0f);
    }
}
