using System.Collections;
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
    public float timer;
    [HideInInspector] public bool lvlEnd = false; //current scene
    [HideInInspector] public int corpseCount1, corpseCount2, totalCorpses;
    [HideInInspector] public float curTime, timeSD; // time since death
    [HideInInspector] public List<float> timeSD1, timeSD2;

    int ind1 = 0, ind2 = 0;

    private void Awake()
    {
        if (lm != null && lm != this)
        {
            Destroy(this);
        }
        else
        {
            lm = this;
            DontDestroyOnLoad(this);
        }
    }

    void Start()
    {
        SetPar();
        Time.timeScale = 1f;
        curScn = SceneManager.GetActiveScene().buildIndex;

        timeSD1 = new List<float>();
        timeSD1.Add(0);
        timeSD2 = new List<float>();
        timeSD2.Add(0);
    }

    void Update()
    {
        curTime += Time.deltaTime;
        timeSD += Time.deltaTime;
        timeSD1[ind1] += Time.deltaTime;
        timeSD2[ind2] += Time.deltaTime;

        if (lvlEnd)
        {
            Pause();
            //endScreen.SetActive(true);
            SetDeaths();
            SetLvl();
            GameManager.gm.lastScn = curScn;

            SceneManager.LoadScene("PileEndScene");
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

    void SetLvl()
    {
        switch (curLvl)
        {
            case Level.WorldOne_1:
                GameManager.gm.lvl = 0;
                break;
            case Level.WorldOne_2:
                GameManager.gm.lvl = 1;
                break;
            case Level.WorldOne_3:
                GameManager.gm.lvl = 2;
                break;
            default:
                break;
        }
    }

    public void NewDeath(bool pOne)
    {
        timeSD = 0;
        if (pOne)
        {
            timeSD1.Add(0);
            ind1++;
        }
        else
        {
            timeSD2.Add(0);
            ind2++;
        }
    }

    void SetDeaths()
    {
        switch (curLvl)
        {
            case Level.WorldOne_1:
                GameManager.gm.lvlC[0, 0] = totalCorpses;
                GameManager.gm.lvlC1[0,0] = corpseCount1;
                GameManager.gm.lvlC2[0, 0] = corpseCount2;
                break;
            case Level.WorldOne_2:
                GameManager.gm.lvlC[0, 1] = totalCorpses;
                GameManager.gm.lvlC1[0, 1] = corpseCount1;
                GameManager.gm.lvlC2[0, 1] = corpseCount2;
                break;
            case Level.WorldOne_3:
                GameManager.gm.lvlC[0, 2] = totalCorpses;
                GameManager.gm.lvlC1[0, 2] = corpseCount1;
                GameManager.gm.lvlC2[0, 2] = corpseCount2;
                break;
            default:
                break;
        }
    }
}
