using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    public Transform target;    
    public Vector3 defaultDistance;    
    public float speedDamping;

    public Vector3 velocity = Vector3.one;

    private Transform thisTransform;

    private void Awake()
    {
        thisTransform = transform;
    }

    private void FixedUpdate()
    {
        if (!target)
            return;

        SmoothPosition();

    }
    private void SmoothPosition()
    {
        Vector3 toPos = target.position + (target.rotation * defaultDistance);
        Vector3 curPos = Vector3.SmoothDamp(thisTransform.position, toPos, ref velocity, speedDamping);
        thisTransform.position = curPos;
        thisTransform.LookAt(target, target.up);
    }

}
