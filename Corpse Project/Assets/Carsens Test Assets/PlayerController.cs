using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    //SHOULD I MAKE THIS A SINGLETON??
    // public static PlayerController player;
    
    public float MoveSpeed;
    public float JumpHeight;
    public float DeathForce;
    public float ExtraHeight;
    public float TorqueForce;
    [Space(20)] 
    public GameObject DeathParticles;
    public AudioClip HitSound, TerrainHitSound, JumpSound;
    public bool canMove = true;

    private bool canJump = true;
    private Rigidbody2D rb;
    private float coyoteTimer;
    private RaycastHit2D[] groundCheck;
    private Vector3 lastVel;
    private AudioSource aso;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        aso = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        lastVel = rb.velocity;
    }

    void Update()
    {
        //Always check for raycasts to the ground
        Ray2D ray = new Ray2D(transform.position, Vector2.down);
        Debug.DrawRay(ray.origin, ray.direction * 1f, Color.green);

        Vector3 raySize = transform.localScale;
        groundCheck = Physics2D.BoxCastAll(ray.origin, raySize, 0f, ray.direction, 0.4f);
        
        
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
        if (groundCheck.Length > 0)
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
   

//    Not sure if we need any of these things if the actual object is the one calling OnTrigger/Collision
//    Commenting this out so shit doesnt break
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        //****** Will need to change this check *****
        if (other.gameObject.CompareTag("Hazard"))
        {
            //Add a force relative to your current velocity
            Vector2 flingForce = Vector2.Reflect(lastVel, -other.transform.up);
            flingForce.y += ExtraHeight;
            rb.AddForce(flingForce * DeathForce);
            
            //Rotate for fun
            rb.AddTorque(-flingForce.x * TorqueForce);
            
            //Create particle effect at point of impact, for fun
            Instantiate(DeathParticles, other.GetContact(0).point, Quaternion.identity);
            
            //Shake camera for fun
            Camera.main.GetComponent<CameraShake>().shakeMagnitude = Mathf.Clamp(lastVel.magnitude / 100, 0.1f, 0.37f);
            Camera.main.GetComponent<CameraShake>().ShakeCamera(0.25f);

            //Play a sound.... for fun.
            aso.PlayOneShot(HitSound);
        }

        else
        {
            if (!canMove)
            {
                aso.PlayOneShot(TerrainHitSound);
                Camera.main.GetComponent<CameraShake>().shakeMagnitude = 0.05f;
                Camera.main.GetComponent<CameraShake>().ShakeCamera(0.1f);
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

    
    //Kills the player and turns them into an uncontrollable corpse
    public void Die()
    {
        Destroy(this); //removes this script so u cant control it anymore
        
        //other stuff if we want it
    }

    //Launches the player a certain amount
    public void Launch(float launchForce, Vector3 launchDirection)
    {
        rb.AddForce(launchDirection * launchForce, ForceMode2D.Impulse);
        //thats it rn
    }
}
