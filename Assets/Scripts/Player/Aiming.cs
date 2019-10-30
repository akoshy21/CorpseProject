using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming : MonoBehaviour
{
    // Annamaria Koshy

    public Transform armL, armR, armLU, armRU;
    public float distAL, distAR, distALU, distARU;
    public Transform shoulderL, shoulderR;
    public float RotationSpeed;
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
            if (pc.facingRight)
            {
                WhereToPoint(armR);
            }
            else
            {
                WhereToPoint(armL);
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
