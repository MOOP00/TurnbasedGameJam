using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RamanMainMenu : MonoBehaviour
{
    public void PlayerGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void Quit()
    {
         Application.Quit();
    }




}

