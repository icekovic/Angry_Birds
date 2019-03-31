using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void startARGame()
    {
        Game.currentControlType = ControlType.AR;
        loadScene();
    }

    public void startVRGame()
    {
        Game.currentControlType = ControlType.VR;
        loadScene();
    }

    void loadScene()
    {
        SceneManager.LoadScene("Game");
    }
}