using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Menu : MonoBehaviour
{
    public void PlayGame()
    {
        Loading.LoadLevel("MainScene");
        //SceneManager.LoadScene("MainScene");
    }

    public void QualityChange()
    {
        QualitySettings.SetQualityLevel(0);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
