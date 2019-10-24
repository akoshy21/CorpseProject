using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassFunctions : MonoBehaviour
{
    public float velocityCap; //The minimum speed you need to travel to break the glass
    private float velocityDivide; //Part of crashSlowSpeed calculations
    private float crashSlowSpeed; //Force added to body when it breaks through the glass
    public GameObject glassCol;
    private float collisionSpeed; //Just logging the speed of the gameobject
    public SpriteRenderer glassRenderer;
    // Start is called before the first frame update
    void Start()
    {
        velocityDivide = 6f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        collisionSpeed = other.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude;
        crashSlowSpeed = other.gameObject.GetComponent<Rigidbody2D>().velocity.x / velocityDivide;
        
        
        
        if (collisionSpeed > velocityCap)
        {
            Debug.Log("Shatter");
         Shatter();   
         other.gameObject.GetComponent<Rigidbody2D>().AddForceAtPosition
             (new Vector2(-crashSlowSpeed,0.5f), new Vector2(0f, 7f),ForceMode2D.Impulse);
        }
        
        

        if (other.gameObject.CompareTag("Player") == true && collisionSpeed > velocityCap)
        {
            PlayerController controller = other.transform.parent.gameObject.GetComponentInChildren<PlayerController>();
            controller.Die();
        }
    }

    public void Shatter()
    {
        glassCol.SetActive(false);
        //CARSEN CAN DO SOME COOL SHIT HERE WITH THE PARTICLE EFFECTS
        glassRenderer.color = Color.red;
        
    }
}
