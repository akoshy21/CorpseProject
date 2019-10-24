﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatfromFunctions : MonoBehaviour
{
    public Rigidbody2D rigbod;
    public GameObject platform;
    public Transform topRightBound;
    public Transform leftBottomBound;
    
    public float moveSpeedUp;
    public float moveSpeedSide;

    public bool turnOn; //public bool just to remotely turn platform on
    public bool goUp;//if true, platform moves vertically, if false horizontally

    private bool moveLeft;
    private bool moveRight;

    private bool moveDown;
    private bool moveUp;
    
    void Start()
    {
        if (goUp)
        {
            moveUp = true;
        }
        else
        {
            moveRight = true;
        }
    }

   
    void Update()
    {
        if (turnOn)
        {
            if (goUp)
            {
                if (platform.transform.position.y > topRightBound.transform.position.y)
                {
                    moveUp = false;
                    moveDown = true;
                   
                }
                
                if (platform.transform.position.y < leftBottomBound.transform.position.y)
                {
                    moveUp = true;
                    moveDown = false;
                }

                    if (moveUp)
                    {
                        rigbod.velocity = (new Vector2(0f, moveSpeedUp));
                    }

                     if (moveDown)
                    {
                        Debug.Log("going down");
                        rigbod.velocity = (new Vector2(0f, -moveSpeedUp));   
                    }
            }

            if (goUp == false)
            {
                if (platform.transform.position.x > topRightBound.transform.position.x)
                {
                    moveRight = false;
                    moveLeft = true;
                }
                
                if (platform.transform.position.x < leftBottomBound.transform.position.x)
                {
                    moveRight = true;
                    moveLeft = false;    
                }

                if (moveRight)
                {
                    rigbod.velocity = (new Vector2(moveSpeedSide, 0f));
                }

                if (moveLeft)
                {
                    rigbod.velocity = (new Vector2(-moveSpeedSide,0f));
                }
            }
        }  
    }
}