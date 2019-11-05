using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterFall : MonoBehaviour
{
    public GameObject letter;
    public Rigidbody2D rigbod;

    
    private float timer;

    public float dropTime;
    
    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
        rigbod.isKinematic = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.anyKeyDown)
        {
            rigbod.isKinematic = true;
        }
    }
}
