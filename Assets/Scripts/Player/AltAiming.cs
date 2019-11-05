using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltAiming : MonoBehaviour
{
    // Derived from the script made by Annamaria Koshy
    // Modified by Carsen Decker

    public Transform shoulderL, shoulderR;
    [Space(20)] 
    public HingeJoint2D RightArm;
    public HingeJoint2D LeftArm;
    public HingeJoint2D LowerRightArm, LowerLeftArm;
    
    [Space(20)]
    public PlayerController pc;

    private Quaternion lookRotation;
    private Vector3 direction;


    void Start()
    {
        pc = GetComponentInChildren<PlayerController>();
    }

    void Update()
    {
        if (!LevelManager.lm.paused)
        {
            //For now, begins aiming when the mouse is pressed
            if (Input.GetMouseButton(0))
            {
                //Aims with the arm that matches the direction the player is facing
                if (pc.facingRight)
                {
                    MotorizeArmRight();
                    
                    LeftArm.useLimits = false;
                    LeftArm.useMotor = false;
                    LowerLeftArm.useLimits = false;
                    LowerLeftArm.useMotor = false;
                }
                else
                {
                    MotorizeArmLeft();
                    
                    RightArm.useLimits = false;
                    RightArm.useMotor = false;
                    LowerRightArm.useLimits = false;
                    LowerRightArm.useMotor = false;
                }
            }
            //If not aiming, reset both arms to ragdoll
            else
            {
                RightArm.useLimits = false;
                RightArm.useMotor = false;
                LowerRightArm.useLimits = false;
                LowerRightArm.useMotor = false;
                
                LeftArm.useLimits = false;
                LeftArm.useMotor = false;
                LowerLeftArm.useLimits = false;
                LowerLeftArm.useMotor = false;
            }
        }

    }

    /// <summary>
    /// Takes the mouse position and sets the limits of the right arm's "shoulder" hinge joint to point towards the mouse pos
    /// </summary>
    void MotorizeArmRight()
    {
        RightArm.useLimits = true;
        LowerRightArm.useLimits = true;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        
        //Creates new limits to assign, calculates the angle between the direction to the mouse pos and the hinge's current angle
        JointAngleLimits2D limits = new JointAngleLimits2D();
        Vector3 dir = (mousePos - shoulderR.transform.position).normalized;
        float angle = Vector2.SignedAngle(dir, Vector2.right);
        limits.max = angle + 10;
        limits.min = angle - 10;
        
        //Does the same thing as above, but for the lower arm past the elbow
        JointAngleLimits2D limitsLower = new JointAngleLimits2D();
        Vector2 dirLower = (mousePos - shoulderR.transform.position).normalized;
        float angleLower = Vector2.SignedAngle(dirLower, LowerRightArm.transform.right);
        limitsLower.max = angleLower + 10;
        limitsLower.min = angleLower - 10;


//        Debug.Log("Angle: " + Vector2.Angle(dir, RightArm.transform.right));
//        Debug.Log("LowerAngle: " + Vector2.Angle(dir, LowerRightArm.transform.right));
//        Debug.Log("Joint: " + RightArm.jointAngle + ", Target Angle: " + angle);
        Debug.DrawRay(RightArm.transform.position, RightArm.transform.right, Color.blue);
        Debug.DrawRay(mousePos, dir, Color.red);

        //Uses the motor to push the joint to match the new limits faster. Changes motor speed depending on direction the joint has to move
        if (Vector2.Angle(dir, RightArm.transform.right) > 10)
        {
            JointMotor2D newMotor = RightArm.motor;

            if (RightArm.jointAngle < angle)
            {
                if (newMotor.motorSpeed <= 0)
                {
                    newMotor.motorSpeed = 200;
                }
            }
            else if (RightArm.jointAngle > angle)
            {
                if (newMotor.motorSpeed >= 0)
                {
                    newMotor.motorSpeed = -200;
                }
            }

            RightArm.motor = newMotor;
            RightArm.limits = limits;
            RightArm.useMotor = true;
        }
        else
        {
            JointMotor2D newMotor = RightArm.motor;
            newMotor.motorSpeed = 0;
            RightArm.motor = newMotor;
            
            limits.max = limits.min;
            RightArm.limits = limits;
        }

        //Same as above, but with the lower arm
        if (Vector2.Angle(dirLower, LowerRightArm.transform.right) > 10)
        {
            JointMotor2D newMotor = LowerRightArm.motor;

            if (LowerRightArm.jointAngle < angle)
            {
                if (newMotor.motorSpeed <= 0)
                {
                    newMotor.motorSpeed = 100;
                }
            }
            else if (LowerRightArm.jointAngle > angle)
            {
                if (newMotor.motorSpeed >= 0)
                {
                    newMotor.motorSpeed = -100;
                }
            }

            LowerRightArm.motor = newMotor;
            LowerRightArm.limits = limitsLower;
            LowerRightArm.useMotor = true;
        }
        else
        {
            JointMotor2D newMotor = LowerRightArm.motor;
            newMotor.motorSpeed = 0;
            LowerRightArm.motor = newMotor;
            
            limitsLower.max = limitsLower.min;
            LowerRightArm.limits = limitsLower;
        }
        
    }
    
    /// <summary>
    /// Same as MotorizeArmRight(), but for the left arm
    /// </summary>
    void MotorizeArmLeft()
    {
        LeftArm.useLimits = true;
        LowerLeftArm.useLimits = true;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        
        JointAngleLimits2D limits = new JointAngleLimits2D();
        Vector3 dir = (mousePos - shoulderL.transform.position).normalized;
        float angle = Vector2.SignedAngle(dir, Vector2.left);
        limits.max = angle + 10;
        limits.min = angle - 10;
        
        JointAngleLimits2D limitsLower = new JointAngleLimits2D();
        Vector2 dirLower = (mousePos - shoulderL.transform.position).normalized;
        float angleLower = Vector2.SignedAngle(dirLower, -LowerLeftArm.transform.right);
        limitsLower.max = angleLower + 10;
        limitsLower.min = angleLower - 10;


        Debug.Log("Angle: " + Vector2.Angle(dir, LeftArm.transform.right));
//        Debug.Log("LowerAngle: " + Vector2.Angle(dir, LowerRightArm.transform.right));
//        Debug.Log("Joint: " + RightArm.jointAngle + ", Target Angle: " + angle);
        Debug.DrawRay(LeftArm.transform.position, LeftArm.transform.right, Color.blue);
        Debug.DrawRay(mousePos, dir, Color.red);

        if (Vector2.Angle(dir, -LeftArm.transform.right) > 10)
        {
            JointMotor2D newMotor = LeftArm.motor;

            if (LeftArm.jointAngle < angle)
            {
                if (newMotor.motorSpeed <= 0)
                {
                    newMotor.motorSpeed = 200;
                }
            }
            else if (LeftArm.jointAngle > angle)
            {
                if (newMotor.motorSpeed >= 0)
                {
                    newMotor.motorSpeed = -200;
                }
            }

            LeftArm.motor = newMotor;
            LeftArm.limits = limits;
            LeftArm.useMotor = true;
        }
        else
        {
            JointMotor2D newMotor = LeftArm.motor;
            newMotor.motorSpeed = 0;
            LeftArm.motor = newMotor;
            
            limits.max = limits.min;
            LeftArm.limits = limits;
        }

        if (Vector2.Angle(dirLower, -LowerLeftArm.transform.right) > 10)
        {
            JointMotor2D newMotor = LowerLeftArm.motor;

            if (LowerLeftArm.jointAngle < angle)
            {
                if (newMotor.motorSpeed <= 0)
                {
                    newMotor.motorSpeed = 100;
                }
            }
            else if (LowerLeftArm.jointAngle > angle)
            {
                if (newMotor.motorSpeed >= 0)
                {
                    newMotor.motorSpeed = -100;
                }
            }

            LowerLeftArm.motor = newMotor;
            LowerLeftArm.limits = limitsLower;
            LowerLeftArm.useMotor = true;
        }
        else
        {
            JointMotor2D newMotor = LowerLeftArm.motor;
            newMotor.motorSpeed = 0;
            LowerLeftArm.motor = newMotor;
            
            limitsLower.max = limitsLower.min;
            LowerLeftArm.limits = limitsLower;
        }
        
    }
    
}
