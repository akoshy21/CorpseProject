using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class FanpushScript : MonoBehaviour
{
    // written by luke, edited by annamaria

    public enum Direction { Left, Right, Off };
    public Direction dir;

    private bool pushing;
    public float ticker;
    private float tickerCap;
    private bool tick;
    public List<GameObject> pushedPlayer;
    public float pushForce;
    // Start is called before the first frame update
    void Start()
    {
        tickerCap = 0.2f;
        pushedPlayer = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        ticker = ticker + 1f * Time.deltaTime;
        if (ticker > tickerCap)
        {
            ticker = 0;
            tick = true;
            Debug.Log("push");
        }

        if (pushing)
        {
            if (tick)
            {
                //Debug.Log("tickin");

                for (int i = 0; i < pushedPlayer.Count; i++)
                {
                    Push(pushedPlayer[i]);
                }

                tick = false;
            }
        }
    }
    

    private void OnTriggerStay2D(Collider2D other)
    {
       /* if (pushLeft)
        {
            if (tick)
            {
                //other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-pushForce,0.5f),ForceMode2D.Impulse);
            }
        }

            
            
        if (pushRight)
        {
            if (tick)
            {
                other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(pushForce,0.5f),ForceMode2D.Impulse);
                Debug.Log("Pushing");
            }
        }*/
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            pushing = true;
            Debug.Log("Trigger Enter Procced");
            pushedPlayer.Add(other.gameObject);
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            pushedPlayer.Remove(other.gameObject);
            Push(other.gameObject, pushForce * 2);
            pushing = false;
        }
    }

    void Push(GameObject go)
    {
        Push(go, pushForce);
    }

    void Push(GameObject go, float speed)
    {
        if (dir == Direction.Left)
        {
            go.GetComponent<Rigidbody2D>().AddForce(-transform.parent.parent.right * speed, ForceMode2D.Impulse);
        }
        else if (dir == Direction.Right)
        {
            go.GetComponent<Rigidbody2D>().AddForce(transform.parent.parent.right * speed, ForceMode2D.Impulse);
        }
    }
}
