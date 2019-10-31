using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltAiming : MonoBehaviour
{
    // Derived from the script made by Annamaria Koshy
    // Modified by Carsen Decker

    public Transform armL, armR, armLU, armRU;
//    public float distAL, distAR, distALU, distARU;
    public Transform shoulderL, shoulderR;
    public float RotationSpeed;
    [Space(20)] 
    public HingeJoint2D RightArm;
    public HingeJoint2D LeftArm;
    public HingeJoint2D LowerRightArm, LowerLeftArm;
    
    [Space(20)]
    public PlayerController pc;

    private Quaternion lookRotation;
    private Vector3 direction;


    // Start is called before the first frame update
    void Start()
    {
        pc = GetComponentInChildren<PlayerController>();
//        distAR = Vector3.Distance(armR.position, shoulderR.position);
//        distAL = Vector3.Distance(armL.position, shoulderL.position);
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
                    RightArm.useLimits = false;
                    LowerRightArm.useLimits = false;
                }
            }
            else
            {
                RightArm.useLimits = false;
                LowerRightArm.useLimits = false;
                RightArm.useMotor = false;
            }
        }

    }

    void MotorizeArm()
    {
        RightArm.useLimits = true;
        LowerRightArm.useLimits = true;
        RightArm.useMotor = true;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        JointAngleLimits2D limits = new JointAngleLimits2D();
        Vector3 dir = (mousePos - shoulderR.transform.position).normalized;
        float angle = Vector3.SignedAngle(RightArm.transform.right, dir, Vector3.back);
        limits.max = angle;
        limits.min = angle - 5;
        
        JointAngleLimits2D limitsLower = new JointAngleLimits2D();
        Vector3 dirLower = (mousePos - shoulderR.transform.position);
        float angleLower = Vector3.SignedAngle(LowerRightArm.transform.right, dirLower, Vector3.back);
        limitsLower.max = angleLower;
        limitsLower.min = angleLower - 5;


//
        Debug.Log("Angle:" + Vector3.Angle(dir, RightArm.transform.right));
        Debug.DrawRay(RightArm.transform.position, RightArm.transform.right, Color.blue);
        Debug.DrawRay(RightArm.transform.position, dir, Color.red);
        
        if (Vector3.Angle(RightArm.transform.right, dir) > 5)
        {
            RightArm.limits = limits;
        }

        if (Vector3.Angle(LowerRightArm.transform.right, dirLower) > 5)
        {
            LowerRightArm.limits = limitsLower;
        }

    }
    
}
