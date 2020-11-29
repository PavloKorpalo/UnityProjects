using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public void GotoMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void PauseButton()
    {
        TankEngine.Pause = true;
    }

    public void ResumeButton()
    {
        TankEngine.Pause = false;
    }
}
