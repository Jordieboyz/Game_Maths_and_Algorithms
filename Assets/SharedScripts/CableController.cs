using UnityEngine;
using UnityEngine.UI;

public class CableController : MonoBehaviour
{
    public Slider cableSlider;
 
 
    private float cableSpeed = .5f;
    private Vector3 newPos;
    private float[] minMaxHeight = {0.1f, 2.3f};

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        newPos = Vector3.Lerp(new Vector3(0, minMaxHeight[1], 0), new Vector3(0, minMaxHeight[0], 0), cableSlider.value);
        transform.localScale += new Vector3(0, (newPos.y-transform.localScale.y)* cableSpeed * Time.deltaTime , 0);
    }


}
