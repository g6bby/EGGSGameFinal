using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggController : MonoBehaviour
{
    [SerializeField]
    private float rollSpeed = 12f;

    void Start()
    {
        Cursor.visible = false; 
        Cursor.lockState = CursorLockMode.Locked;
    }


    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;

        cameraForward.y = 0;
        cameraRight.y = 0;

        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 movement = (cameraForward * verticalInput + cameraRight * horizontalInput).normalized;

        GetComponent<Rigidbody>().AddForce(movement * rollSpeed);
    }


}
