using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollManager : MonoBehaviour
{
    public bool dead;
//    public static RagdollManager body;
//    public GameObject newBod;
    public Transform start;

//    public bool dying = true;

    // Start is called before the first frame update
    void Start()
    {
        start = FindObjectOfType<Starter>().transform;
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
//        if (dead)
//        {
//            CreateRagdoll();
//        }
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
        
        Starter.start.bodyCount--;
//        body = null;
        Starter.start.newChild();
    }

//    private void CorrectRotation()
//    {
//        if (transform.rotation != originalRotation)
//        {
//            restoreRotation = true;
//        }
//
//        if (restoreRotation && onGround)
//        {
//            transform.rotation = Quaternion.Lerp(transform.rotation, originalRotation, Time.time * rotateSpeed);
//            //Debug.Log("Rotating " + transform.rotation.eulerAngles);
//            if (Mathf.Abs(Quaternion.Angle(originalRotation, transform.rotation)) <= 2) ;
//            {
//                restoreRotation = false;
//            }
//        }
//    }
}
