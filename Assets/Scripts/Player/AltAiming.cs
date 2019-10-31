using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltAiming : MonoBehaviour
{
    // Derived from the script made by Annamaria Koshy
    // Modified by Carsen Decker

    public Transform armL, armR, armLU, armRU;
    public float distAL, distAR, distALU, distARU;
    public Transform shoulderL, shoulderR;
    public float RotationSpeed;
    [Space(20)] 
    public HingeJoint2D RightArmJoint;
    public HingeJoint2D LeftArmJoint;
    
    [Space(20)]
    public PlayerController pc;

    private Quaternion lookRotation;
    private Vector3 direction;


    // Start is called before the first frame update
    void Start()
    {
        pc = GetComponentInChildren<PlayerController>();
        distAR = Vector3.Distance(armR.position, shoulderR.position);
        distAL = Vector3.Distance(armL.position, shoulderL.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
        if (!GameManager.gm.paused)
        {
            if (Input.GetMouseButton(0))
            {
                if (pc.facingRight)
                {
                    MotorizeArm();
//                    WhereToPoint(armR);
                }
                else
                {
//                    WhereToPoint(armL);
                }
            }
            else
            {
                
            }
        }
        ///* if(player != dead) */
        //armOneUpper.RotateAround(armOneUpper.GetComponent<HingeJoint2D>().anchor,
        //    Vector3.forward,
        //    Vector3.Angle(armOneUpper.GetComponent<HingeJoint2D>().anchor, Input.mousePosition) * Time.deltaTime * RotationSpeed);
        //armTwoUpper.RotateAround(armTwoUpper.GetComponent<HingeJoint2D>().anchor,
        //    Vector3.forward,
        //    Vector3.Angle(armTwo.GetComponent<HingeJoint2D>().anchor, Input.mousePosition) * Time.deltaTime * RotationSpeed);

    }

    void MotorizeArm()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        mousePos.z = 0;
        
        JointAngleLimits2D limits = new JointAngleLimits2D();
        Vector3 dir = (RightArmJoint.anchor - (Vector2)mousePos).normalized;
//        limits.max 
//        limits.max = Vector3.SignedAngle(RightArmJoint.anchor, mousePos, Vector3.up);
//        limits.min = Vector3.SignedAngle(RightArmJoint.anchor, mousePos, Vector3.up) + 3;

        RightArmJoint.limits = limits;
    }
    
    void WhereToPoint(Transform t)
    {
        ////find the vector pointing from our position to the target
        direction = (Input.mousePosition - t.position).normalized;

        ////create the rotation we need to be in to look at the target
        //lookRotation = Quaternion.LookRotation(direction);

        float angleChange = Vector3.Angle(t.position, Input.mousePosition);

        if (t.Equals(armL))
        {
            t.position = shoulderL.position + (direction * distAL);
        }
        else if (t.Equals(armR))
        {
            t.position = shoulderR.position + (direction * distAR);
        }
        Debug.DrawRay(t.position, direction, Color.red);

        Quaternion ro = Quaternion.Euler(new Vector3(0, 0, -angleChange));
//        ro.eulerAngles = new Vector3(0, 0, -angleChange);
        t.rotation = ro;

//        ro.ro.FightThePower();

    }
}
