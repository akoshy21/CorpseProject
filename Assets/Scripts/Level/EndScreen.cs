 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndScreen : MonoBehaviour
{
    //annamaria koshy

    public GameObject bodyPrefabOne, bodyPrefabTwo, pOneSpawn, pTwoSpawn;
    [Space(10)]
    public TextMeshProUGUI d1;
    public TextMeshProUGUI d2, dTot, dPM, dPM1, dPM2, sDP, sDP1, sDP2, lDP, lDP1, lDP2, safety;

    int bodOne, bodTwo; // bodycount 1, bodycount 2
    public int dCOne, dCTwo; // deathcount 1, deathcount 2

    private void Start()
    {
        Time.timeScale = 1f;
        dCOne = GameManager.gm.lvlC1[GameManager.gm.world, GameManager.gm.lvl];
        dCTwo = GameManager.gm.lvlC2[GameManager.gm.world, GameManager.gm.lvl];
        InvokeRepeating("NewBody", 0, 0.5f);

        UpdateText();
    }

    private void Update()
    {
    }

    void UpdateText()
    {
        d1.text = dCOne.ToString();
        d2.text = dCTwo.ToString();
        dTot.text = (dCOne + dCTwo).ToString();

        if (dCOne + dCTwo != 0)
        {

            safety.text = "shit";
        }
        else
        {
            safety.text;
        }


        dPM.text = (GameManager.gm.lvlC[GameManager.gm.world, GameManager.gm.lvl]/LevelManager.lm.curTime).ToString("n2");
        dPM1.text = (GameManager.gm.lvlC1[GameManager.gm.world, GameManager.gm.lvl] / LevelManager.lm.curTime).ToString("n2");
        dPM2.text = (GameManager.gm.lvlC2[GameManager.gm.world, GameManager.gm.lvl] / LevelManager.lm.curTime).ToString("n2");

        sDP.text = ShortestTimeSD(LevelManager.lm.timeSD).ToString("n2");
        sDP1.text = ShortestTimeSD(LevelManager.lm.timeSD1).ToString("n2");
        sDP2.text = ShortestTimeSD(LevelManager.lm.timeSD2).ToString("n2");

        lDP.text = LongestTimeSD(LevelManager.lm.timeSD).ToString("n2");
        lDP1.text = LongestTimeSD(LevelManager.lm.timeSD1).ToString("n2");
        lDP2.text = LongestTimeSD(LevelManager.lm.timeSD2).ToString("n2");
    }

    int CalculatePars()
    {
        // S = 0, A = 1, B = 2, C = 3, D = 4, F = 5
        float temp = LevelManager.lm.par / (dCOne + dCTwo)) * 100;

        if (temp >= 100)
        {
            return 0;
        }
    }

    float LongestTimeSD(List<float> l)
    {
        float temp = 0;

        if(l == null)
            return 0;
        for (int i = 0; i < l.Count; i++)
        {
            if (temp < l[i])
            {
                temp = l[i];
            }
        }
        return temp;
    }

    float ShortestTimeSD(List<float> l)
    {
        float temp;

        if (l == null)
            return 0;

        temp = l[0];
        for (int i = 0; i < l.Count; i++)
        {
            if (temp > l[i])
            {
                temp = l[i];
            }
        }
        return temp;
    }

    void NewBody()
    {
       if(dCOne > bodOne)
        {
            Instantiate(bodyPrefabOne, pOneSpawn.transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
            bodOne++;
        }
        if(dCTwo > bodTwo)
        {
            Instantiate(bodyPrefabTwo, pTwoSpawn.transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
            bodTwo++;
        }
    }
}
