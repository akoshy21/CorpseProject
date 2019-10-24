using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    /*
     * @author Kate Howell
     * 
     * This script loads nextSceneToLoad when both playerOne and playerTwo are colliding with this object
     */

    [SerializeField, Tooltip("Delay in Seconds that the scene loaded after the player collides with the goal")]
    public int loadDelay;

    public bool playerOneAtGoal;
    public bool playerTwoAtGoal;

    [SerializeField, Tooltip("Handle of the Scene to be loaded when goal is activated")]
    public Scene nextSceneToLoad;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.parent.CompareTag("PlayerOne"))
        {
            PlayerController controller = collision.transform.parent.GetComponentInChildren<PlayerController>();
            if (!controller.dead)
            {
                playerOneAtGoal = true;
            }
        }
        else if (collision.transform.parent.CompareTag("PlayerTwo"))
        {
            PlayerController controller = collision.transform.parent.GetComponentInChildren<PlayerController>();
            if (!controller.dead)
            {
                playerTwoAtGoal = true;
            }
        }

        if(playerOneAtGoal && playerTwoAtGoal)
        {
            
        }


        //testing section
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController controller = collision.transform.parent.GetComponentInChildren<PlayerController>();
            if (!controller.dead)
            {
                SceneManager.LoadScene(nextSceneToLoad.handle);
            }
        }
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerOne"))
        {
            playerOneAtGoal = false;
        }
        else if (collision.gameObject.CompareTag("PlayerTwo"))
        {
            playerTwoAtGoal = false;
        }
    }


    IEnumerator loadNextScene()
    {
        yield return new WaitForSeconds(loadDelay);
        SceneManager.LoadScene(nextSceneToLoad.handle);
        yield return null;
  
    }
}
