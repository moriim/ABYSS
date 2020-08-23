using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningGun : MonoBehaviour, IWeapon
{
    public LineRenderer lineRenderer;

    public int startingAmmo {get; set;} = 100;
    public int maxAmmo {get; set;} = 200;
    public int ammo {get; set;}
    public float primaryFireDelay {get; set;} = 0.05f;
    public float secondaryFireDelay {get; set;} = 1.2f;
    public GameObject owner {get; set;}
    public GameObject ownerCam {get; set;}

    public float lastPrimaryShot {get; set;}
    public float lastSecondaryShot {get; set;}

    public bool railgunMode;
    public bool isFiringPrimary;
    public LayerMask enemyMask;
    public float primaryDamage;
    public float secondaryDamage;

    private RaycastHit hitInfo;

    public void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lastPrimaryShot = -primaryFireDelay;
        railgunMode = false;
        isFiringPrimary = false;
    }
    public void FixedUpdate()
    {
        if(isFiringPrimary && Time.time > lastPrimaryShot + primaryFireDelay
                           && Time.time > lastSecondaryShot + secondaryFireDelay
                           && !railgunMode)
        {
            lineRenderer.SetPosition(0, ownerCam.transform.position);
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, ownerCam.transform.position + ( ownerCam.transform.forward * 100f));
            if(Physics.Raycast(ownerCam.transform.position, ownerCam.transform.forward, out hitInfo, 100f, enemyMask))
            {
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, hitInfo.point);
                IDamageable enemy = hitInfo.collider.gameObject.GetComponentInParent<IDamageable>();
                if(enemy != null)
                {
                    enemy.Hit(primaryDamage);
                }
                lastPrimaryShot = Time.time;
            }
        }
    }
    public void OnPrimaryDown()
    {
        if(!railgunMode)
        {
            isFiringPrimary = true;
            lineRenderer.enabled = true;
        }
        else
        {
            
        }
    }
    public void OnPrimaryUp()
    {
       isFiringPrimary = false;
       lineRenderer.enabled = false;
    }
    public void OnSecondaryDown()
    {
        railgunMode = true;
    }
    public void OnSecondaryUp()
    {
        railgunMode = false;
    }
    public void Unequip()
    {
        Debug.Log("Unequipped Lightning Gun");
    }
    public void Equip()
    {
        Debug.Log("Equipped Lightning Gun");
    }

}

