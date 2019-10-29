using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class FanpushScript : MonoBehaviour
{
    public bool pushLeft;

    public bool pushRight;
    private bool pushing;
    public float ticker;
    private float tickerCap;
    private bool tick;
    public GameObject pushedPlayer;
    public float pushForce;
    // Start is called before the first frame update
    void Start()
    {
        tickerCap = 0.2f;
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

        if (ticker < tickerCap)
        {
            tick = false;
        }

        if (pushing)
        {
            if (pushLeft)
            {
                if (tick)
                {
                    pushedPlayer.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-pushForce,0.5f),ForceMode2D.Impulse);
                }
            }

            
            
            if (pushRight)
            {
                if (tick)
                {
                    pushedPlayer.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(pushForce,0.5f),ForceMode2D.Impulse);
                    Debug.Log("Pushing");
                }
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
            pushedPlayer = other.gameObject;

        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("PlayerOne")||other.CompareTag("PlayerTwo"))
        {
            
            pushing = false;
        }
    }
}
