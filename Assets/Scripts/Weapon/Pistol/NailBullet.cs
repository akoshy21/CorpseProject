using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NailBullet : Bullet
{
    /*
     * @author Kate Howell
     * 
     * this class extends the abstract Bullet class, with specific implementation for type Pistol
     * 
     */

    /// <summary>
    /// To be called when the bullet collides with anything other than a player
    /// </summary>
    /// <param name="objectHit">Object the bullet hits</param>
    public override void Collide(GameObject objectHit)
    {
        print("collide");
        if (objectHit.CompareTag("Ground"))
        {
            
        }
        else
        {
            //Destroy(this.gameObject);
        }
        //Destroy(this.gameObject);
        //collision code
        //partical effects horraaayyy
    }

    /// <summary>
    /// To be called when the bullet collides with a player
    /// </summary>
    /// <param name="objectHit">Player Controller of the player the bullet hit</param>
    public override void Hit(PlayerController PlayerHit)
    {
        print("hit");
        if (!PlayerHit.dead)
        {
            PlayerHit.Die();
            Rigidbody2D collidedRb = PlayerHit.GetComponent<Rigidbody2D>();
            collidedRb.constraints = RigidbodyConstraints2D.FreezePosition;
        }
        
        GetComponentInChildren<ParticleSystem>().Play();
        
    }
}
