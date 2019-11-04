using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    public float launchHeight;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            print("Player");
            GameObject current = collision.gameObject;
            PlayerController controller = current.transform.parent.GetComponentInChildren<PlayerController>();
            if (controller == null)
            {
                print("Null1");
                current = current.transform.parent.gameObject;
                controller = current.transform.parent.GetComponentInChildren<PlayerController>();
            }
            if (controller == null)
            {
                print("Null2");
                current = current.transform.parent.gameObject;
                controller = current.transform.parent.GetComponentInChildren<PlayerController>();
            }
            if (controller != null && !controller.dead)
            {
                print("Jump");
                Rigidbody2D rigidbody = controller.GetComponent<Rigidbody2D>();
                if(rigidbody != null)
                {
                    print("TheActualJump");
                    rigidbody.AddForce(new Vector2(0, launchHeight), ForceMode2D.Impulse);
                }
            }

        }
        
        if (collision.transform.parent.CompareTag("PlayerOne"))
        {
            PlayerController controller = collision.transform.parent.GetComponentInChildren<PlayerController>();
            if (controller != null && !controller.dead)
            {
                Rigidbody2D rigidbody = controller.GetComponent<Rigidbody2D>();
                if(rigidbody != null)
                {
                    rigidbody.AddForce(new Vector2(0, launchHeight), ForceMode2D.Impulse);
                }
            }
        }
        else if (collision.transform.parent.CompareTag("PlayerTwo"))
        {
            print("Trigger");
            PlayerController controller = collision.transform.parent.GetComponentInChildren<PlayerController>();
            if (controller != null && !controller.dead)
            {
                Rigidbody2D rigidbody = controller.GetComponent<Rigidbody2D>();
                if (rigidbody != null)
                {
                    rigidbody.AddForce(new Vector2(0, launchHeight), ForceMode2D.Impulse);
                }
            }
        }
    }
}
