﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WreckBallScriptLuke : MonoBehaviour
{

    public Rigidbody2D rigbod;
    public float killPower;
    public float rePushx;
    public float rePushy;
    public float speedCap;
    public float killSpeed;
    private Vector2 vel;
    public float normalPush;
    public GameObject ball;

    public Transform xPos;

    public AudioSource audiosource;

    public AudioClip chainclip;
    // Start is called before the first frame update
    void Start()
    {
        //rigbod.AddForce(new Vector2(initPush,0f), ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        vel = rigbod.velocity;
        if (vel.magnitude < speedCap)
        {
            
            audiosource.PlayOneShot(chainclip);
            if (transform.position.x > xPos.transform.position.x)
            {
                rigbod.AddForce(new Vector2(-rePushx,rePushy), ForceMode2D.Impulse);
            }
            
            if (transform.position.x < xPos.transform.position.x)
            {
                rigbod.AddForce(new Vector2(rePushx,rePushy), ForceMode2D.Impulse);
            }
           
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (vel.magnitude > killSpeed)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                
                PlayerController controller = other.transform.parent.gameObject.GetComponentInChildren<PlayerController>();
                if (controller != null  && !controller.dead)
                {
                    controller.LaunchMirrored(gameObject, other, killPower);
//                    controller.Die();
                    controller.Explode();
                }
            }

            if (other.gameObject.CompareTag("PlayerTwo"))
            {
                //Kill Player 2
            }
        }

    }

    
}
