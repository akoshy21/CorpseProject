using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // annamaria koshy

    public static GameManager gm;

    public GameObject endScreen;

    [HideInInspector] public bool paused;
 public int curScn; //current scene
    [HideInInspector] public bool lvlEnd = false; //current scene
    [HideInInspector] public int corpseCount1, corpseCount2, totalCorpses;

    private void Awake()
    {
        gm = this;
    }

    void Start()
    {
        Time.timeScale = 1f;
        curScn = SceneManager.GetActiveScene().buildIndex;
    }

    void Update()
    {
        if (lvlEnd)
        {
            Pause();
            endScreen.SetActive(true);
            lvlEnd = false;
        }
    }


    public void Pause()
    {
        Debug.Log("PAUSE");
        if (paused)
        {
            paused = false;
            Time.timeScale = 1f;
        }
        else
        {
            paused = true;
            Time.timeScale = 0f;
        }
    }
}
