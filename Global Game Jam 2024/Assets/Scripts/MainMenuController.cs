using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject howToPanel;
    public GameObject creditsPanel;

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void HowTo()
    {
        howToPanel.SetActive(!howToPanel.activeSelf);
    }

    public void Credits()
    {
        creditsPanel.SetActive(!creditsPanel.activeSelf);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
