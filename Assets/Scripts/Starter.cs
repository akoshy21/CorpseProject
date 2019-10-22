using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starter : MonoBehaviour
{
    public static Starter start;
    public GameObject prefab;
    public bool delay;
    public int bodyCount = 1;

    // Start is called before the first frame update
    void Awake()
    {
        start = this;
    }

    public void newChild()
    {
		bodyCount++;
		Instantiate(prefab, transform.position, prefab.transform.rotation);
//		PlayerController.dead = false;
    }
}
