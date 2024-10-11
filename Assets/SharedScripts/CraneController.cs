using System.Collections;
using System.Collections.Generic;
using GameMath.UI;
using UnityEngine;
using UnityEngine.UI;

public class CraneController : MonoBehaviour
{
    public HoldableButton leftKey;
    public HoldableButton rightKey;

    private float rotSpeed = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(leftKey.IsHeldDown){
            transform.Rotate(new Vector3(0, -rotSpeed, 0));
        }
        else if(rightKey.IsHeldDown){
            transform.Rotate(new Vector3(0, +rotSpeed, 0));
        }
    }
}
