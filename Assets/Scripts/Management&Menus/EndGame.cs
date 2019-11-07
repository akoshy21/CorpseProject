 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    //annamaria koshy

    public GameObject bodyPrefabOne, bodyPrefabTwo, pOneSpawn, pTwoSpawn;
    [Space(10)]
    public TextMeshProUGUI d1;
    public TextMeshProUGUI d2, dTot, safety, safetyOne, safetyTwo;

    int bodOne, bodTwo; // bodycount 1, bodycount 2
    public int dCOne, dCTwo; // deathcount 1, deathcount 2

    private void Start()
    {
        Time.timeScale = 1f;
        dCOne = listSum(GameManager.gm.lvlC1);
        dCTwo = listSum(GameManager.gm.lvlC2);
        InvokeRepeating("NewBody", 0, 0.5f);

        UpdateText();
    }

    float listSum(List<float> li)
    {
        float sum = 0;
        for (int i = 0; i < li.Count; i++)
        {
            sum += li[i];
        }
        return sum;
    }

    int listSum(List<int> li)
    {
        int sum =0;
        for (int i = 0; i < li.Count; i++)
        {
            sum += li[i];
        }
        return sum;
    }

    void UpdateText()
    {
        d1.text = dCOne.ToString();
        d2.text = dCTwo.ToString();
        dTot.text = (dCOne + dCTwo).ToString();

        safety.text = CalculateTotSafety(GameManager.gm.lvlSR);
        safetyOne.text = CalculateTotSafety(GameManager.gm.lvlSR1);
        safetyTwo.text = CalculateTotSafety(GameManager.gm.lvlSR2);
    }

    float CalcAvg(List<float> li)
    {
        float temp, sum;

        sum = listSum(li);
        temp = sum / li.Count;

        return temp;
    }


    string CalculateTotSafety(List<float>sr)
    {
        float temp = 0;

        float sum = listSum(sr);

        temp = sum / sr.Count;

        if (temp > 100)
        {
            return "S";
        }
        else if (temp <= 100 && temp >= 90)
        {
            return "A";
        }
        else if (temp < 90 && temp >= 80)
        {
            return "B";
        }
        else if (temp < 80 && temp >= 70)
        {
            return "C";
        }
        else if (temp < 70 && temp >= 60)
        {
            return "D";
        }
        else if (temp < 60 && temp >= 50)
        {
            return "F";
        }
        return "F-";
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

    public void PlayAgain()
    {
        Destroy(GameManager.gm.gameObject);
        SceneManager.LoadScene("MainMenu");
    }
}
