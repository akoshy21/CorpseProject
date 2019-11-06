﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public bool isLever;
    public bool buttonActive;
    private List<GameObject> entryList;
    public bool stayOn;

    public SpriteRenderer spriteRend;
    public GameObject buttonBoxCollider;
    public Sprite buttonpressedsprite;

    public Sprite buttonunpressedspirte;

    public Sprite leverOnSprite;

    public Sprite leverOffSprite;
    // Start is called before the first frame update
    void Start()
    {
        entryList = new List<GameObject>();
        if (isLever == false)
        {
            spriteRend.sprite = buttonunpressedspirte;
            buttonBoxCollider.SetActive(true);
        }

        if (isLever)
        {
            spriteRend.sprite = leverOffSprite;
            buttonBoxCollider.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isLever == false && buttonActive == false)
        {
            spriteRend.sprite = buttonunpressedspirte;
        }
       
        if (entryList.Count > 0)
        {
            
            if (isLever == false)
            {
                buttonActive = true;
                spriteRend.sprite = buttonpressedsprite;
            }

            if (isLever)
            {
                if (Input.GetKey(KeyCode.E) || Input.GetButton("P1Interact") || Input.GetButton("P2Interact"))
                {
                    buttonActive = true;
                    spriteRend.sprite = leverOnSprite;
                }
                else
                {
                    buttonActive = false;
                    spriteRend.sprite = leverOffSprite;
                }
            }
        }
        else
        {
            buttonActive = false;
        }

     

        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
     
        entryList.Add(other.gameObject);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
      Debug.Log(other);
        

        entryList.Remove(other.gameObject);
    }

   
}
