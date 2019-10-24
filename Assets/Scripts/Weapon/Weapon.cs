﻿using System.Collections;
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

    public abstract void Attack();

    //function equips and unequips weapon
    public void Equip(GameObject player)
    {
        PlayerController controller = player.transform.parent.GetComponentInChildren<PlayerController>();
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
            equipped = true;
            
            

            //Testing equip until player controller can be modified
            GunTest GunTest = player.transform.parent.GetComponent<GunTest>();

            if (player.transform.parent == null)
            {
                throw new System.Exception("Weapon failed to Equip, parent = null");
            }

            if (GunTest == null)
            {
                throw new System.Exception("Weapon failed to Equip, gunTest = null");
            }

            if(GunTest.gunlocation == null)
            {
                throw new System.Exception("Weapon failed to Equip, gunLocation = null");
            }

            this.transform.SetParent(GunTest.gunlocation.transform);
            this.transform.SetPositionAndRotation(GunTest.gunlocation.transform.position, GunTest.gunlocation.transform.rotation);

            if (this.transform.parent == null)
            {
                throw new System.Exception("Weapon failed to Equip, parent = null");
            }

            Destroy(this.GetComponent<Rigidbody2D>());

            GunTest.weaponEquipped = true;
            GunTest.weapon = this;
            if(GunTest.weapon == null)
            {
                throw new System.Exception("Weapon failed to Equip, GunTestweapon = null");
            }
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

}