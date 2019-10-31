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
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButton("Pause"))
        {
            if (pauseScreen.activeSelf)
            {
                GameManager.gm.paused = false;
                pauseScreen.SetActive(false);
                Time.timeScale = 1f;
            }
            else {
                GameManager.gm.paused = true;
                pauseScreen.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }

    public void LoadMain()
    {
        Time.timeScale = 1f;
        GameManager.gm.paused = false;
        SceneManager.LoadScene("MainMenu");
    }

    public void ReloadScene()
    {
        Time.timeScale = 1f;
        GameManager.gm.paused = false;
        SceneManager.LoadScene(GameManager.gm.curScn);
    }
}
