using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggCollect : MonoBehaviour
{
    public MonoBehaviour eggController;

    private bool playerCollided = false;

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
            EnableScript();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerCollided = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerCollided = false;
        }
    }
}
