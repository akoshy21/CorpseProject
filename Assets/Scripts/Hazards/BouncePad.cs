using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    [SerializeField, Tooltip("Must be more than 100")]
    public float launchHeightMax;

    [SerializeField, Tooltip("Base Force Before Adjustments")]
    public float baseLaunchForce = 150;

    //public List<Rigidbody2D> currentBodies = new List<Rigidbody2D>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

}
