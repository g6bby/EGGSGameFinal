using System.Collections;
using UnityEngine;
using Cinemachine;

public class FOVChange : MonoBehaviour
{
    public CinemachineFreeLook freelookCam;
    private float minFOV = 40f;
    private float maxFOV = 90f;
    private float currentFOV;
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
        float t = 0f;
        float startFOV = freelookCam.m_Lens.FieldOfView;
        float targetFOV = Mathf.Lerp(minFOV, maxFOV, Mathf.Min(currentEggscore / softcapValue, 1.0f));

        while (t < 1f)
        {
            float lerpedFOV = Mathf.Lerp(startFOV, targetFOV, t);
            freelookCam.m_Lens.FieldOfView = lerpedFOV;

            t += Time.deltaTime;
            keyPressed = false;
            yield return null;
        }

        freelookCam.m_Lens.FieldOfView = targetFOV;
    }
}
