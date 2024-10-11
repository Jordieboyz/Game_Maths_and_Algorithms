using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using GameMath.UI;
using UnityEngine;
using UnityEngine.UI;

public class TrolleyController : MonoBehaviour
{
    public Slider trolleySlider;
    public GameObject trolleyMax;
    public GameObject trolleyMin;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(trolleyMin.transform.position, trolleyMax.transform.position, trolleySlider.value);
    }

}
