using System;
using UnityEngine;

// "Global" enum to handle the different states within the program
public enum State { WAIT_FOR_INPUT = 0, rotateCrane, adjustTrolley, lowerCable, pickupConcrete, respawnConcrete };

public class StateMachine : MonoBehaviour {

    static public GameObject concrete;
    static public Vector3 concreteAttachPos;

    // Core of the StateMachine
    static private State currentState;
    static public Func<State, bool> isCurrentState = (state) => currentState == state;
    static public Action NextState = () => currentState = 
            (currentState == State.respawnConcrete) ? currentState = State.WAIT_FOR_INPUT : ++currentState;


    private void Update() {
        // Instead of Invoking "Start()" perform assignment the first time we call "Update()"
        if(concrete == null){ concrete = GameObject.Find("Concrete"); }

        if(isCurrentState(State.WAIT_FOR_INPUT)){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out RaycastHit hit)) {
                if(hit.transform.name == concrete.name) {
                    if(Input.GetMouseButtonDown(0)) {
                        concreteAttachPos = concrete.GetComponent<SphereCollider>().transform.position;
                        NextState();
                    }
                }
            }
        }
    }
}