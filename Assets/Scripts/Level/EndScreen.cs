 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    //annamaria koshy

    public GameObject bodyPrefab, pOneSpawn, pTwoSpawn;

    int bodOne, bodTwo; // bodycount 1, bodycount 2
    int dCOne, dCTwo; // deathcount 1, deathcount 2

    private void Start()
    {
        bodyPrefab.GetComponent<PlayerController>().dead = true;
        InvokeRepeating("NewBody", 0, 0.5f);
    }

    private void Update()
    {;
    }

    void NewBody()
    {
       if(dCOne > bodOne)
        {
            Instantiate(bodyPrefab, pOneSpawn.transform);
        }
        else if(dCTwo > bodTwo)
        {
            Instantiate(bodyPrefab, pTwoSpawn.transform);
        }
    }
}
