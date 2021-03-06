﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Lethal : MonoBehaviour
{
    /*
     * @author Kate Howell
     */

    public AudioClip[] StabSounds;
    
    private AudioSource aso;

    private void Start()
    {
        aso = GetComponent<AudioSource>();
    }

//    private void OnCollisionEnter2D(Collision2D other)
//    {
//        if (other.gameObject.CompareTag("Player"))
//        {
//            PlayerController pC = other.transform.parent.GetComponentInChildren<PlayerController>();
//            if (pC != null && !pC.dead)
//            {
//                //pC.Die();
//            }
//            
//            
//        }
//    }

    //this function is derived from a scirpt Carsen originally wrote on the Player Controller
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController pC = other.transform.parent.GetComponentInChildren<PlayerController>();
            if (pC != null && !pC.dead)
            {
                pC.Die();
                Rigidbody2D collidedRb = other.GetComponent<Rigidbody2D>();
                collidedRb.constraints = RigidbodyConstraints2D.FreezePosition;

                GameManager.gm.InstantiateSplatter(other, this.GetComponent<Collider2D>());

                GetComponentInChildren<ParticleSystem>().transform.position = collidedRb.transform.position;
                GetComponentInChildren<ParticleSystem>().Play();

                if (aso != null)
                {
                    aso.pitch = 1;
                    aso.pitch += Random.Range(-0.3f, 0.3f);
                    aso.PlayOneShot(StabSounds[Random.Range(0, StabSounds.Length)]);
                }
            }


        }
    }


}
