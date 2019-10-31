using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killBoxFunctions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController controller = other.transform.parent.gameObject.GetComponentInChildren<PlayerController>();
            if(controller != null)
            {
                controller.Die();
            }
            Debug.Log("player fell out");
        }
    }
}
