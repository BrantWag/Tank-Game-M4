using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwithc : MonoBehaviour
{
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        Debug.Log("Exiting Application");
        Application.Quit();
    }
}
