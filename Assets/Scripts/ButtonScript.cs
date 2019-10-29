using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public bool isLever;
    public bool buttonActive;
    private List<GameObject> entryList;
    public bool stayOn;
    // Start is called before the first frame update
    void Start()
    {
        entryList = new List<GameObject>();  
    }

    // Update is called once per frame
    void Update()
    {
       
       
        if (entryList.Count > 0)
        {
            Debug.Log("List go");
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
                else
                {
                    buttonActive = false;
                }
            }
        }
        else
        {
            buttonActive = false;
        }

     

        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
     
        entryList.Add(other.gameObject);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
      Debug.Log(other);
        

        entryList.Remove(other.gameObject);
    }

   
}
