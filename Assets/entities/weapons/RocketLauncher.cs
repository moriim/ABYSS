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
    public float primaryFireDelay {get; set;} = 0.8f;
    public float secondaryFireDelay {get; set;} = 0.8f;
    public GameObject owner {get; set;}
    public GameObject ownerCam {get; set;}
    public Transform barrelEndPoint;
    public MeshRenderer gunModel;

    public float lastPrimaryShot {get; set;}
    public float lastSecondaryShot {get; set;}

    public float projectileSpeed;

    public void Awake()
    {
        lastPrimaryShot = -primaryFireDelay;
    }
    public void FixedUpdate()
    {

    }
    public void OnPrimaryDown()
    {
        if(Time.time > lastPrimaryShot + primaryFireDelay)
        {
            GameObject rocket = Instantiate(rocketPrefab, ownerCam.transform.position + ownerCam.transform.forward, ownerCam.transform.rotation);
            rocket.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * projectileSpeed);
            rocket.GetComponent<RocketProjectile>().owner = owner;
            rocket.GetComponent<RocketProjectile>().rocketVis.transform.position = barrelEndPoint.position;
            rocket.GetComponent<RocketProjectile>().rocketVis.transform.rotation = barrelEndPoint.rotation;
            lastPrimaryShot = Time.time;
        }
    }
    public void OnPrimaryUp()
    {

    }
    public void OnSecondaryDown()
    {
        if(Time.time > lastPrimaryShot + primaryFireDelay)
        {
            GameObject rocket = Instantiate(grenadePrefab, ownerCam.transform.position + ownerCam.transform.forward, ownerCam.transform.rotation);
            rocket.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * projectileSpeed * 0.5f);
            rocket.GetComponent<GrenadeProjectile>().owner = owner;
            lastPrimaryShot = Time.time;
        }
    }
    public void OnSecondaryUp()
    {

    }
    public void Unequip()
    {
        Debug.Log("Unequipped Rocket Launcher");
        gunModel.enabled = false;
    }
    public void Equip()
    {
        Debug.Log("Equipped Rocket Launcher");
        gunModel.enabled = true;
    }

}
