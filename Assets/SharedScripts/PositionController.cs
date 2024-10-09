using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionController : MonoBehaviour
{
    public Transform parent;
    private Vector3 relativePos;
    private Quaternion relativeRot;
    
    // Start is called before the first frame update
    void Start()
    {
        relativePos = parent.InverseTransformPoint(transform.position);
        relativeRot = Quaternion.Inverse(parent.rotation) * transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = parent.rotation * relativeRot;
        transform.position = parent.TransformPoint(relativePos);
    }
}
