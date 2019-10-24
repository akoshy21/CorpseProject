using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolBullet : Bullet
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
        //collision code
        //partical effects horraaayyy
    }

    /// <summary>
    /// To be called when the bullet collides with a player
    /// </summary>
    /// <param name="objectHit">Player Controller of the player the bullet hit</param>
    public override void Hit(PlayerController PlayerHit)
    {
        if (!PlayerHit.dead)
        {
            PlayerHit.Die();
        }

        //bloodSpatter horrayy
    }
}
