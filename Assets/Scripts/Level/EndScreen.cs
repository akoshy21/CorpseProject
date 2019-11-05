 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    //annamaria koshy

    public GameObject bodyPrefab, pOneSpawn, pTwoSpawn;

    int bodOne, bodTwo; // bodycount 1, bodycount 2
    public int dCOne, dCTwo; // deathcount 1, deathcount 2

    private void Start()
    {
        Time.timeScale = 1f;
        dCOne = GameManager.gm.lvlC1[GameManager.gm.world, GameManager.gm.lvl];
        dCTwo = GameManager.gm.lvlC2[GameManager.gm.world, GameManager.gm.lvl];
        InvokeRepeating("NewBody", 0, 0.5f);
    }

    private void Update()
    {
    }

    void NewBody()
    {
       if(dCOne > bodOne)
        {
            Instantiate(bodyPrefab, pOneSpawn.transform);
            bodOne++;
        }
        if(dCTwo > bodTwo)
        {
            Instantiate(bodyPrefab, pTwoSpawn.transform);
            bodTwo++;
        }
    }
}
