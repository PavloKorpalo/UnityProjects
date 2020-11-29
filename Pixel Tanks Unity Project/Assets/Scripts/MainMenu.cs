using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGameMedium()
    {
        SpawnScript.ChoosedTank = (int)ChosedTank.Medium;

        SceneManager.LoadScene(1);
    }
    public void PlayGameArtillery()
    {
        SpawnScript.ChoosedTank = (int)ChosedTank.Artillery;

        SceneManager.LoadScene(1);
    }
    public void PlayGameLight()
    {
        SpawnScript.ChoosedTank = (int)ChosedTank.Light;

        SceneManager.LoadScene(1);
    }

    public void GameQuit()
    {
        Debug.Log("Game Quit!");

        Application.Quit();
    }
}
