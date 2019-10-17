using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollManager : MonoBehaviour
{
//  public static RagdollManager body;
//  public GameObject newBod;
//  public bool dying = true;
    public bool dead;
    public Transform start;
    public float rotateSpeed;

    private PlayerController controller;
    

    // Start is called before the first frame update
    void Start()
    {
        start = FindObjectOfType<Starter>().transform;
        controller = GetComponentInChildren<PlayerController>();
        
        Starter.start.delay = false;

        for (int i = 0; i < transform.childCount; ++i)
        {
            //transform.GetChild(i).gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            print("For loop: " + transform.GetChild(i));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.grounded)
        {
            CorrectRotation();
        }
    }

    public void CreateRagdoll()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.GetComponent<PlayerController>() != null)
            {
                Destroy(transform.GetChild(i).gameObject.GetComponent<PlayerController>());
                Debug.Log("DESTROYING " + transform.GetChild(i));
            }
            transform.GetChild(i).gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            transform.GetChild(i).gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
        }
        
        Starter.start.newChild();
    }

    private void CorrectRotation()
    {
        if (Mathf.Abs(Quaternion.Angle(Quaternion.identity, transform.rotation)) <= 2)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, Time.time * rotateSpeed);
        }
    }
}
