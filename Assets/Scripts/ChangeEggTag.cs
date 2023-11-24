using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTagOnKeyPress : MonoBehaviour
{
    public string newTag = "EggCollected";

    void Update()
    {
        // Check if the "E" key is pressed
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Change the tag to the specified newTag
            ChangeObjectTag();
        }
    }

    void ChangeObjectTag()
    {
        // Change the tag of the GameObject to the newTag
        gameObject.tag = newTag;

        // Optionally, you can print a message to the console
        Debug.Log("Tag changed to: " + newTag);
    }
}
