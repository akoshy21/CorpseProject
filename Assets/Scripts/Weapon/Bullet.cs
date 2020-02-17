using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    /*
     * @author Kate Howell
     * 
     * This class serves as a abstract class to build bullets for specific weapons
     * 
     * This class allows moves the bullet, handles player collision (abstractly), and handles object collision (abstractly)
     */

    
    private Vector2 intialVelocity;

    [SerializeField, Tooltip("Speed bullet moves")]
    public float bulletSpeed;

    public bool hitObject;

    [SerializeField, Tooltip("Rate of deceleration over time on bullet on the X axis")]
    public float decelerationX;

    [SerializeField, Tooltip("Rate at which bullet falls on the y axis")]
    public float decelerationY;

    private float velocityY;

    private float velocityX;

    private Vector2 velocity;

    private bool pointingRight;

    private Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        //if the intial velocity of the bullet is never set - this sound be done when its spawned
        if (intialVelocity.Equals(Vector2.zero))
        {
            Destroy(this.gameObject);
        }

        //the initial veloctiy should be a nomalized vector
        if(intialVelocity.x > 1)
        {
            intialVelocity.Normalize();
        }

        //switch veloctiy direction if pointingRight is true
        if (pointingRight)
        {
            intialVelocity = -intialVelocity;
        }

        //calculate the new velocity
        velocityY = intialVelocity.y * bulletSpeed;

        velocityX = intialVelocity.x * bulletSpeed;

        //find the rigidBody componenet
        rigidBody = GetComponent<Rigidbody2D>();

        //check the rigid body exsists
        if(rigidBody == null)
        {
            throw new System.Exception("No rigid body found on bullet");
        }
    }

    void Update()
    {
        if (!hitObject)
        {
            Move();
        }
        else //what to do after you hit something?
        {
            //rigidBody.velocity = new Vector2(0, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print(collision.transform.name);
        //if you havent already hit something
        if (!hitObject)
        {
            

            //Destroy(rigidBody);

            //if it was the player that you hit, 
            if (collision.gameObject.CompareTag("Player"))
            {
               
                //used to get the player controller 
                GameObject current = collision.gameObject;
                PlayerController controller = current.transform.parent.GetComponentInChildren<PlayerController>();
                if (controller == null)
                {
                    current = current.transform.parent.gameObject;
                    controller = current.transform.parent.GetComponentInChildren<PlayerController>();
                }
                if (controller == null)
                {
                    current = current.transform.parent.gameObject;
                    controller = current.transform.parent.GetComponentInChildren<PlayerController>();
                }
                if (controller != null && !controller.dead)
                {
                    //run Hit with the controller as input
                    Hit(controller);

                    //parent the nail the the dead body
                    //this.transform.SetParent(collision.gameObject.transform);
                    //parent the body to the nail
                    controller.gameObject.transform.parent.transform.SetParent(this.gameObject.transform);

                    //blood splatter
                    GameManager.gm.InstantiateSplatter(collision.collider, this.GetComponent<Collider2D>());
                }

                return;
            }
            else if (collision.gameObject.CompareTag("Corpse"))
            {
               // Destroy(this.gameObject);

                //hit something that wasnt the player
               // hitObject = true;
            }
            else
            {
                Collide(collision.gameObject);

                //hit something that wasnt the player
                //hitObject = true;
            }

        }
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        print(collision.gameObject.transform.name);
        if (collision.gameObject.CompareTag("Ground"))
        {
            print("ground");
            hitObject = true;
            Collide(collision.gameObject);
        }
        
    }

    public abstract void Collide(GameObject objectHit);

    public abstract void Hit(PlayerController PlayerHit);

    private void Move()
    {
        
        velocityY -= decelerationY;
        velocityX -= decelerationX;

        velocity = new Vector2(velocityX, velocityY);
        transform.Translate(velocity * Time.deltaTime);
        //rigidBody.velocity = velocity;
    }

    /// <summary>
    /// To be called when bullet is instiated to set its intial direction
    /// </summary>
    /// <param name="velocity">Normalized Vector3 direction for bullet to move</param>
    public void SetIntialVelocity(Vector3 velocity)
    {
        intialVelocity = velocity;
    }
}
