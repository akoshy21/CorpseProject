using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    [SerializeField, Tooltip("Must be more than 100")]
    public float launchHeightMax;

    [SerializeField, Tooltip("Base Force Before Adjustments")]
    public float baseLaunchForce = 150;

    //public List<Rigidbody2D> currentBodies = new List<Rigidbody2D>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            //print("player");
            GameObject current = collision.gameObject;
            PlayerController controller = current.transform.parent.GetComponentInChildren<PlayerController>();
            if (controller == null)
            {
                current = current.transform.parent.gameObject;
                controller = current.transform.parent.GetComponentInChildren<PlayerController>();
            }
            if (controller == null)
            {
                current = current.transform.parent.gameObject;
                controller = current.transform.parent.GetComponentInChildren<PlayerController>();
            }
            if (controller != null && !controller.dead)
            {
                Rigidbody2D rigidbody = controller.GetComponent<Rigidbody2D>();
                if(rigidbody != null)
                {
                    float velocity = rigidbody.velocity.y;
                    
                    float forceToAdd = baseLaunchForce;
                    
                    print(velocity);

                    if (velocity < 0)
                    {
                        forceToAdd -= velocity;
                        //velocity = -velocity;
                        //print("velocityLaunch");
                        
                        if (forceToAdd + velocity > launchHeightMax)
                        {
                            forceToAdd = launchHeightMax;
                            //print("thisSHouldntPrint");
                        }
                        else
                        {
                            forceToAdd += velocity;
                        }
            
            
                        //print(forceToAdd);
                        rigidbody.AddForce(new Vector2(0, forceToAdd), ForceMode2D.Impulse);
                    
                        //print("TheActualJump");
                        //rigidbody.velocity = new Vector2(rigidbody.velocity.x,0);
                        //rigidbody.AddForce(new Vector2(0, forceToAdd), ForceMode2D.Impulse);
                        //print(forceToAdd);
                    }
                    else
                    {
                        //print("its positive?");
                    }
                    

                    
                }
                /*
                //print("Jump");
                Rigidbody2D rigidbody = controller.GetComponent<Rigidbody2D>();
                bool alreadyGotThatOne = false;
                foreach(Rigidbody2D body in currentBodies)
                {
                    if (rigidbody.Equals(body))
                    {
                        alreadyGotThatOne = true;
                    }
                }

                if (!alreadyGotThatOne)
                {
                    currentBodies.Add(rigidbody);
                    //Launch();
                   // print("launch");
                }
                else
                {
                    print("alreadyGotThatOne");
                }
                */
                
                
                
            }

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        /*
        GameObject current = other.gameObject;
        PlayerController controller = current.transform.parent.GetComponentInChildren<PlayerController>();
        if (controller == null)
        {
            current = current.transform.parent.gameObject;
            if (current != null)
            {
                controller = current.transform.parent.GetComponentInChildren<PlayerController>();
            }
            
        }
        if (controller == null)
        {
            current = current.transform.parent.gameObject;
            if (current != null)
            {
                controller = current.transform.parent.GetComponentInChildren<PlayerController>();
            }
        }
        if (controller != null && !controller.dead)
        {
                
            Rigidbody2D rigidbody = controller.GetComponent<Rigidbody2D>();
            int savedIndex = 0;
            bool foundIt = false;
            
           
            for (int i = 0; i < currentBodies.Count; i++)
            {
                if (currentBodies[i].Equals(rigidbody))
                {
                    //foundIt = true;
                    //savedIndex = i;
                }
            }

            if (foundIt)
            {
                //currentBodies.Remove(currentBodies[savedIndex]);
            }
        }
        */
    }

    /*
    private void Launch()
    {
        foreach(Rigidbody2D body in currentBodies)
        {
            float velocity = body.velocity.y;

            float forceToAdd = baseLaunchForce;

            if (velocity < 0)
            {
                //forceToAdd += velocity; 
            }
            else
            {
                forceToAdd -= velocity;
                //velocity = -velocity;
                //print("velocityLaunch");
            }

            if (forceToAdd + velocity > launchHeightMax)
            {
                //forceToAdd = launchHeightMax - velocity;
                //print("thisSHouldntPrint");
            }
            else
            {
                forceToAdd += velocity;
            }

            print(velocity);
            print(forceToAdd);
            body.AddForce(new Vector2(0, forceToAdd), ForceMode2D.Impulse);

        }
        
        if(rigidbody != null)
        {
            float velocity = rigidbody.velocity.y;
                    
            //check for negative velocity
            float jumpAdjustment = (velocity > 0) ? velocity : -velocity;
                    
            //check for 0 veloctiy
            if (velocity == 0)
            {
                jumpAdjustment = 100;
            }
                    
            //max it out at 100
            if (jumpAdjustment > launchHeightMax)
            {
                jumpAdjustment = launchHeightMax;
            }
            else if (jumpAdjustment < 100)
            {
                jumpAdjustment = 100;
            }
            print(velocity);
                    
            //print("TheActualJump");
            //rigidbody.velocity = new Vector2(rigidbody.velocity.x,0);
            rigidbody.AddForce(new Vector2(0, jumpAdjustment), ForceMode2D.Impulse);
            print(jumpAdjustment);
        }
        
    }
    */
}
