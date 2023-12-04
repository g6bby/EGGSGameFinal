using System.Collections;
using UnityEngine;
using Cinemachine;

public class FOVChange : MonoBehaviour
{
    public CinemachineFreeLook freelookCam;
    private float minFOV = 40f;
    private float maxFOV = 90f;
    private float currentFOV;
    private float fovChangeSpeed = 2f;
    private float softcapValue = 35f;
    private int currentEggscore = 0;

    private bool keyPressed = false;

    void Update()
    {
        if (keyPressed && Input.GetKeyDown(KeyCode.E))
        {
            currentEggscore++;
            StartCoroutine(IncreaseFOVOverTime());
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trigger"))
        {
            keyPressed = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Trigger"))
        {
            keyPressed = false;
        }
    }

    IEnumerator IncreaseFOVOverTime()
    {
        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            float camerafov_interpolator = Mathf.Min(currentEggscore / softcapValue, 1.0f);
            currentFOV = Mathf.Lerp(minFOV, maxFOV, camerafov_interpolator);
            freelookCam.m_Lens.FieldOfView = currentFOV;

            elapsedTime += Time.deltaTime * fovChangeSpeed;
            keyPressed = false;
            yield return null;
        }

        //currentFOV = maxFOV;
        //freelookCam.m_Lens.FieldOfView = currentFOV;

        // Reset the keyPressed variable
    }
}
