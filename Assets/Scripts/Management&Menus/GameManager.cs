using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //akoshy

    [HideInInspector] public int totalCorpses, p1Corpses, p2Corpses;
    // p - par, c - corpses; one = world one, 1 = lvl 1
    [HideInInspector] public int[,] lvlC, lvlC1, lvlC2, lvlP;
    [HideInInspector] public int world, lvl, lastScn;

    public GameObject splatter;
    public static GameManager gm;
    public int worldCount, lvlPWorld;
    public Material surf;

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
        SetUpParCorpseCount();
    }
    private void Start()
    {
    }

    void SetUpParCorpseCount()
    {
        // assuming rn there are at max five lvls per world; there's probably a better way of doing this.

        lvlC = new int[worldCount, 5];
        lvlC1 = new int[worldCount, 5];
        lvlC2 = new int[worldCount, 5];
        lvlP = new int[worldCount, 5];
    }

    void SafetyRating()
    {
        //float temp, tot;

        //for (int i = 0; i < lvlCount; i++)
        //{
            
        //}
    }

    public void InstantiateSplatter(Collider2D player, Collider2D surface)
    {
        Vector3 ptOne = surface.ClosestPoint(player.transform.position);
        Vector3 ptTwo = player.ClosestPoint(ptOne);

        if (player.gameObject.CompareTag("Player"))
        {
            for (int i = 0; i < player.transform.parent.childCount; i++)
            {
                if (player.transform.parent.GetChild(i).GetComponent<SpriteRenderer>() != null)
                {
                    player.transform.parent.GetChild(i).GetComponent<SpriteRenderer>().material = surf;
                }
            }
            player.GetComponent<SpriteRenderer>().material = surf;
        }

        Instantiate(splatter, ptOne, Quaternion.Euler(0, 0, Random.Range(0,360)), surface.transform);
        Instantiate(splatter, ptTwo, Quaternion.Euler(0, 0, Random.Range(0, 360)), player.transform);
    }
}
