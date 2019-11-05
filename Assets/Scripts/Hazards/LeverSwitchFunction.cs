using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverSwitchFunction : MonoBehaviour
{
    public bool leverActive;

  

    public GameObject connectedHazard;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            
            if (leverActive)
            {
                leverActive = false;
            
            }
            
            if (leverActive == false)
            {
                leverActive = true;
            }
            
        }
    }
}
