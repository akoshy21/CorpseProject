using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTest : MonoBehaviour
{
    public bool weaponEquipped;
    public Weapon weapon;
    public GameObject gunlocation;
    // Start is called before the first frame update
    void Start()
    {
        gunlocation = GameObject.FindGameObjectWithTag("GunLocation");
        if(gunlocation == null)
        {
            throw new System.Exception("Gun Location is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (weaponEquipped)
            {
                weapon.Attack();
            }
        } 
    }
}
