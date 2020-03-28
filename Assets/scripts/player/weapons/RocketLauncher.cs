using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : MonoBehaviour, IWeapon
{
    public GameObject rocketPrefab;
    public GameObject grenadePrefab;
    
    public int startingAmmo {get; set;} = 15;
    public int maxAmmo {get; set;} = 50;
    public int ammo {get; set;}
    public float fireDelay {get; set;} = 1f;
    public GameObject owner {get; set;}
    public GameObject ownerCam {get; set;}

    public float lastShot {get; set;}

    public void Awake()
    {
        lastShot = -fireDelay;
    }
    public void Update()
    {

    }
    public void OnPrimaryDown()
    {
        if(Time.time > lastShot + fireDelay)
        {
            GameObject rocket = Instantiate(rocketPrefab, ownerCam.transform.position + ownerCam.transform.forward, ownerCam.transform.rotation);
            rocket.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * 50f);
            rocket.GetComponent<RocketProjectile>().owner = owner;
            lastShot = Time.time;
        }
    }
    public void OnPrimaryUp()
    {

    }
    public void OnSecondaryDown()
    {
        if(Time.time > lastShot + fireDelay)
        {
            GameObject rocket = Instantiate(grenadePrefab, ownerCam.transform.position + ownerCam.transform.forward, ownerCam.transform.rotation);
            rocket.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * 25f);
            rocket.GetComponent<GrenadeProjectile>().owner = owner;
            lastShot = Time.time;
        }
    }
    public void OnSecondaryUp()
    {

    }
    public void Unequip()
    {

    }
    public void Equip()
    {

    }

}
