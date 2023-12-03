using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;
using UnityEngine.SceneManagement;

public class EggCollect : MonoBehaviour
{
    public MonoBehaviour eggController;
    public GameObject collectUI;
    public GameObject triggerBox;

    public AudioSource audioSource;

    public TextMeshProUGUI scoreText;
    private static int currentScore = 0;
    private static int maxScore = 35;

    public CinemachineFreeLook freelookCam;
    private float camFOV = 40f;

    private bool playerCollided = false;

    public string newTag = "EggCollected";

    public Animator fadeToBlack;

    void Start()
    {
        collectUI.SetActive(false);
        audioSource.enabled = false;
        fadeToBlack.enabled = false;
    }

    void Update()
    {
        if (playerCollided && Input.GetKeyDown(KeyCode.E))
        {  
            EnableScript();
            HandleEggCollection();
            UpdateScoreText();
            ChangeFOVCam();
        }

        if (currentScore == 35)
        {
            fadeToBlack.enabled = true;
            StartCoroutine("ToEndScene");
        }
        
    }

    void EnableScript()
    {
        if (eggController != null && !eggController.enabled)
        {
            eggController.enabled = true;
            Debug.Log("Script Enabled on Other Object");
        }
    }

    void HandleEggCollection()
    {
        audioSource.enabled = true;
        audioSource.Play();

        collectUI.SetActive(false);
        triggerBox.SetActive(false);

        //GameObject.FindGameObjectWithTag("OtherEgg").tag = newTag;
        //Debug.Log("Tag changed to: " + newTag);
    }

    void UpdateScoreText()
    {
        //Debug.Log("Before score increment: " + currentScore);
        if (currentScore < maxScore)
        {
            //CHEAT //currentScore = 35;
            currentScore++;
            scoreText.text = $"{currentScore}/{maxScore}";
        }
        //Debug.Log("After score increment: " + currentScore);

    }

    void ChangeFOVCam()
    {
        camFOV += 8f;
        freelookCam.m_Lens.FieldOfView = camFOV;
    }

    IEnumerator ToEndScene()
    {
        yield return new WaitForSeconds(6f);
        
        currentScore = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("EndScene");
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
