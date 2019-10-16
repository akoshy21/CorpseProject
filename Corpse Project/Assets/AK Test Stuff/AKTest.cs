using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AKTest : MonoBehaviour
{
    public enum Testing { Ragdoll, Spike, Head };

    public Testing obj;

    public static AKTest player;
    public bool dead;

    // Start is called before the first frame update
    void Start()
    {
        if (obj == Testing.Ragdoll)
        {
            player = this;

            for (int i = 0; i < transform.childCount; ++i)
            {
                //transform.GetChild(i).gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                print("For loop: " + transform.GetChild(i));
            }
        }
        else if (obj == Testing.Head)
        {
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (obj == Testing.Ragdoll)
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
            // quick and dirty movement test
            // transform.position += new Vector3(Input.GetAxis("Horizontal") * 0.5f, 0, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (obj == Testing.Spike)
        {
            Debug.Log("BOINK");
            AKTest.player.dead = true;
        }
    }
}
