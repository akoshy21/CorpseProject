using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FallingHazard : MonoBehaviour
{
    public ButtonScript Button;
    public bool activated;
    private RaycastHit2D[] groundCheckRay;

    private void Update()
    {
        //physics
        Ray2D groundCheck = new Ray2D(transform.position, Vector2.down);
        groundCheckRay = Physics2D.RaycastAll(groundCheck.origin, groundCheck.direction, 0.22f);

        if (Button.buttonActive)
        {
            
        }
    }
    void Fall()
    {

    }
}
