using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    /*
     * @author Kate Howell
     * 
     * This class serves as a abstract class to build weapons off of
     * 
     * This class allows you to attack, and allows you to equip and unequip the weapon
     */

    [SerializeField, Tooltip("If gun is equipped to a Player")]
    public bool equipped;

    public bool pointingRight = true;

    public abstract void Attack();
    
    
    //function equips and unequips weapon
    public void Equip(GameObject player)
    {
        PlayerController controller;
        Transform currentTransform = player.transform;
        
        if (player.transform.parent == null)
        {
            throw new System.Exception("Weapon failed to Equip, parent = null");
        }

        //Small code changes by Carsen Decker
        while (currentTransform.parent != null || (!currentTransform.CompareTag("PlayerOne") && !currentTransform.CompareTag("PlayerTwo")))
        {
            currentTransform = currentTransform.parent;
        }

        if (currentTransform == null)
        {
            throw new System.Exception("Ah man the transform = null, something broke");
        }

        controller = currentTransform.GetComponentInChildren<PlayerController>();

        if (controller == null)
        {
            throw new System.Exception("Weapon failed to Equip, controller == null"); 
        }
            
        
        if (equipped)
        {
            equipped = false;
            //unequip
            this.gameObject.AddComponent<Rigidbody2D>();
            this.transform.parent = null;
            if(this.transform.parent != null)
            {
                throw new System.Exception("Weapon failed to unequip, parent != null");
            }
        }
        else
        {
            transform.SetParent(controller.gunLocation);
            transform.SetPositionAndRotation(controller.gunLocation.position, controller.gunLocation.rotation);

            Destroy(this.GetComponent<Rigidbody2D>());

            controller.weaponEquipped = true;
            controller.weapon = this;
            if(controller.weapon == null)
            {
                throw new System.Exception("Weapon failed to Equip, GunTestweapon = null");
            }
            
            equipped = true;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (!equipped)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Equip(collision.gameObject);
            }
        }
            
    }

    public void gunFacingRight(bool facingRight)
    {
        SpriteRenderer[] sprites = GetComponents<SpriteRenderer>();
        if (pointingRight)
        {
            if (!facingRight)
            {
                foreach (var spriteRenderer in sprites)
                {
                    spriteRenderer.flipY = true;
                }
            }
        }
        else
        {
            if (facingRight)
            {
                foreach (var spriteRenderer in sprites)
                {
                    spriteRenderer.flipY = false;
                }
            }
        }
    }

}
