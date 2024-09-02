using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    #region Camera Attributes
    [Header("----- Camera Settings -----")]
    // Sensitivity of the camera movement
    [SerializeField] int sensitivity;

    // Limits for vertical camera rotation (min and max)
    [SerializeField] int lockVertMin, lockVerMax;

    // Option to invert the Y-axis control
    [SerializeField] bool invertY;
    #endregion

    #region Private Variables
    // Store the current X-axis rotation value
    float rotateX;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        // Hide the cursor and lock it to the center of the screen
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // Get mouse input
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;

        // Invert Y-axis if the option is selected
        if (invertY)
        {
            rotateX += mouseY; // Increment rotation based on mouseY input
        }
        else
        {
            rotateX -= mouseY; // Decrement rotation based on mouseY input
        }

        // Clamp the rotation on the X-axis to prevent over-rotation
        rotateX = Mathf.Clamp(rotateX, lockVertMin, lockVerMax);

        // Apply rotation to the camera on the X-axis
        transform.localRotation = Quaternion.Euler(rotateX, 0, 0);

        // Rotate the player object on the Y-axis
        transform.parent.Rotate(Vector3.up * mouseX);
    }
}
