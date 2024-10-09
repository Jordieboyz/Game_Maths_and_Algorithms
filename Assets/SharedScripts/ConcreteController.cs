using UnityEngine;

public class ConcreteController : MonoBehaviour {

    public ParticleSystem particleEffect;
    
    static public bool isAttached = false;

    void OnTriggerEnter(Collider other) {
        if(isAttached == false) {
            isAttached = true;
            particleEffect.transform.position = other.transform.position + other.GetComponent<BoxCollider>().center;
            particleEffect.Play();
        }
    }
}
