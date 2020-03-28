using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IDamageable
{
    float health {get; set;}
    void Die();
    void Hit(float damage);
    GameObject gObj {get; set;}
}
