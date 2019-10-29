using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanScript : MonoBehaviour
{
    public GameObject rightTrigger;
    public GameObject leftTrigger;

    public bool pushLeft;

    public bool pushRight;
    // Start is called before the first frame update
    
    //Luke Brockmann
    void Start()
    {
        
        if (pushRight)
        {
            rightTrigger.SetActive(true);
        }

        if (pushLeft)
        {
            leftTrigger.SetActive(true);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
