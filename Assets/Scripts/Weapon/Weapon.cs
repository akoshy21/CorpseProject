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
     *
     * Small fixes by Carsen Decker ;)
     */

    [SerializeField, Tooltip("If gun is equipped to a Player")]
    public bool equipped;

    [SerializeField, Tooltip("gun doesnt equip, shoots in intervals")]
    public bool automated;

    [SerializeField, Tooltip("rate in seconds new bullets will spawn if automated")]
    public float automatedBulletSpawnRate = 3f;

    public bool attacking;

    public bool pointingRight = true;

    public abstract void Attack();

    private PlayerController controller;

    public bool buttonActivated;
    public GameObject buttonOrLever;
    public int buttonShootDelay;

    public void Update()
    {
        if (automated && !attacking)
        {
            StartCoroutine(automatedAttack());
            attacking = true;
        }

        if (buttonActivated)
        {
            if (buttonOrLever.GetComponent<ButtonScript>().buttonActive && !attacking)
            {
                Attack();
                attacking = true;
                StartCoroutine(buttonAttack());
            }
        }
    }

    //function equips and unequips weapon
    public void Equip(GameObject player)
    {
//        PlayerController controller;
        Transform currentTransform = player.transform;
        
        if (player.transform.parent == null)
        {
            throw new System.Exception("Weapon failed to Equip, parent = null");
        }

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
            
            foreach (var collider in GetComponentsInChildren<Collider2D>())
            {
                collider.enabled = true;
            }
        }
        else
        {
            transform.SetParent(controller.gunLocation);
            transform.position = controller.gunLocation.position;
            transform.localRotation = Quaternion.Euler(0, 0, 0);

            //turns off the colliders so it doesn't collide with the player and mess with it
            foreach (var collider in GetComponentsInChildren<Collider2D>())
            {
                collider.enabled = false;
            }
//            transform.SetPositionAndRotation(controller.gunLocation.position, Quaternion.Euler(0, 0, 0));

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
        if (!equipped && !automated)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Equip(collision.gameObject);
            }
        }
            
    }

    public void gunFacingRight(bool facingRight)
    {
//        SpriteRenderer[] sprites = GetComponents<SpriteRenderer>();
        Vector3 tempScale = transform.localScale;
        if (pointingRight)
        {
            if (!facingRight)
            {
                tempScale.x = tempScale.x * -1;
                pointingRight = false;
                transform.parent = controller.gunLocationLeft.transform;
                transform.position = controller.gunLocationLeft.transform.position;
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
        }
        else
        {
            if (facingRight)
            {
                tempScale.x = tempScale.x * -1;
                pointingRight = true;
                transform.parent = controller.gunLocationRight.transform;
                transform.position = controller.gunLocationRight.transform.position;
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
        }

        transform.localScale = tempScale;
    }

    public IEnumerator automatedAttack()
    {
        yield return new WaitForSeconds(automatedBulletSpawnRate);
        Attack();
        //print("Attack");
        StartCoroutine(automatedAttack());
        
    }

    public IEnumerator buttonAttack()
    {
        yield return new WaitForSeconds(buttonShootDelay);
        attacking = false;

    }

}
