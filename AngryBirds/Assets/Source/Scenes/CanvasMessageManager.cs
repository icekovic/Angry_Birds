using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasMessageManager : MonoBehaviour
{
    [SerializeField]
    private GameObject gameCompleted;

    [SerializeField]
    private GameObject levelCompleted;

    [SerializeField]
    private GameObject gameOver;

    private void Awake()
    {
        levelCompleted.SetActive(false);
        gameCompleted.SetActive(false);
        gameOver.SetActive(false);
    }

    public void ShowLevelCompletedMessage()
    {
        levelCompleted.SetActive(true);
    }

    public void ShowGameCompletedMessage()
    {
        gameCompleted.SetActive(true);
    }

    public void ShowGameOverMessage()
    {
        gameOver.SetActive(true);
    }

    public void CloseLevelCompletedMessage()
    {
        levelCompleted.SetActive(false);
    }

    public void CloseGameCompletedMessage()
    {
        gameCompleted.SetActive(false);
    }

    public void CloseGameOverMessage()
    {
        gameOver.SetActive(false);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene("SecondLevel");
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("FirstLevel");
    }
}
