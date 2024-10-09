using UnityEngine;

public class CableController : MonoBehaviour {

    public GameObject hook;
    public GameObject trolley;

    private LineRenderer cable;
    private PositionController softParent;

    readonly private float cableSpeed = 10f;
    private readonly float lineOffset = 0.25f;
    private float hookColliderOffsetY;
    [SerializeField] private float delay = 1.0f;

    // Start is called before the first frame update
    private void Start()
    {
        cable = GetComponent<LineRenderer>();
        hookColliderOffsetY = hook.GetComponent<BoxCollider>().center.y;
        softParent = GetComponent<PositionController>();
    }

    // Update is called once per frame
    private void Update()
    {
        RenderLine();

        if(StateMachine.isCurrentState(State.adjustCable)) {
            if (ConcreteController.isAttached == true) {
                StateMachine.NextState();
            } else {
                AdjustCable(StateMachine.concreteAttachPos-new Vector3(0,hookColliderOffsetY,0));
                softParent.relativePos = softParent.parent.InverseTransformPoint(transform.position);
            }
        } else if(StateMachine.isCurrentState(State.pickupConcrete)) {
            if(transform.position.y >= trolley.transform.position.y+hookColliderOffsetY) {
                delay = 1.0f;
                StateMachine.NextState();
            } else {
                delay -= Time.deltaTime;
                if(delay <= 0) {
                    Vector3 moveTowards = Vector3.MoveTowards(transform.position, trolley.transform.position, cableSpeed * Time.deltaTime);
                    
                    transform.position = moveTowards;
                    StateMachine.concrete.transform.position = moveTowards+new Vector3(0,hookColliderOffsetY,0);

                    softParent.relativePos = softParent.parent.InverseTransformPoint(transform.position);
                }
            }
        }
    }

    private void RenderLine(){
        float delta = trolley.transform.position.y-transform.position.y;
        
        cable.SetPosition(0, new Vector3(-lineOffset, 0, 0));
        cable.SetPosition(1, new Vector3(-lineOffset, delta, 0));
        cable.SetPosition(2, new Vector3(lineOffset, delta, 0));
        cable.SetPosition(3, new Vector3(lineOffset, 0, 0));
    }

    private void AdjustCable(Vector3 target) {
        // // lerp between 10==min and trolley height
        float minY = 10;
        float maxY = trolley.transform.position.y;
        float targetY = StateMachine.concreteAttachPos.y;
        Vector3 localTarget = target;

        if(targetY > maxY) {
            localTarget = trolley.transform.position+new Vector3(0,hookColliderOffsetY,0);
        } else if(targetY < minY) {
            localTarget = new Vector3(transform.position.x, minY+hookColliderOffsetY, transform.position.z);
        }


        Vector3 newPos = Vector3.MoveTowards(transform.position, localTarget, cableSpeed * Time.deltaTime);

        // If the position of the cable did not change since the last update and it didn't reach the target, 
        // reset the Statemachine with a new position for the concrete
        if(newPos == transform.position && transform.position != target){
            StateMachine.resetStateMachine();
        }
        transform.position = newPos;
    }
}

