using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : Weapon
{
    public GameObject rocketPrefab;
    
    public override void Primary()
    {
        if(Time.time > lastShot + fireDelay)
        {
            GameObject rocket = Instantiate(rocketPrefab, ownerCam.transform.position, ownerCam.transform.rotation);
            rocket.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * 50f);
            rocket.GetComponent<RocketProjectile>().owner = owner;
            lastShot = Time.time;
        }
    }
        
    
    public override void Secondary()
    {

    }

    public override void Unequip()
    {

    }
    
    public override void Equip()
    {

    }
}
