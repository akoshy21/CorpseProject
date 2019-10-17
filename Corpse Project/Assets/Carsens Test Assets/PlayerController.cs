using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public AudioClip HitSound, TerrainHitSound, JumpSound;
    public bool canMove = true;
    public bool grounded;

    private bool canJump = true;
    private Rigidbody2D rb;
    private float coyoteTimer;
    private RaycastHit2D[] groundCheck;
    private Vector3 lastVel;
    private AudioSource aso;

    private RagdollManager myRagdoll;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        aso = GetComponent<AudioSource>();
        myRagdoll = gameObject.GetComponentInParent<RagdollManager>();
        Debug.Log("Ragdoll script is: " + myRagdoll);
    }

    private void FixedUpdate()
    {
        lastVel = rb.velocity;
    }

    void Update()
    {
        //Always check for raycasts to the ground
        Ray2D ray = new Ray2D(transform.position, Vector2.down);
        Debug.DrawRay(ray.origin, ray.direction * 1.2f, Color.green);

        Vector3 raySize = transform.localScale;
        groundCheck = Physics2D.RaycastAll(ray.origin, ray.direction, 1.2f);
//        groundCheck = Physics2D.BoxCastAll(ray.origin, raySize, 0f, ray.direction, 1f);

        if (groundCheck.Length > 0)
        {
            foreach (var hit in groundCheck)
            {
                if (hit.collider.gameObject.CompareTag("Ground"))
                {
                    grounded = true;
                    break;
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

    //Moves the player with velocity (and lerps hooray)
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
                aso.PlayOneShot(JumpSound);
            }
        }
        else
        {
            coyoteTimer -= Time.deltaTime;
            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && coyoteTimer > 0)
            {
                rb.AddForce(new Vector2(0, JumpHeight), ForceMode2D.Impulse);
                coyoteTimer = 0;
                aso.PlayOneShot(JumpSound);
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

    
    ///Kills the player and turns them into an uncontrollable corpse
    public void Die()
    {
        if (!dead)
        {
            dead = true;
            Debug.Log("death");
//            myRagdoll.dead = true;
//            myRagdoll.CreateRagdoll();
//            RagdollManager.body.dead = true;
        }
    }

    ///Launches the player a certain amount in a certain direction
    public void Launch(float launchForce, Vector3 launchDirection)
    {
        rb.AddForce(launchDirection * launchForce, ForceMode2D.Impulse);
        //thats it rn
    }
    
    /// <summary>
    /// More similar functionality to my spike game, will launch the player at a classic pong "bounce" trajectory
    /// (usually make <c>collidingObject</c> the transform of the object that is calling this function)
    /// </summary>
    public void LaunchMirrored(Transform collidingObject)
    {
        //Add a force relative to your current velocity
        Vector2 flingForce = Vector2.Reflect(lastVel, -collidingObject.transform.up);
        flingForce.y += ExtraHeight;
        rb.AddForce(flingForce * DeathForce);
    }
    /// <summary>
    /// Launches the player at trajectory mirrored to their velocity, but spins them around for a more floppy effect
    /// </summary>
    public void LaunchMirrored(Transform collidingObject, bool spinRotation)
    {
        Vector2 flingForce = Vector2.Reflect(lastVel, -collidingObject.transform.up);
        flingForce.y += ExtraHeight;
        rb.AddForce(flingForce * DeathForce);
        rb.AddTorque(-flingForce.x * TorqueForce);
    }
}
