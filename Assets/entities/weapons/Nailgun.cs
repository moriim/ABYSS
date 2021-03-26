using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nailgun : MonoBehaviour, IWeapon
{
    public GameObject nailPrefab;
    
    public int startingAmmo {get; set;} = 15;
    public int maxAmmo {get; set;} = 50;
    public int ammo {get; set;}
    public float primaryFireDelay {get; set;} = 0.1f;
    public float secondaryFireDelay {get; set;} = 1.25f;
    public GameObject owner {get; set;}
    public GameObject ownerCam {get; set;}
    public Transform barrelEndPoint;
    public MeshRenderer gunModel;

    public float lastPrimaryShot {get; set;}
    public float lastSecondaryShot {get; set;}

    public bool isFiringPrimary;
    public int numShotsInSecondaryFire;
    public float projectileSpeed;
    public float spread; 

    public void Awake()
    {
        lastPrimaryShot = -primaryFireDelay;
        isFiringPrimary = false;
    }
    public void FixedUpdate()
    {
        if(isFiringPrimary && Time.time > lastPrimaryShot + primaryFireDelay
                           && Time.time > lastSecondaryShot + secondaryFireDelay)
        {
            GameObject nail = Instantiate(nailPrefab, ownerCam.transform.position + ownerCam.transform.forward, ownerCam.transform.rotation);
            nail.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * projectileSpeed);
            nail.GetComponent<NailProjectile>().owner = owner;
            nail.GetComponent<NailProjectile>().nailVis.transform.position = barrelEndPoint.position;
            nail.GetComponent<NailProjectile>().nailVis.transform.rotation = barrelEndPoint.rotation;
            lastPrimaryShot = Time.time; 
        }
    }
    public void OnPrimaryDown()
    {
        isFiringPrimary = true;
    }
    public void OnPrimaryUp()
    {
        isFiringPrimary = false;
    }
    public void OnSecondaryDown()
    {
        if(Time.time > lastSecondaryShot + secondaryFireDelay)
        {
            for(int i = 0; i < numShotsInSecondaryFire; i++)
            {
                GameObject nail = Instantiate(nailPrefab, ownerCam.transform.position + ownerCam.transform.forward, ownerCam.transform.rotation);
                nail.GetComponent<NailProjectile>().owner = owner;
                switch(i)
                {
                    case 0:
                        break;
                    case 1:
                        nail.transform.Rotate(spread, 0, 0);
                        break;
                    case 2:
                        nail.transform.Rotate(-spread, 0, 0);
                        break;
                    case 3:
                        nail.transform.Rotate(spread, spread, 0);
                        break;
                    case 4:
                        nail.transform.Rotate(spread, -spread, 0);
                        break;
                    case 5:
                        nail.transform.Rotate(-spread, spread, 0);
                        break;
                    case 6:
                        nail.transform.Rotate(-spread, -spread, 0);
                        break;
                    case 7:
                        nail.transform.Rotate(0, spread, 0);
                        break;
                    case 8:
                        nail.transform.Rotate(0, -spread, 0);
                        break;
                }
                nail.GetComponent<Rigidbody>().velocity = nail.transform.TransformDirection(Vector3.forward * projectileSpeed);
            }
            lastSecondaryShot = Time.time;
        }
        
    }
    public void OnSecondaryUp()
    {

    }
    public void Unequip()
    {
        Debug.Log("Unequipped Nailgun");
        gunModel.enabled = false;
    }
    public void Equip()
    {
        Debug.Log("Equipped Nailgun");
        gunModel.enabled = true;
    }

}
