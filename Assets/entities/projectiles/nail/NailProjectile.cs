using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NailProjectile : MonoBehaviour
{
    public GameObject nailExplosion;
    public GameObject nailVis;
    public GameObject owner;

    void Awake()
    {
        Destroy(this.gameObject, 10f);
    }

    private void OnCollisionEnter(Collision other)
    {
        GameObject explosion = Instantiate(nailExplosion, transform.position, transform.rotation);
        explosion.GetComponent<NailExplosion>().owner = owner;
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        GameObject explosion = Instantiate(nailExplosion, transform.position, transform.rotation);
        explosion.GetComponent<NailExplosion>().owner = owner;
        Destroy(this.gameObject);
    }
}
