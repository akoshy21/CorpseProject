using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // annamaria koshy

    public static LevelManager lm;

    public int lvlNum;
    public int par;
    public GameObject endScreen;

    [HideInInspector] public bool paused;
    public int curScn; //current scene
    public float timer;
    [HideInInspector] public bool lvlEnd = false; //current scene
    [HideInInspector] public int corpseCount1, corpseCount2, totalCorpses;
    [HideInInspector] public float curTime; // time since death
    [HideInInspector] public List<float> timeSD, timeSD1, timeSD2;

    bool end = false;
    int ind = 0, ind1 = 0, ind2 = 0;

    private void Awake()
    {
        if (lm != null && lm != this)
        {
            Destroy(this.gameObject);
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
        timeSD = new List<float>();
        timeSD.Add(0);
        timeSD1 = new List<float>();
        timeSD1.Add(0);
        timeSD2 = new List<float>();
        timeSD2.Add(0);
    }

    void Update()
    {
        if (!end)
        {
            curTime += Time.deltaTime;
            timeSD[ind] += Time.deltaTime;
            timeSD1[ind1] += Time.deltaTime;
            timeSD2[ind2] += Time.deltaTime;
        }

        if (lvlEnd)
        {
            Pause();
            //endScreen.SetActive(true);
            SetDeaths();
            SetLvl();
            GameManager.gm.lastScn = curScn;

            SceneManager.LoadScene("PileEndScene");
            lvlEnd = false;
            end = true;
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
        GameManager.gm.lvlP[lvlNum] = par;
    }

    void SetLvl()
    {
        GameManager.gm.lvlNum = lvlNum;
    }

    public void NewDeath(bool pOne)
    {
        if (pOne)
        {
            timeSD.Add(0);
            ind++;
            timeSD1.Add(0);
            ind1++;
        }
        else
        {
            timeSD.Add(0);
            ind++;
            timeSD2.Add(0);
            ind2++;
        }
    }

    void SetDeaths()
    {
        GameManager.gm.lvlC[lvlNum] = totalCorpses;
        GameManager.gm.lvlC1[lvlNum] = corpseCount1;
        GameManager.gm.lvlC2[lvlNum] = corpseCount2;
    }
}
