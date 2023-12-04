using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartTheGame : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
        //int previousScore = PlayerPrefs.GetInt("CurrentScore", 0);

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("StartScreen");
    }
}
