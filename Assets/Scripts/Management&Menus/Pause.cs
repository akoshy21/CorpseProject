using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    // AK
    public GameObject pauseScreen;

    private void Start()
    {
        // idk if we wanna have a pausemenu tag or smth
        //pauseScreen = GameObject.FindGameObjectWithTag("Pause");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Pause"))
        {
            if (pauseScreen.activeSelf)
            {
                LevelManager.lm.paused = false;
                pauseScreen.SetActive(false);
                Time.timeScale = 1f;
            }
            else {
                LevelManager.lm.paused = true;
                pauseScreen.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }

    public void LoadMain()
    {
        Time.timeScale = 1f;
        LevelManager.lm.paused = false;
        SceneManager.LoadScene("MainMenu");
    }

    public void ReloadScene()
    {
        Time.timeScale = 1f;
        LevelManager.lm.paused = false;
        SceneManager.LoadScene(LevelManager.lm.curScn);
    }
}
