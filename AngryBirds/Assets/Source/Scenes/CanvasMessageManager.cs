using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasMessageManager : MonoBehaviour
{
    [SerializeField]
    private GameObject gameCompleted;

    [SerializeField]
    private GameObject gameOver;

    private void Awake()
    {
        gameCompleted.SetActive(false);
        gameOver.SetActive(false);
    }

    public void ShowGameCompletedMessage()
    {
        gameCompleted.SetActive(true);
    }

    public void ShowGameOverMessage()
    {
        gameOver.SetActive(true);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("FirstLevel");
    }
}
