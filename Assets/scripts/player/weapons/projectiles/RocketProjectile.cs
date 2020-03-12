using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketProjectile : MonoBehaviour
{
    public GameObject rocketExplosion;
    public GameObject owner;

    void Awake()
    {
        Destroy(this.gameObject, 10f);
    }

    void OnDestroy()
    {
        GameObject explosion = Instantiate(rocketExplosion, transform.position, Quaternion.identity);
        explosion.GetComponent<RocketExplosion>().owner = owner;
    }

    private void OnCollisionEnter(Collision other)
    {
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
    }
}
