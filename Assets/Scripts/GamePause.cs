using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePause : MonoBehaviour
{
    public GameObject menupause;
    bool paused = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PauseGame(bool state)
    {
        paused = state;
        if (state)
        {
            Time.timeScale = 0.00001f;
            menupause.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Time.timeScale = 1f;
            menupause.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void ExitLevel()
    {
        PauseGame(false);
        Cursor.lockState = CursorLockMode.None;
        Loading.LoadLevel("Menu");
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame(!paused);

        }
    }
}
