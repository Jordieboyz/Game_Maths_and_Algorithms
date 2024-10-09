using UnityEngine;

public class CraneController : MonoBehaviour {

    readonly private float rotationSpeed = 1f;

    private void Update() {   
        if(StateMachine.isCurrentState(State.rotateCrane)) {
            
            // Calculate the Angle between the current position and the target
            float desiredAngle = Utils.calcAngle(transform.position, StateMachine.concreteAttachPos);

            // Make sure the rotation doesn't stop before we are within the allowed error
            if(Mathf.Abs(Mathf.DeltaAngle(desiredAngle, transform.rotation.eulerAngles.y)) <= .5f) {
                StateMachine.NextState();
            } else {
                // Fancy one-liner to perform the rotation 
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, desiredAngle, 0), rotationSpeed*Time.deltaTime);
            }
        }
    }
}
