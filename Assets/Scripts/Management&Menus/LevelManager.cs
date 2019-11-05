﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // annamaria koshy

    public static LevelManager lm;

    public enum Level { WorldOne_1, WorldOne_2, WorldOne_3}
    public Level curLvl;
    public int par;
    public GameObject endScreen;

    [HideInInspector] public bool paused;
 public int curScn; //current scene
    [HideInInspector] public bool lvlEnd = false; //current scene
    [HideInInspector] public int corpseCount1, corpseCount2, totalCorpses;

    private void Awake()
    {
        lm = this;
    }

    void Start()
    {
        SetPar();
        Time.timeScale = 1f;
        curScn = SceneManager.GetActiveScene().buildIndex;
    }

    void Update()
    {
        if (lvlEnd)
        {
            Pause();
            endScreen.SetActive(true);
            SetDeaths();
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

    void SetPar()
    {
        switch (curLvl)
        {
            case Level.WorldOne_1:
                GameManager.gm.lvlP[0,0] = par;
                break;
            case Level.WorldOne_2:
                GameManager.gm.lvlP[0,1] = par;
                break;
            case Level.WorldOne_3:
                GameManager.gm.lvlP[0,2] = par;
                break;
            default:
                break;
        }
    }

    void SetDeaths()
    {
        switch (curLvl)
        {
            case Level.WorldOne_1:
                GameManager.gm.lvlC[0, 0] = totalCorpses;
                break;
            case Level.WorldOne_2:
                GameManager.gm.lvlC[0, 1] = totalCorpses;
                break;
            case Level.WorldOne_3:
                GameManager.gm.lvlC[0, 2] = totalCorpses;
                break;
            default:
                break;
        }
    }
}