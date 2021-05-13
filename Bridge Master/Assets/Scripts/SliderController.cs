using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public Slider PathSlider;
    public Slider PathSlider2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PathSlider.value = transform.position.z;
        if (PathSlider2 != null)
        {
        PathSlider2.value = transform.position.z;
        }
    }
}
