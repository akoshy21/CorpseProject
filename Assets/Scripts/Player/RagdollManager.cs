using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollManager : MonoBehaviour
{

// Annamaria Koshy

    public bool dead;
    public float rotateSpeed;

    private PlayerController controller;
    private List<GameObject> children = new List<GameObject>();

    

    void Awake()
    {
        controller = GetComponentInChildren<PlayerController>();
        GetAllChildren(transform);
    }


    public void CreateRagdoll()
    {

        foreach (var child in children)
        {
            if (child.gameObject.GetComponent<PlayerController>() != null)
            {
                Destroy(child.gameObject.GetComponent<PlayerController>());
            }

            if (child.gameObject.GetComponent<Rigidbody2D>() != null)
            {
                child.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                child.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
            }

            child.gameObject.tag = "Corpse";
            if (child.gameObject.layer != 0)
            {
                child.gameObject.layer = 0;
            }
        }
        
        Starter.start.newChild();
    }

    private void GetAllChildren(Transform currentTransform)
    {
        foreach (Transform child in currentTransform)
        {
            children.Add(child.gameObject);
            GetAllChildren(child.transform);
        }
    }
    

    private void CorrectRotation()
    {
        if (Mathf.Abs(Quaternion.Angle(Quaternion.identity, transform.rotation)) <= 2)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, Time.time * rotateSpeed);
        }
    }
}
