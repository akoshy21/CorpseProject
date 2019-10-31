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
                    if (dir == Direction.Left)
                    {
                        pushedPlayer[i].GetComponent<Rigidbody2D>().AddForce(new Vector2(-pushForce, 0.5f), ForceMode2D.Impulse);
                    }
                    else if (dir == Direction.Right)
                    {
                        pushedPlayer[i].GetComponent<Rigidbody2D>().AddForce(new Vector2(pushForce, 0.5f), ForceMode2D.Impulse);
                    }
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
            pushing = false;
        }
    }
}
