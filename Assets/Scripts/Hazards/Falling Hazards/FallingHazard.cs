using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class FallingHazard : MonoBehaviour
{
    /*
     * @author Kate Howell
     */

    [HideInInspector] public bool activated;
    [HideInInspector] public bool fell;
    [HideInInspector] public bool grounded;
    private bool moveUp;
    private Rigidbody2D rigidBody;
    private float startY;

    [SerializeField, Tooltip("check if you want the object to reset and fall again")]
    public bool reset;
    [SerializeField, Tooltip("time object will wait before moving up, only valid if reset is true")]
    public float waitToMoveUp = 3f;
    public float moveSpeedUp = 3f;




    private void Start()
    {
        rigidBody = this.GetComponent<Rigidbody2D>();

        if(rigidBody == null)
        {
            throw new System.Exception("Falling Hazard Rigid Body is Null: " + name);
        }

        rigidBody.gravityScale = 0;

        startY = transform.position.y;
    }
    private void Update()
    {
        if(!moveUp && !activated)
        {
            CheckForActivation();
        }
       

        if (moveUp && activated)
        {
            rigidBody.velocity = new Vector2(0f, moveSpeedUp);
            if(transform.position.y > startY)
            {
                moveUp = false;
                activated = false;
            }
        }
        else if (!activated)
        {
            rigidBody.velocity = new Vector2(0f, 0f);
        }
    }

    public abstract void CheckForActivation();

    public void Fall()
    {
        rigidBody.gravityScale = 1;
        if (reset)
        {
            StartCoroutine(ReturnDelay());
        } 
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && !grounded && !moveUp)
        {
            PlayerController pC = other.transform.parent.GetComponentInChildren<PlayerController>();
            if (pC != null && !pC.dead)
            {
                //pC.Explode();  
            }
        }
        else if (other.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !grounded && !moveUp)
        {
            PlayerController pC = other.transform.parent.GetComponentInChildren<PlayerController>();
            if (pC != null && !pC.dead)
            {
                pC.Explode();
            }
        }
        else if (other.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }
    }

    public IEnumerator ReturnDelay()
    {
        yield return new WaitForSeconds(3f);
        moveUp = true;
        fell = false;
        grounded = false;
        rigidBody.gravityScale = 0;
    }
    

}
