using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    List<IWeapon> weapons;
    IWeapon equippedWeapon;
    private int equippedWeaponIndex;
    public int startingWeaponIndex;
    public GameObject playerCam;

    void Awake()
    {
        weapons = new List<IWeapon>();
        playerCam = transform.Find("camera").gameObject;
        weapons.Add(transform.Find("camera/weapons/rocketlauncher").GetComponent<IWeapon>());
        weapons.Add(transform.Find("camera/weapons/nailgun").GetComponent<IWeapon>());
        weapons.Add(transform.Find("camera/weapons/lightninggun").GetComponent<IWeapon>());
        equippedWeaponIndex = startingWeaponIndex;
        equippedWeapon = weapons[equippedWeaponIndex];
        foreach(IWeapon w in weapons)
        {
            w.owner = transform.root.gameObject;
            w.ownerCam = playerCam;
        }
    }
    void Start()
    {
        weapons[equippedWeaponIndex].Equip();
    }
    void Update()
    {
        
    }

    public void ChangeWeapons(bool direction)
    {
        if(direction)
        {
            if(equippedWeaponIndex+1 >= weapons.Count)
            {
                equippedWeaponIndex = 0;
            }
            else
            {
                equippedWeaponIndex += 1;
            }
        }
        else
        {
            if(equippedWeaponIndex-1 < 0)
            {
                equippedWeaponIndex = weapons.Count-1;
            }
            else
            {
                equippedWeaponIndex -= 1;
            }
        }
        equippedWeapon.Unequip();
        equippedWeapon = weapons[equippedWeaponIndex];
        equippedWeapon.Equip();
    }

    public void PrimaryDown()
    {
        equippedWeapon.OnPrimaryDown();
    }

    public void SecondaryDown()
    {
        equippedWeapon.OnSecondaryDown();
    }

    public void PrimaryUp()
    {
        equippedWeapon.OnPrimaryUp();
    }

    public void SecondaryUp()
    {
        equippedWeapon.OnSecondaryUp();
    }
}
