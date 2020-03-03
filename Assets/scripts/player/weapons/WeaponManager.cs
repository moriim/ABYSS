using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public List<Weapon> weapons;
    public Weapon equippedWeapon;
    private int equippedWeaponIndex;
    public int startingWeaponIndex;
    public GameObject playerCam;

    void Awake()
    {
        playerCam = transform.Find("camera").gameObject;
        weapons.Add(transform.Find("camera/weapons/rocketlauncher").GetComponent<RocketLauncher>());
        equippedWeaponIndex = startingWeaponIndex;
        equippedWeapon = weapons[equippedWeaponIndex];
        foreach(Weapon w in weapons)
        {
            w.owner = transform.root.gameObject;
            w.ownerCam = playerCam;
        }
    }
    void Start()
    {
        
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

    public void FirePrimary()
    {
        equippedWeapon.Primary();
    }

    public void FireSecondary()
    {
        equippedWeapon.Secondary();
    }
}
