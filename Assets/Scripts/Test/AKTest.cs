using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AKTest : MonoBehaviour
{
    // Annamaria Koshy

    public enum Testing { Ragdoll, Spike, Legs, Aim, Controller };

    public Testing obj;

    bool collide;

    Quaternion originalRotation;
    public static AKTest player;
    public bool dead, restoreRotation, start;
    public float rotateSpeed;

    bool onGround;

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
        else if (obj == Testing.Legs)
        {
            originalRotation = this.transform.rotation;
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
        else if (obj == Testing.Legs)
        {
            if (transform.rotation != originalRotation)
            {
                restoreRotation = true;
            }

            if (restoreRotation && onGround)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, originalRotation, Time.time * rotateSpeed);
                //Debug.Log("Rotating " + transform.rotation.eulerAngles);
                if (Mathf.Abs(Quaternion.Angle(originalRotation, transform.rotation)) <= 2)
                {
                    restoreRotation = false;
                }
            }
        }
        else if (obj == Testing.Aim)
        {
            Vector3 dir = Input.mousePosition - this.transform.position;

            //Vector3 x = Input.mousePosition;
            //x.z = 0;
            //transform.position = x;
        }
        else if (obj == Testing.Controller)
        {
            if (Input.GetButtonDown("P1Jump"))
            {
                Debug.Log("FIRE");
            }

            Debug.Log("HorInput: " + Input.GetAxis("P2Horizontal"));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (obj == Testing.Spike && collision.gameObject.CompareTag("Player"))
        {
            PlayerController controller = collision.transform.parent.gameObject.GetComponentInChildren<PlayerController>();
 
            if (controller != null && !controller.dead)
            {
                Debug.Log("BOINK: " + collision.gameObject.name);
                
                controller.LaunchMirrored(gameObject, collision, 10000f);
                controller.Die();
            }
        }
        
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (obj == Testing.Legs)
            {
                onGround = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
//        collide = true;

        if (collision.gameObject.CompareTag("Ground"))
        {
            if (obj == Testing.Legs)
            {
                onGround = false;
            }
        }
    }
}
