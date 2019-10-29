using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public bool isLever;
    public bool buttonActive;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isLever == false)
        {
            buttonActive = true;
            Debug.Log("button message sent");
        }

        if (isLever)
        {
            if (Input.GetKey(KeyCode.E))
            {
                buttonActive = true;
                Debug.Log("Lever messge sent");
            }
        }
        
        Debug.Log("Button hit");
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        buttonActive = false;
    }
}
