using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starter : MonoBehaviour
{
    // Annamaria Koshy

    //public static Starter start;
    public GameObject PlayerPrefab;
    public bool SpawnOnStart;
    public float spawnDelay = 1;
    

    private bool spawning;

    // Start is called before the first frame update
    void Awake()
    {
        if (SpawnOnStart)
        {
            newChild();
        }
    }

    public void newChild()
    {
        if (!spawning)
        {
            StartCoroutine(SpawnDelay());
        }
    }

    //Spawning on delay by Carsen Decker
    IEnumerator SpawnDelay()
    {
        spawning = true;
        
        yield return new WaitForSeconds(spawnDelay);
        
        Instantiate(PlayerPrefab, transform.position, PlayerPrefab.transform.rotation);

        spawning = false;
    }
}
