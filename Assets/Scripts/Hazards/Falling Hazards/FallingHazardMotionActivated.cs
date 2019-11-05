using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingHazardMotionActivated : FallingHazard
{
    private RaycastHit2D[] checkRayHit;

    [Space(20)]
    [SerializeField, Tooltip("player will be checked for under this point")]
    public GameObject checkPoint;

    [SerializeField, Tooltip("Check Ray Length Down from checkPoint")]
    public float checkLength = 100f;


    public override void CheckForActivation()
    {
        Ray2D checkRay = new Ray2D(checkPoint.transform.position, Vector2.down);
        checkRayHit = Physics2D.RaycastAll(checkRay.origin, checkRay.direction, 100.22f);

        if (checkRayHit.Length > 0)
        {
            foreach (var hit in checkRayHit)
            {
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    activated = true;
                }
            }
        }

        if (activated && !fell)
        {
            Fall();
            fell = true;
        }
    }
}
