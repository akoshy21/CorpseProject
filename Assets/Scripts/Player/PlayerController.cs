﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //@author Carsen Decker
    
    [HideInInspector] public bool dead;
    public float MoveSpeed;
    public float JumpHeight;
    public float PushForce;
    public float ExtraHeight;
    public float TorqueForce;
    public bool facingRight = true;
    [Space(20)]
    public bool canMove = true;
    public bool grounded;
    public HingeJoint2D RightLeg, LeftLeg;
    public Transform RightFoot, LeftFoot;
    public float DistanceFromStraight;
    [Space(20)]
    public float LegMotorSpeed;

    private bool canJump = true;
    private bool canPush = true;
    private Rigidbody2D rb;
    private float coyoteTimer;
    private List<RaycastHit2D> groundCheckRight, groundCheckLeft;
    private bool groundedRight, groundedLeft;
    private Vector3 lastVel;
    private float footStartXRight, footStartXLeft;
    private bool reverseStep;
    private bool buttonReleased;
    private RagdollManager myRagdoll;
    
    //gun control edits by Kate Howell
    public bool weaponEquipped;
    public Weapon weapon;
    public Transform gunLocation;
    public GameObject gunLocationRight;
    public GameObject gunLocationLeft;

    //player specific movement edits by Kate Howell
    public int playerInt = 0;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        myRagdoll = GetComponentInParent<RagdollManager>();
        footStartXRight = RightFoot.transform.localPosition.x;
        footStartXLeft = LeftFoot.transform.localPosition.x;

        //Player Specific Movement added by Kate
        if (transform.parent.CompareTag("PlayerOne"))
        {
            playerInt = 1;
        }
        else if(transform.parent.CompareTag("PlayerTwo"))
        {
            playerInt = 2;
        }
        
        if (playerInt < 1)
        {
            throw new SystemException("Player Missing Correct Tag: " + transform.name);
        }
        
        //Gun Control added by Kate
        gunLocation = gunLocationRight.transform;

    }

    private void FixedUpdate()
    {
        lastVel = rb.velocity;
    }

    void Update()
    {
        //Always check for raycasts to the ground from both feet
        Ray2D rightRay = new Ray2D(RightFoot.position, -RightFoot.transform.up);
        Ray2D leftRay = new Ray2D(LeftFoot.position, -LeftFoot.transform.up);
        Ray2D rightRayDown = new Ray2D(RightFoot.position, Vector2.down);
        Ray2D leftRayDown = new Ray2D(LeftFoot.position, Vector2.down);

        Debug.DrawRay(rightRay.origin, rightRay.direction * 0.185f, Color.green);
        Debug.DrawRay(rightRayDown.origin, rightRayDown.direction * 0.185f, Color.green);
        Debug.DrawRay(leftRay.origin, leftRay.direction * 0.185f, Color.green);
        Debug.DrawRay(leftRayDown.origin, leftRayDown.direction * 0.185f, Color.green);


        groundCheckRight = new List<RaycastHit2D>(Physics2D.RaycastAll(rightRay.origin, rightRay.direction, 0.185f));
        groundCheckRight.AddRange(Physics2D.RaycastAll(rightRayDown.origin, rightRayDown.direction, 0.185f));
        
        groundCheckLeft = new List<RaycastHit2D>(Physics2D.RaycastAll(leftRay.origin, leftRay.direction, 0.185f));
        groundCheckLeft.AddRange(Physics2D.RaycastAll(leftRayDown.origin, leftRayDown.direction, 0.185f));


        if (groundCheckRight.Count > 0)
        {
            foreach (var hit in groundCheckRight)
            {
                if (!hit.collider.gameObject.CompareTag("Player"))
                    groundedRight = true;
                else
                    groundedRight = false;
            }
        }
        else
            groundedRight = false;

        if (groundCheckLeft.Count > 0)
        {
            foreach (var hit in groundCheckLeft)
            {
                if (!hit.collider.gameObject.CompareTag("Player"))
                    groundedLeft = true;
                else
                    groundedLeft = false;
            }
        }
        else
            groundedLeft = false;

        if (groundedRight || groundedLeft)
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }


        //If you can move or jump, accept those inputs
        if (canMove)
        {
            Move();
        }

        if (canJump)
        {
            Jump();
        }

        if (canPush)
        {
            Push();
        }
        
        //check for weapon attack - added by Kate
        if (weaponEquipped)
        {
            GunControl();
        }
        
        
        //Player Interaction - added by Kate

    }

    ///Moves the player with velocity (and lerps hooray)
    void Move()
    {
        Vector3 vel = rb.velocity;

        float moveInput = 0;
        
        if (playerInt == 1)//added by kate (:
        {
            //will be -1, 1 or 0
            moveInput = Input.GetAxisRaw("P1Horizontal");

            if (Input.GetKey(KeyCode.D))
            {
                moveInput = 1;
            }else if (Input.GetKey(KeyCode.A))
            {
                moveInput = -1;
            }
        }
        else
        {
            moveInput = Input.GetAxisRaw("P2Horizontal");    
            
            if (Input.GetKey(KeyCode.L))
            {
                moveInput = 1;
            }else if (Input.GetKey(KeyCode.J))
            {
                moveInput = -1;
            }
        }
        
        
        
        if (moveInput > 0)
        {
            facingRight = true;
        }
        else if(moveInput < 0)
        {
            facingRight = false;
        }

        vel.x = Mathf.Lerp(vel.x, MoveSpeed * moveInput, .1f);   

        rb.velocity = vel;

        if (grounded)
        {
            WalkCycle();
        }
        else
        {
            RightLeg.useMotor = false;
            LeftLeg.useMotor = false;
            reverseStep = false;
        }
    }

    /// <summary>
    /// Alternates stepping forward with either leg in order to "walk" properly
    /// </summary>
    void WalkCycle()
    {
        JointMotor2D rightMotor = RightLeg.motor;
        JointMotor2D leftMotor = LeftLeg.motor;
        
        RightFoot.rotation = Quaternion.Euler(Vector2.down);
        LeftFoot.rotation = Quaternion.Euler(Vector2.down);
        
        Rigidbody2D rightRb = RightFoot.GetComponent<Rigidbody2D>();
        Rigidbody2D leftRb = LeftFoot.GetComponent<Rigidbody2D>();

        float moveInput;
        
        if (playerInt == 1)//added by kate (:
        {
            //will be -1, 1 or 0
            moveInput = Input.GetAxisRaw("P1Horizontal");
            
            if (Input.GetKey(KeyCode.D))
            {
                moveInput = 1;
            }else if (Input.GetKey(KeyCode.A))
            {
                moveInput = -1;
            }
        }
        else
        {
            moveInput = Input.GetAxisRaw("P2Horizontal");  
            
            if (Input.GetKey(KeyCode.L))
            {
                moveInput = 1;
            }else if (Input.GetKey(KeyCode.J))
            {
                moveInput = -1;
            }
        }

        if (moveInput > 0)
        {
            if (!reverseStep)
            {
//                Debug.Log(footStartXRight - RightFoot.transform.localPosition.x);
                if (footStartXRight - RightFoot.transform.localPosition.x < DistanceFromStraight)
                {
                    leftMotor.motorSpeed = LegMotorSpeed;
                    LeftLeg.motor = leftMotor;
                    LeftLeg.useMotor = true;
                    RightLeg.useMotor = false;
                    
                    rightRb.velocity = new Vector2(Mathf.Lerp(rightRb.velocity.x, MoveSpeed * 2, 0.1f), rightRb.velocity.y);
                }
                else if (footStartXRight - RightFoot.transform.localPosition.x >= DistanceFromStraight)
                {
                    reverseStep = true;

                    rightMotor.motorSpeed = LegMotorSpeed;
                    RightLeg.motor = rightMotor;
                    RightLeg.useMotor = true;

                    LeftLeg.useMotor = false;
                }

            }
            else if (reverseStep)
            {
                if (footStartXLeft - LeftFoot.transform.localPosition.x < DistanceFromStraight)
                {
                    leftRb.velocity = new Vector2(Mathf.Lerp(leftRb.velocity.x, MoveSpeed * 4, 0.1f), leftRb.velocity.y);
                }
                else if (footStartXLeft - LeftFoot.transform.localPosition.x >= DistanceFromStraight)
                {
                    reverseStep = false;

                    leftMotor.motorSpeed = LegMotorSpeed;
                    LeftLeg.motor = leftMotor;
                    LeftLeg.useMotor = true;

                    RightLeg.useMotor = false;
                }
            }
        }
        else if (moveInput < 0)
        {
            if (reverseStep)
            {
//                Debug.Log(footStartXRight - RightFoot.transform.localPosition.x);
                if (footStartXRight - RightFoot.transform.localPosition.x > -DistanceFromStraight)
                {
                    rightRb.velocity = new Vector2(Mathf.Lerp(rightRb.velocity.x, -MoveSpeed * 2, 0.1f), rightRb.velocity.y);
                }
                else if (footStartXRight - RightFoot.transform.localPosition.x <= -DistanceFromStraight)
                {
                    reverseStep = false;

                    rightMotor.motorSpeed = -LegMotorSpeed;
                    RightLeg.motor = rightMotor;
                    RightLeg.useMotor = true;

                    LeftLeg.useMotor = false;
                }

            }
            else if (!reverseStep)
            {
//                Debug.Log(footStartXRight - LeftFoot.transform.localPosition.x);
                if (footStartXLeft - LeftFoot.transform.localPosition.x > -DistanceFromStraight)
                {
                    rightMotor.motorSpeed = -LegMotorSpeed;
                    RightLeg.motor = rightMotor;
                    RightLeg.useMotor = true;

                    LeftLeg.useMotor = false;
                    
                    leftRb.velocity = new Vector2(Mathf.Lerp(leftRb.velocity.x, -MoveSpeed * 4, 0.1f), leftRb.velocity.y);
                }
                else if (footStartXLeft - LeftFoot.transform.localPosition.x <= -DistanceFromStraight)
                {
                    reverseStep = true;

                    leftMotor.motorSpeed = -LegMotorSpeed;
                    LeftLeg.motor = leftMotor;
                    LeftLeg.useMotor = true;
                    RightLeg.useMotor = false;
                }
            }
        }
        else
        {
            RightLeg.useMotor = true;
            LeftLeg.useMotor = true;
            
            rightMotor.motorSpeed = 0;
            RightLeg.motor = rightMotor;
            leftMotor.motorSpeed = 0;
            LeftLeg.motor = leftMotor;
//            LeftLeg.useMotor = false;
            reverseStep = false;
            
        }
    }    
    
    void Jump()
    {
        //makes a timer that counts down whenever not touching the ground. gives short window to jump when falling off a ledge
        float jumpInput;
        
        if (playerInt == 1)//added by kate (:
        {
            //will be -1, 1 or 0
            jumpInput = Input.GetAxisRaw("P1Jump");

            if (Input.GetKeyDown(KeyCode.W))
            {
                jumpInput = 1;
            }
        }
        else
        {
            jumpInput = Input.GetAxisRaw("P2Jump");
            //Debug.Log(playerInt + " is jumpin");

            if (Input.GetKeyDown(KeyCode.I))
            {
                jumpInput = 1;
            }
        }

        if (jumpInput <= 0)
        {
            buttonReleased = true;
        }
        
        //If you are grounded when you jump
        if (grounded)
        {
            coyoteTimer = 0.1f;
            rb.gravityScale = 1.5f;
            if (jumpInput > 0 && buttonReleased)
            {
                rb.AddForce(new Vector2(0, JumpHeight), ForceMode2D.Impulse);
                StartCoroutine(TinyJumpDelay());
                coyoteTimer = 0;
                buttonReleased = false;
            }
        }
        //If you fall off something and try and jump in mid air
        else
        {
            coyoteTimer -= Time.deltaTime;
            if (jumpInput > 0 && coyoteTimer > 0 && buttonReleased)
            {
                rb.AddForce(new Vector2(0, JumpHeight), ForceMode2D.Impulse);
                coyoteTimer = 0;
                buttonReleased = false;
            }
            else if (jumpInput <= 0 && coyoteTimer <= 0)
            {
                rb.gravityScale = 8f;
            }
        }
    }

    //A very small delay between when you are able to jump again because im losing my mind
    private IEnumerator TinyJumpDelay()
    {
        canJump = false;
        yield return new WaitForSeconds(0.1f);
        canJump = true;
    }

    private IEnumerator PushDelay()
    {
        canPush = false;
        yield return new WaitForSeconds(0.4f);
        canPush = true;
    }

    //Extra juice option, adds a small time pause (think Smash) when hitting something
    private IEnumerator HitDelay()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(0.04f);
        Time.timeScale = 1;
    }

    //Gun Controller added by Kate
    public void GunControl()
    {
        if (playerInt == 1)
        {
            float attackInput = Input.GetAxisRaw("P1Fire");

            if (attackInput > 0)
            {
                if (weaponEquipped)
                {
                    weapon.Attack();
                }
            }
        }
        else
        {
            float attackInput = Input.GetAxisRaw("P2Fire");

            if (attackInput > 0)
            {
                if (weaponEquipped)
                {
                    weapon.Attack();
                }
            }
        }
        
        //gun location based on direction
        if (facingRight)
        {
            weapon.gunFacingRight(true);
        }
        else
        {
            weapon.gunFacingRight(false);
        }
    }

    private void Push()
    {
        float pushInput = 0;
        
        if (playerInt == 1)
        {
            pushInput = Input.GetAxisRaw("P1Push");
        }
        else if (playerInt == 2)
        {
            pushInput = Input.GetAxisRaw("P2Push");
        }

        if (pushInput > 0)
        {
//            Debug.Log("Pushed");
            RaycastHit2D[] pushHits = new RaycastHit2D[0];
            if (facingRight)
            {
                pushHits = Physics2D.BoxCastAll(transform.position, transform.localScale, 0, Vector2.right, 0.2f);
            }
            else
            {
                pushHits = Physics2D.BoxCastAll(transform.position, transform.localScale, 0, Vector2.left, 0.2f);
            }

            if (pushHits.Length > 0)
            {
                foreach (var hits in pushHits)
                {
                    if (hits.collider.CompareTag("Player"))
                    {
                        Transform currentTf = hits.collider.transform;
                        while (currentTf.parent != null || (!currentTf.CompareTag("PlayerOne") && !currentTf.CompareTag("PlayerTwo")))
                        {
                            currentTf = currentTf.parent;
                        }
                        
                        Rigidbody2D hitRb = hits.collider.GetComponent<Rigidbody2D>();

                        if (playerInt == 1 && currentTf.CompareTag("PlayerTwo"))
                        {
                            if (facingRight)
                            {
                                hitRb.AddForce(Vector2.right * PushForce);
                            }
                            else
                            {
                                hitRb.AddForce(Vector2.left * PushForce);
                            }

                            StartCoroutine(PushDelay());
                        }
                        else if (playerInt == 2 && currentTf.CompareTag("PlayerOne"))
                        {
                            if (facingRight)
                            {
                                hitRb.AddForce(Vector2.right * PushForce);
                            }
                            else
                            {
                                hitRb.AddForce(Vector2.left * PushForce);
                            }

                            StartCoroutine(PushDelay());
                        }
                    }
                }
            }
        }
    }


    //-----------------------------------------------------------//
    //  PUBLIC HELPER FUNCTIONS (for calling from other scripts) //
    //-----------------------------------------------------------//


    ///Kills the player and turns them into an uncontrollable corpse. REMEMBER TO CALL THIS LAST
    public void Die()
    {
        if (!dead)
        {
            dead = true;
            myRagdoll.CreateRagdoll();

            if (playerInt == 1)
            {
                LevelManager.lm.corpseCount1++;
            }
            else if (playerInt == 2)
            {
                LevelManager.lm.corpseCount2++;
            }
        }
    }

    ///Launches the player a certain amount in a certain direction
    public void Launch(float launchForce, Vector3 launchDirection)
    {
        rb.AddForce(launchDirection * launchForce, ForceMode2D.Impulse);
    }

    /// <summary>
    /// More similar functionality to my spike game, will launch the player at a classic pong "bounce" trajectory
    /// (usually make <c>collidingObject</c> the transform of the object that is calling this function)
    /// </summary>
    public void LaunchMirrored(GameObject collidingObject, Collision2D collisionData, float forceMultiplier)
    {
        //Add a force relative to your current velocity
        Vector2 flingForce = Vector2.Reflect(lastVel, -collisionData.GetContact(0).normal);
        flingForce.y += ExtraHeight;
        if (collidingObject.GetComponent<Rigidbody2D>() != null)
        {
            flingForce += collidingObject.GetComponent<Rigidbody2D>().velocity;
        }
        rb.AddForce(flingForce * forceMultiplier, ForceMode2D.Impulse);
        rb.AddTorque(-flingForce.x * TorqueForce);
    }
    
    
    
    //-----------------------------------------------------------//
    //         GET FUNCTIONS (for calling from other scripts)    //
    //-----------------------------------------------------------//

    public Vector3 GetVelocity()
    {
        return rb.velocity;
    }

}
