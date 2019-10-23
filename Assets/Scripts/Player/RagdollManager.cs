using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollManager : MonoBehaviour
{
    public bool dead;
    public Transform start;
    public float rotateSpeed;

    private PlayerController controller;
    

    void Awake()
    {
        start = FindObjectOfType<Starter>().transform;
        controller = GetComponentInChildren<PlayerController>();
    }


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
