using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IWeapon
{
    int startingAmmo {get; set;}
    int maxAmmo {get; set;}
    int ammo {get; set;}
    float fireDelay {get; set;}
    GameObject owner {get; set;}
    GameObject ownerCam {get; set;}

    float lastShot {get; set;}

    void Awake();
    void Update();
    void OnPrimaryDown();
    void OnPrimaryUp();
    void OnSecondaryDown();
    void OnSecondaryUp();
    void Unequip();
    void Equip();
}
