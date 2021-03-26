using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLerp : MonoBehaviour
{
    public Transform projectilePos;
    public int lerpDuration = 45;
    int elapsedFrames = 0;

    void Update()
    {
        if(projectilePos == null)
        {
            GameObject.Destroy(this.gameObject);
            return;
        }
        float lerpRatio = Mathf.Clamp01((float)elapsedFrames / lerpDuration);
        Vector3 newPos = Vector3.Lerp(this.transform.position, projectilePos.position, lerpRatio);
        this.transform.position = newPos;
        elapsedFrames += 1;
    }
}
