using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggController : MonoBehaviour
{
    public float rollSpeed = 5f;

    void Start()
    {
        Cursor.visible = false; 
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        // Get the input for movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Get the camera's forward and right vectors
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;

        // Project the vectors onto the horizontal plane (Y = 0)
        cameraForward.y = 0;
        cameraRight.y = 0;

        // Normalize the vectors to ensure consistent speed in all directions
        cameraForward.Normalize();
        cameraRight.Normalize();

        // Calculate the movement direction based on input and camera orientation
        Vector3 movement = (cameraForward * verticalInput + cameraRight * horizontalInput).normalized;

        // Apply force to make the player move
        GetComponent<Rigidbody>().AddForce(movement * rollSpeed);
    }
}
