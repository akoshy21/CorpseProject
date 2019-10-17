using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollManager : MonoBehaviour
{
    public bool dead;
    public static RagdollManager body;
    public GameObject newBod;
    public Transform start;

    public bool dying = true;

    // Start is called before the first frame update
    void Start()
    {
        body = this;
        Starter.start.delay = false;

        for (int i = 0; i < transform.childCount; ++i)
        {
            //transform.GetChild(i).gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            print("For loop: " + transform.GetChild(i));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (dead && dying)
        {
            dying = false;
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).gameObject.GetComponent<PlayerController>() != null)
                {
                    Destroy(transform.GetChild(i).gameObject.GetComponent<PlayerController>());
                    Debug.Log("DESTROYING " + transform.GetChild(i));
                }
                transform.GetChild(i).gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                body.transform.GetChild(i).gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
            }
            Starter.start.bodyCount--;
            body = null;
            Starter.start.newChild();
        }
    }
}
