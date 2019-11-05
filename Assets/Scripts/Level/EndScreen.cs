 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndScreen : MonoBehaviour
{
    //annamaria koshy

    public GameObject bodyPrefab, pOneSpawn, pTwoSpawn;
    [Space(10)]
    public TextMeshProUGUI d1;
    public TextMeshProUGUI d2, dTot;

    int bodOne, bodTwo; // bodycount 1, bodycount 2
    public int dCOne, dCTwo; // deathcount 1, deathcount 2

    private void Start()
    {
        Time.timeScale = 1f;
        dCOne = GameManager.gm.lvlC1[GameManager.gm.world, GameManager.gm.lvl];
        dCTwo = GameManager.gm.lvlC2[GameManager.gm.world, GameManager.gm.lvl];
        InvokeRepeating("NewBody", 0, 0.5f);

        d1.text = dCOne.ToString();
        d2.text = dCTwo.ToString();
        dTot.text = (dCOne + dCTwo).ToString();
    }

    private void Update()
    {
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
            Instantiate(bodyPrefab, pOneSpawn.transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
            bodOne++;
        }
        if(dCTwo > bodTwo)
        {
            Instantiate(bodyPrefab, pTwoSpawn.transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
            bodTwo++;
        }
    }
}
