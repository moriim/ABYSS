using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeExplosion : MonoBehaviour
{
    public GameObject owner;
    public float radius;
    public float forceConstant;
    public AnimationCurve animationCurve;
    

   void Start()
   {
       Explode();
       Destroy(this.gameObject, 1f);
   }

    void Explode()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        for(int i = 0; i < hitColliders.Length; i++)
        {
            if(hitColliders[i].gameObject == owner)
            {
                float forcePercent = Mathf.Clamp01(Vector3.Distance(owner.transform.position, transform.position));
                float forceMult = animationCurve.Evaluate(forcePercent);

                Vector3 forceDir = Vector3.Normalize(owner.transform.position - transform.position);

                owner.GetComponent<Rigidbody>().AddForce(forceDir * forceMult * forceConstant, ForceMode.VelocityChange);
            }
            else if (hitColliders[i].gameObject.GetComponent<Boid>() != null)
            {
                hitColliders[i].gameObject.GetComponent<Boid>().velocity += Vector3.Normalize(hitColliders[i].transform.position - transform.position) * forceConstant * 2f;
            }
        }
    }
}
