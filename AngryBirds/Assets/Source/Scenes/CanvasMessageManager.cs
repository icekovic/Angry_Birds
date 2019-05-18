﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasMessageManager : MonoBehaviour
{
    [SerializeField]
    private GameObject gameCompleted;

    [SerializeField]
    private GameObject levelCompleted;

    [SerializeField]
    private GameObject gameOver;

    [SerializeField]
    private GameObject hud;

    private Text livesText;
    private int lives = 5;

    private void Awake()
    {
        levelCompleted.SetActive(false);
        gameCompleted.SetActive(false);
        gameOver.SetActive(false);
        hud.SetActive(true);
        livesText = GameObject.Find("NumbersOfLivesValue").GetComponent<Text>();
    }

    private void Update()
    {
        livesText.text = lives.ToString();
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

    public void CloseHud()
    {
        hud.SetActive(false);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene("SecondLevel");
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("FirstLevel");
    }

    public void TakeOneLife()
    {
        lives--;
    }

    public int GetNumberOfLives()
    {
        return lives;
    }
}
