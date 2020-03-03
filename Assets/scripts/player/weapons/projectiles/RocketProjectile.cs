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
    private void OnCollisionEnter(Collision other)
    {
        GameObject explosion = Instantiate(rocketExplosion, transform.position, Quaternion.identity);
        explosion.GetComponent<RocketExplosion>().owner = owner;
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        GameObject explosion = Instantiate(rocketExplosion, transform.position, Quaternion.identity);
        explosion.GetComponent<RocketExplosion>().owner = owner;
        Destroy(this.gameObject);
    }
}
