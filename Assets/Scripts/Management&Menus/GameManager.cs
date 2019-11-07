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
            Destroy(this.gameObject);
        }
        else
        {
            gm = this;
            DontDestroyOnLoad(this);
        }
        SetUpParCorpseCount();
    }
    private void Start()
    {
    }

    void SetUpParCorpseCount()
    {
        // assuming rn there are at max five lvls per world; there's probably a better way of doing this.

        lvlC = new int[worldCount, 8];
        lvlC1 = new int[worldCount, 8];
        lvlC2 = new int[worldCount, 8];
        lvlP = new int[worldCount, 8];
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

        GameObject tempSplat = Instantiate(splatter, ptOne, Quaternion.Euler(0, 0, Random.Range(0,360)));
        tempSplat.transform.parent = surface.transform;
        Instantiate(splatter, ptTwo, Quaternion.Euler(0, 0, Random.Range(0, 360)), player.transform);
    }

    public void InstantiateSplatter(Collider2D player, Collider2D surface, bool ground)
    {
        InstantiateSplatter(player, surface);
        Vector3 pt = player.ClosestPoint(surface.ClosestPoint(player.transform.position));
        GameObject closestGround = FindClosestGround(player.gameObject);
        Vector3 newPt = closestGround.GetComponent<Collider2D>().ClosestPoint(pt);

        Instantiate(splatter, newPt, Quaternion.Euler(0, 0, Random.Range(0, 360)));
    }

    public GameObject FindClosestGround(GameObject obj)
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Ground");
        GameObject closest = null;
        float distance = 1;
        Vector3 position = obj.transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
}
