using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void startARGame()
    {
        GameController.currentControlType = ControlType.AR;
        loadScene();
    }

    public void startVRGame()
    {
        GameController.currentControlType = ControlType.VR;
        loadScene();
    }

    void loadScene()
    {
        SceneManager.LoadScene("FirstLevel");
    }
}