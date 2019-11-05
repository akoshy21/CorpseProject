using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    /*
     * @author Kate Howell
     * 
     * this class extends the abstract Weapon class, with specific implementation for type Pistol
     * 
     */

    public GameObject pistolBulletPrefab;
    private PistolBullet pistolBullet;

    public Transform bulletSpawn;

    [SerializeField, Tooltip("Delay between shots")]
    public float shotDelay;

    private bool delayed;

    void Awake()
    {
        pistolBullet = pistolBulletPrefab.GetComponent<PistolBullet>();
        if(pistolBullet == null)
        {
            throw new UnassignedReferenceException("wrong type of bullet assigned to Pistol");
        }
    }

    public override void Attack()
    {
        if (automated || (equipped && !delayed))
        {
            //currently not being affected by players position
            PistolBullet b = Instantiate(pistolBulletPrefab, bulletSpawn.position, bulletSpawn.rotation).GetComponent<PistolBullet>();

            //**ADD CALCUATION FOR BULLET DIRECTION***
            if (pointingRight)
            {
                b.SetIntialVelocity(new Vector3(b.bulletSpeed, 0, 0));
            }
            else
            {
                b.SetIntialVelocity(new Vector3(b.bulletSpeed, 0, 0));
            }


            delayed = true;
            StartCoroutine(Delay(shotDelay));
        } 
    }

    private IEnumerator Delay(float delayInt)
    {
        yield return new WaitForSeconds(delayInt);
        delayed = false;
    }

}
