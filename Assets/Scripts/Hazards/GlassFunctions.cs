﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassFunctions : MonoBehaviour
{
    public float velocityCap; //The minimum speed you need to travel to break the glass
    private float velocityDivide; //Part of crashSlowSpeed calculations
    private float crashSlowSpeed; //Force added to body when it breaks through the glass
    public GameObject glassCol;
    private float collisionSpeed; //Just logging the speed of the gameobject
    public SpriteRenderer glassRenderer;

    private ParticleSystem shatterParticles;

    private bool shattered;
    // Start is called before the first frame update
    
    //Luke Brockmann
    
    void Start()
    {
        velocityDivide = 6f;
        shatterParticles = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Rigidbody2D>() != null)
        {
            collisionSpeed = other.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude;
            crashSlowSpeed = other.gameObject.GetComponent<Rigidbody2D>().velocity.x / velocityDivide;



            if (collisionSpeed > velocityCap && !shattered)
            {
//                Debug.Log("Shatter");
                Shatter();
                other.gameObject.GetComponent<Rigidbody2D>().AddForceAtPosition
                    (new Vector2(-crashSlowSpeed, 0.5f), new Vector2(0f, 7f), ForceMode2D.Impulse);
                
                if (other.gameObject.CompareTag("Player"))
                {
                    PlayerController controller = other.transform.parent.gameObject.GetComponentInChildren<PlayerController>();
                    controller.Die();
                }

                if (other.GetComponent<Rigidbody2D>().velocity.x < 0)
                {
                    Vector3 tempTf = shatterParticles.transform.localScale;
                    tempTf.y *= -1;
                    shatterParticles.transform.localScale = tempTf;
                }
            }

        }
    }

    public void Shatter()
    {
        shattered = true;
        glassCol.SetActive(false);
        
        //CARSEN CAN DO SOME COOL SHIT HERE WITH THE PARTICLE EFFECTS
        //^ hey thats me
        glassRenderer.color = Color.clear;
        shatterParticles.Play();
        
    }
}