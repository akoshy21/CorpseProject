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
        LevelManager.lm.paused = false;
        pauseScreen.SetActive(false);
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

        if (LevelManager.lm.paused)
        {
            if (Input.GetButtonDown("Triangle"))
            {
                ReloadScene();
            }
            if (Input.GetButtonDown("Circle"))
            {
                LoadMain();
            }
        }
    }

    public void LoadMain()
    {
        LevelManager.lm.paused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        Destroy(LevelManager.lm.gameObject);
    }

    public void ReloadScene()
    {
        LevelManager.lm.paused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(LevelManager.lm.curScn);
        Destroy(LevelManager.lm.gameObject);
    }
}
