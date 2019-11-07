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

    private bool hitObject;

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
        if (intialVelocity.Equals(Vector2.zero))
        {
            Destroy(this.gameObject);
        }

        if(intialVelocity.x > 1)
        {
            intialVelocity.Normalize();
        }

        if (pointingRight)
        {
            intialVelocity = -intialVelocity;
        }
        velocityY = intialVelocity.y * bulletSpeed;

        velocityX = intialVelocity.x * bulletSpeed;

        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!hitObject)
        {
            Move();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //print("collision");
        if (!hitObject)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
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
                    Hit(controller);
                    this.transform.SetParent(collision.gameObject.transform);
                    GameManager.gm.InstantiateSplatter(collision.collider, this.GetComponent<Collider2D>());
                }     
            }
            else if (collision.gameObject.CompareTag("Corpse"))
            {
                Destroy(this.gameObject);
            }
            else
            {
                Collide(collision.gameObject);
            }
            
            hitObject = true;
        }
        
    }

    public abstract void Collide(GameObject objectHit);

    public abstract void Hit(PlayerController PlayerHit);

    private void Move()
    {
        velocityY -= decelerationY;
        velocityX -= decelerationX;

        velocity = new Vector2(velocityX, velocityY);
        //transform.Translate(velocity * Time.deltaTime);
        rigidBody.velocity = velocity;
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
