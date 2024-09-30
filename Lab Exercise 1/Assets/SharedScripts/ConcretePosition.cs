using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConcretePosition : MonoBehaviour
{
    public ParticleSystem particleEffect;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {}

    void OnTriggerEnter(Collider other){
        particleEffect.transform.position = other.transform.position + other.GetComponent<BoxCollider>().center;
        particleEffect.Play();
    }

    void OnTriggerStay(Collider other)
    {
        transform.position = other.transform.position + other.GetComponent<BoxCollider>().center;
    }
}
