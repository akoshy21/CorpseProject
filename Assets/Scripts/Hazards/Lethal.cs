using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Lethal : MonoBehaviour
{
    /*
     * @author Kate Howell
     */
    
    //this function is derived from a scirpt Carsen originally wrote on the Player Controller
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController pC = other.transform.parent.GetComponentInChildren<PlayerController>();
            if (pC != null && !pC.dead)
            {
                //pC.Die();
            }
            
            
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController pC = other.transform.parent.GetComponentInChildren<PlayerController>();
            if (pC != null && !pC.dead)
            {
                pC.Die();
            }


        }
    }


}
