using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyLimb : MonoBehaviour
{
    
    /*
     * @author Kate Howell
     */
    
    
    private bool spriteDisplayed;
    public Sprite spriteSticky;
    
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Player") && other.transform.parent != transform.parent)
        {
            HingeJoint2D joint = gameObject.AddComponent<HingeJoint2D>();
            joint.anchor = other.contacts[0].point;
            joint.connectedBody = other.transform.GetComponent<Rigidbody2D>();
            joint.enableCollision = false;
        }
       
    }

    private void Update()
    {
        if (!spriteDisplayed)
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = spriteSticky;
            spriteDisplayed = true;
        }
    }
}
