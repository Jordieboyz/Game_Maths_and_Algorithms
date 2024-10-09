using UnityEngine;

public class ConcreteController : MonoBehaviour {

    public ParticleSystem particleEffect;
    
    static public bool isAttached = false;

    void Update(){
        Collider other = IsObjectTouchingAttachPoint(GetComponent<SphereCollider>());
        if(other != null && other.name == "Hook"){
            if(isAttached == false) {
                isAttached = true;
                particleEffect.transform.position = other.transform.position + other.GetComponent<BoxCollider>().center;
                particleEffect.Play();
            }
        }
    }

    // The "OnTriggerEnter" function didn't work for me, so I created my own.
    private Collider IsObjectTouchingAttachPoint(SphereCollider attachPoint) {
        Collider[] hitColliders = Physics.OverlapSphere(attachPoint.transform.position, attachPoint.radius);

        foreach (Collider collider in hitColliders) {
            // Ignore the collider attached to this GameObject
            if (collider.gameObject != gameObject) {
                return collider;
            }
        }
        return null;
    }
}
