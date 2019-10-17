using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollManager : MonoBehaviour
{
    public bool dead;
    public static RagdollManager player;

    // Start is called before the first frame update
    void Start()
    {
        player = this;

        for (int i = 0; i < transform.childCount; ++i)
        {
            //transform.GetChild(i).gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            print("For loop: " + transform.GetChild(i));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (dead)
        {
            for (int i = 0; i < transform.childCount; ++i)
            {
                //transform.GetChild(i).gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
                transform.GetChild(i).gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                transform.GetChild(i).gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
                print("For loop: " + transform.GetChild(i));

            }
        }
    }
}
