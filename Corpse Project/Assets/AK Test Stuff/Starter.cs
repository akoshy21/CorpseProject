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
    void Start()
    {
        start = this;
    }

    public void newChild()
    {
        if (!delay && bodyCount < 1)
        {
            bodyCount++;
            Instantiate(prefab);
            PlayerController.dead = false;
        }
    }
}
