using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// @author: Carsen Decker
public class PlayerController : MonoBehaviour
{
    // public static PlayerController player;

    public bool dead;

    public float MoveSpeed;
    public float JumpHeight;
    public float DeathForce;
    public float ExtraHeight;
    public float TorqueForce;
    [Space(20)]
    public bool canMove = true;
    public bool grounded;
    public HingeJoint2D RightLeg, LeftLeg;
    public Transform RightFoot, LeftFoot;
    public Transform RightLegJoint, LeftLegJoint;
    public float DistanceFromStraight;

    private bool canJump = true;
    private Rigidbody2D rb;
    private float coyoteTimer;
    private RaycastHit2D[] groundCheck;
    private Vector3 lastVel;
    private float footStartXPos;

    private RagdollManager myRagdoll;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        myRagdoll = GetComponentInParent<RagdollManager>();
        footStartXPos = RightFoot.transform.localPosition.x;
    }

    private void FixedUpdate()
    {
        lastVel = rb.velocity;
    }

    void Update()
    {
        LegKinematics();
        //Always check for raycasts to the ground
        Ray2D ray = new Ray2D(transform.position, Vector2.down);
        Debug.DrawRay(ray.origin, ray.direction * 1.2f, Color.green);

        Vector3 raySize = transform.localScale;
        groundCheck = Physics2D.RaycastAll(ray.origin, ray.direction, 1.2f);

        if (groundCheck.Length > 0)
        {
            foreach (var hit in groundCheck)
            {
                if (hit.collider.gameObject.CompareTag("Ground") || hit.collider.CompareTag("Player"))
                {
                    //Annoyingly long check just to see if one of the objects tagged "player" is a part of this object or not
                    //If it is, we dont want to be able to jump off of ourselves
                    bool notMe = true;
                    for (int i = 0; i < transform.parent.childCount; i++)
                    {
                        if (hit.collider.gameObject.Equals(transform.parent.GetChild(i).gameObject))
                        {
                            notMe = false;
                        }
                    }

                    if (notMe)
                    {
                        grounded = true;
                        break;
                    }

                }
                else
                {
                    grounded = false;
                }
            }
        }
        else
            grounded = false;

        if (canMove)
        {
            Move();
        }

        if (canJump)
        {
            Jump();
        }

    }

    ///Moves the player with velocity (and lerps hooray)
    void Move()
    {
        Vector3 vel = rb.velocity;
        if (Input.GetKey(KeyCode.A))
        {
            vel.x = Mathf.Lerp(vel.x, -MoveSpeed, 0.1f);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            vel.x = Mathf.Lerp(vel.x, MoveSpeed, 0.1f);
        }
        else
        {
            vel.x = Mathf.Lerp(vel.x, 0, 0.1f);
        }

        rb.velocity = vel;
        
        WalkCycle();
    }

    void LegKinematics()
    {
//        foreach (var joint in RightLegJoints)
//        {
//            Vector2 directionToEffector = RightFoot.GetChild(0).position - joint.position;
//            Vector2 directionToGoal = RightGoalPositionTest.transform.position - joint.position;
//            joint.rotation = Quaternion.FromToRotation(directionToEffector, directionToGoal);
//        }
    }

    void WalkCycle()
    {
        JointMotor2D rightMotor = RightLeg.motor;
        JointMotor2D leftMotor = LeftLeg.motor;
        
        RightFoot.rotation = Quaternion.Euler(Vector2.down);
        
        Rigidbody2D rightRb = RightFoot.GetComponent<Rigidbody2D>();

        if (Input.GetKey(KeyCode.D) && Mathf.Abs(RightFoot.transform.localPosition.x - footStartXPos) >= DistanceFromStraight)
        {
            rightRb.velocity = new Vector2(Mathf.Lerp(rightRb.velocity.x, MoveSpeed, 0.1f), rightRb.velocity.y);
        }
        
    }    
    
    void Jump()
    {
        //makes a timer that counts down whenever not touching the ground. gives short window to jump when falling off a ledge
        if (grounded)
        {
            coyoteTimer = 0.1f;
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
            {
                rb.AddForce(new Vector2(0, JumpHeight), ForceMode2D.Impulse);
                StartCoroutine(TinyJumpDelay());
                coyoteTimer = 0;
            }
        }
        else
        {
            coyoteTimer -= Time.deltaTime;
            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && coyoteTimer > 0)
            {
                rb.AddForce(new Vector2(0, JumpHeight), ForceMode2D.Impulse);
                coyoteTimer = 0;
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

    //Extra juice option, adds a small time pause (think Smash) when hitting something
    private IEnumerator HitDelay()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(0.04f);
        Time.timeScale = 1;
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
    public void LaunchMirrored(GameObject collidingObject, Collision2D collisionData)
    {
        //Add a force relative to your current velocity
        Vector2 flingForce = Vector2.Reflect(lastVel, -collisionData.GetContact(0).normal);
        flingForce.y += ExtraHeight;
        if (collidingObject.GetComponent<Rigidbody2D>() != null)
        {
            flingForce += collidingObject.GetComponent<Rigidbody2D>().velocity;
        }
        rb.AddForce(flingForce * DeathForce, ForceMode2D.Impulse);
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
