using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming : MonoBehaviour
{
    // Annamaria Koshy

    public Transform armOne, armTwo, armOneUpper, armTwoUpper;
    public float distAO;
    public Transform shoulderOne, shoulderTwo;
    public float RotationSpeed;

    private Quaternion lookRotation;
    private Vector3 direction;


    // Start is called before the first frame update
    void Start()
    {
        distAO = Vector3.Distance(armOne.position, shoulderOne.position);
    }

    // Update is called once per frame
    void Update()
    {

        WhereToPoint(armOne);

        ///* if(player != dead) */
        //armOneUpper.RotateAround(armOneUpper.GetComponent<HingeJoint2D>().anchor,
        //    Vector3.forward,
        //    Vector3.Angle(armOneUpper.GetComponent<HingeJoint2D>().anchor, Input.mousePosition) * Time.deltaTime * RotationSpeed);
        //armTwoUpper.RotateAround(armTwoUpper.GetComponent<HingeJoint2D>().anchor,
        //    Vector3.forward,
        //    Vector3.Angle(armTwo.GetComponent<HingeJoint2D>().anchor, Input.mousePosition) * Time.deltaTime * RotationSpeed);

    }

    void WhereToPoint(Transform t)
    {
        ////find the vector pointing from our position to the target
        direction = (Input.mousePosition - t.position).normalized;

        ////create the rotation we need to be in to look at the target
        //lookRotation = Quaternion.LookRotation(direction);

        ////rotate us over time according to speed until we are in the required rotation
        //t.rotation = Quaternion.Slerp(t.rotation, lookRotation, Time.deltaTime * RotationSpeed);
        //Debug.Log("ROTATING");

        float angleChange = Vector3.Angle(t.position, Input.mousePosition);
        
        t.transform.position = shoulderOne.position + (direction * distAO);

        Debug.DrawRay(t.position, direction, Color.red);

        Quaternion ro = Quaternion.Euler(new Vector3(0, 0, -angleChange));
//        ro.eulerAngles = new Vector3(0, 0, -angleChange);
        t.rotation = ro;

//        ro.ro.FightThePower();

    }
}
