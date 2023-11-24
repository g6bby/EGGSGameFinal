using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggCollect : MonoBehaviour
{
    public MonoBehaviour eggController;
    public GameObject collectUI;
    public GameObject triggerBox;

    private bool playerCollided = false;

    public string newTag = "EggCollected";

    void Start()
    {
        collectUI.SetActive(false);
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
            collectUI.SetActive(false);
            triggerBox.SetActive(false);

            GameObject.FindGameObjectWithTag("OtherEgg").tag = newTag;
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
