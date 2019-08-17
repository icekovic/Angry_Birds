using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasMessageManager : MonoBehaviour
{
    [SerializeField]
    private GameObject gameCompleted;

    [SerializeField]
    private GameObject gameOver;

    [SerializeField]
    private GameObject hud;

    [SerializeField]
    private GameObject menu;

    private Text livesText;
    private int lives = 5;

    //private Button showMenuButton;

    private void Awake()
    {
        gameCompleted.SetActive(false);
        gameOver.SetActive(false);
        hud.SetActive(true);
        //menu.SetActive(false);
        livesText = GameObject.Find("NumbersOfLivesValue").GetComponent<Text>();
        //showMenuButton = GameObject.Find("ShowMenuButton").GetComponent<Button>();
    }

    private void Update()
    {
        livesText.text = lives.ToString();
    }

    public void ShowGameCompletedMessage()
    {
        gameCompleted.SetActive(true);
        CloseHud();
        //CloseMenu();
    }

    public void ShowGameOverMessage()
    {
        gameOver.SetActive(true);
        CloseHud();
        //CloseMenu();
    }

    public void CloseGameCompletedMessage()
    {
        gameCompleted.SetActive(false);
    }

    public void CloseGameOverMessage()
    {
        gameOver.SetActive(false);
    }

    //public void HideShowMenuButton()
    //{
    //    showMenuButton.gameObject.SetActive(false);
    //}

    //public void DisplayShowMenuButton()
    //{
    //    showMenuButton.gameObject.SetActive(true);
    //}

    public void CloseHud()
    {
        hud.SetActive(false);
    }

    //public void ShowMenu()
    //{
    //    menu.SetActive(true);
    //}

    //public void CloseMenu()
    //{
    //    menu.SetActive(false);
    //}

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
