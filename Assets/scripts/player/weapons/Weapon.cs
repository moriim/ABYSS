using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int startingAmmo;
    public int maxAmmo;
    public int ammo;
    public float fireDelay;
    public GameObject owner;
    public GameObject ownerCam;

    protected float lastShot;

    void Awake()
    {
        lastShot = -fireDelay;
    }

    void Update()
    {
        
    }

    public virtual void Primary()
    {

    }
    
    public virtual void Secondary()
    {

    }

    public virtual void Unequip()
    {

    }
    
    public virtual void Equip()
    {

    }
}
