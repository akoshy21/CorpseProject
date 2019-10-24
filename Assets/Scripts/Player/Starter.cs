using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starter : MonoBehaviour
{
    public static Starter start;
    public GameObject PlayerPrefab;
    public int bodyCount = 1;

    // Start is called before the first frame update
    void Awake()
    {
        start = this;
    }

    public void newChild()
    {
		bodyCount++;
		Instantiate(PlayerPrefab, transform.position, PlayerPrefab.transform.rotation);
//		PlayerController.dead = false;
    }
}
