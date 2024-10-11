using UnityEngine;

public class PositionController : MonoBehaviour {
    
    public Transform parent;
    public Vector3 relativePos;
    public Quaternion relativeRot;
    
    // Start is called before the first frame update
    private void Start() {
        relativePos = parent.InverseTransformPoint(transform.position);
        relativeRot = Quaternion.Inverse(parent.rotation) * transform.rotation;
    }

    // Update is called once per frame
    private void Update() {
        transform.SetPositionAndRotation(parent.TransformPoint(relativePos), parent.rotation * relativeRot);
    }
}
