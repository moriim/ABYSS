using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IWeapon
{
    int startingAmmo {get; set;}
    int maxAmmo {get; set;}
    int ammo {get; set;}
    float primaryFireDelay {get; set;}
    float secondaryFireDelay {get; set;}
    GameObject owner {get; set;}
    GameObject ownerCam {get; set;}

    float lastPrimaryShot {get; set;}
    float lastSecondaryShot {get; set;}

    void Awake();
    void FixedUpdate();
    void OnPrimaryDown();
    void OnPrimaryUp();
    void OnSecondaryDown();
    void OnSecondaryUp();
    void Unequip();
    void Equip();
}
