using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTagOnKeyPress : MonoBehaviour
{
    public string newTag = "EggCollected";

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ChangeObjectTag();
        }
    }

    void ChangeObjectTag()
    {
        gameObject.tag = newTag;

        Debug.Log("Tag changed to: " + newTag);
    }
}
