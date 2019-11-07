using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWall : MonoBehaviour
{
    /*
     * @author Kate Howell
     */

    public Rigidbody2D rigbod;
    public GameObject platform;
    public Transform topBound;
    public Transform bottomBound;

    public float moveSpeedUp;
    public float moveSpeedSide;

    public bool turnOn; //public bool just to remotely turn platform on
    public bool buttonNeeded;

    private bool moveDown;
    private bool moveUp;

    public GameObject buttonOrLever;
    public GameObject buttonOrLever2;


    // Start is called before the first frame update
    void Start()
    {
        moveUp = true;
    }

    // Update is called once per frame
    void Update()
    {
        bool active = false;

        if (buttonOrLever != null)
        {
            if (buttonOrLever.GetComponent<ButtonScript>().buttonActive == true)
            {
                active = true;
            }
        }

        if (buttonOrLever2 != null)
        {
            if (buttonOrLever2.GetComponent<ButtonScript>().buttonActive == true)
            {
                active = true;
            }
        }

        if (buttonOrLever.GetComponent<ButtonScript>().buttonActive == false)
        {
            rigbod.velocity = new Vector2(0f, 0f);
        }
        if (active)
        {
            if (turnOn)
            {
                if (platform.transform.position.y > topBound.transform.position.y)
                {
                    moveUp = false;

                }
                else
                {
                    moveUp = true;
                }

                if (moveUp)
                {
                    rigbod.velocity = new Vector2(0f, moveSpeedUp);
                }
                else
                {
                    rigbod.velocity = new Vector2(0f, 0f);
                }
            }
            
        }
        else
        {
            if (platform.transform.position.y > bottomBound.transform.position.y)
            {
                moveUp = false;
                moveDown = true;
            }
            else
            {
                moveDown = false;
            }

            if (moveDown)
            {
                rigbod.velocity = (new Vector2(0f, -moveSpeedUp));
            }
            else
            {
                rigbod.velocity = new Vector2(0f, 0f);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController controller = other.transform.parent.gameObject.GetComponentInChildren<PlayerController>();
            if (controller != null && !controller.dead)
            {
                controller.Explode();
            }
        }
    }
}
