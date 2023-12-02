using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class EggCollect : MonoBehaviour
{
    public MonoBehaviour eggController;
    public GameObject collectUI;
    public GameObject triggerBox;

    public AudioSource audioSource;

    //public CinemachineFreeLook freelookCam;
    //private float camFOV = 40f;

    private bool playerCollided = false;

    public string newTag = "EggCollected";

    void Start()
    {
        collectUI.SetActive(false);
        audioSource.enabled = false;
    }

    void EnableScript()
    {
        if (eggController != null && !eggController.enabled)
        {
            eggController.enabled = true;
            Debug.Log("Script Enabled on Other Object");
        }
    }

    void Update()
    {
        if (playerCollided && Input.GetKeyDown(KeyCode.E))
        {  
            //camFOV += 8f;
            //freelookCam.m_Lens.FieldOfView = camFOV;

            audioSource.enabled = true;
            audioSource.Play();

            collectUI.SetActive(false);
            triggerBox.SetActive(false);

            //GameObject.FindGameObjectWithTag("OtherEgg").tag = newTag;

            Debug.Log("Tag changed to: " + newTag);

            EnableScript();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("EggCollected"))
        {
            playerCollided = true;
            collectUI.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("EggCollected"))
        {
            playerCollided = false;
            collectUI.SetActive(false);
        }
    }

}
