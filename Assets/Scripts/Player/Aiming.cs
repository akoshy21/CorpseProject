using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming : MonoBehaviour
{
    // Annamaria Koshy

    public Transform armOne, armTwo, armOneUpper, armTwoUpper;
    public float RotationSpeed;

    private Quaternion lookRotation;
    private Vector3 direction;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        WhereToPoint(armOne);
        WhereToPoint(armTwo);

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
        //direction = (Input.mousePosition - t.position).normalized;

        ////create the rotation we need to be in to look at the target
        //lookRotation = Quaternion.LookRotation(direction);

        ////rotate us over time according to speed until we are in the required rotation
        //t.rotation = Quaternion.Slerp(t.rotation, lookRotation, Time.deltaTime * RotationSpeed);
        //Debug.Log("ROTATING");

        float angleChange = Vector3.Angle(t.GetComponent<HingeJoint2D>().anchor, Input.mousePosition);
        float totalAng = t.GetComponent<HingeJoint2D>().jointAngle + angleChange;
        JointMotor2D motor = t.GetComponent<HingeJoint2D>().motor;

        if (angleChange > 30)
        {
            t.GetComponent<HingeJoint2D>().useMotor = true;
            if (totalAng > t.GetComponent<HingeJoint2D>().jointAngle)
            {
                motor.motorSpeed = RotationSpeed;
            }
            else
            {
                motor.motorSpeed = -RotationSpeed;
            }
        }
        else
        {
            t.GetComponent<HingeJoint2D>().useMotor = false;
        }

        t.GetComponent<HingeJoint2D>().motor = motor;
    }
}
