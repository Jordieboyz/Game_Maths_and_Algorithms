using UnityEngine;

public class TrolleyController : MonoBehaviour {

    public GameObject trolleyMax;
    public GameObject trolleyMin;

    private PositionController softParent;
    private readonly float trolleySpeed = 10f;
    private float[] minMaxSpawnDistance= {};
    
    private void Start(){
        softParent = GetComponent<PositionController>();
        minMaxSpawnDistance = new float[2]{
                Utils.dist2DXZ(softParent.parent.transform.position, trolleyMin.transform.position),
                Utils.dist2DXZ(softParent.parent.transform.position, trolleyMax.transform.position)
        };
    }

    private void Update() {      
        if(StateMachine.isCurrentState(State.adjustTrolley)) {
            Vector3 conCollider = StateMachine.concreteAttachPos;

            if(Utils.dist2DXZ(conCollider, transform.position) <= .5f) {
                StateMachine.NextState();
            } else {
                MoveTrolley(new Vector3(conCollider.x, transform.position.y, conCollider.z));                
                softParent.relativePos = softParent.parent.InverseTransformPoint(transform.position);
            }
        }
        if(StateMachine.isCurrentState(State.respawnConcrete)) {
            if(ConcreteController.isAttached == true) {
                ConcreteController.isAttached = false;
                StateMachine.concrete.transform.position = GetRandPosInTrolleyRange();
            } else {
                StateMachine.NextState();
            }
        }   
    }


    private void MoveTrolley(Vector3 target) {
        // This only works if softParent == Tower Crane, but because we know this is the case, we can save an additional GameObject.
        Vector3 towerCranePos = softParent.parent.transform.position;

        float targetDist    = Utils.dist2DXZ(towerCranePos, StateMachine.concreteAttachPos);
        float minDist       = Utils.dist2DXZ(towerCranePos, trolleyMin.transform.position);
        float maxDist       = Utils.dist2DXZ(towerCranePos, trolleyMax.transform.position);

        // Handle areas outside the range of the trolley
        if     (targetDist < minDist) { target = trolleyMin.transform.position; } 
        else if(targetDist > maxDist) { target = trolleyMax.transform.position; }

        transform.position = Vector3.MoveTowards(transform.position, target, trolleySpeed * Time.deltaTime);
    }
    
    private Vector3 GetRandPosInTrolleyRange() {
        // Randomize an angle between 0 and 360 degrees
        float angle = Random.Range(0, 2*Mathf.PI);

        // Randomize the radius between min and max spawn distance
        float radius = Random.Range(minMaxSpawnDistance[0], minMaxSpawnDistance[1]); 

        // Randomize the height between min and max height
        float y = Random.Range(10, 20);

        // Convert Polar coordinates to Cartesian coordinates
        float x = Mathf.Cos(angle) * radius;
        float z = Mathf.Sin(angle) * radius;

        return new Vector3(x, y, z);
    }
}
