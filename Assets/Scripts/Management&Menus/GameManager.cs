using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //akoshy

    [HideInInspector] public int totalCorpses, p1Corpses, p2Corpses;
    // p - par, c - corpses; one = world one, 1 = lvl 1
    [HideInInspector] public int[,] lvlC, lvlP;

    public static GameManager gm;
    public int worldCount, lvlPWorld;

    private void Awake()
    {
        if (gm != null && gm != this)
        {
            Destroy(this);
        }
        else
        {
            gm = this;
        }
    }
    private void Start()
    {
        SetUpParCorpseCount();
    }

    void SetUpParCorpseCount()
    {
        // assuming rn there are at max five lvls per world; there's probably a better way of doing this.
        lvlC = new int[worldCount, 5];
        lvlP = new int[worldCount, 5];
    }

    void SafetyRating()
    {
        //float temp, tot;

        //for (int i = 0; i < lvlCount; i++)
        //{
            
        //}
    }
}
