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
    public float loadDelay;
    
    public bool playerOneAtGoal;
    public bool playerTwoAtGoal;
    private AudioSource audioSource;

    private SpriteRenderer spriteRenderer;
    public Sprite doorOpenSprite;
    public Sprite doorClosedSprite;

    /*
    [SerializeField, Tooltip("Handle of the Scene to be loaded when goal is activated")]
    public Scene nextSceneToLoad;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.parent.CompareTag("PlayerOne"))
        {
            PlayerController controller = collision.transform.parent.GetComponentInChildren<PlayerController>();
            if (controller != null && !controller.dead)
            {
                playerOneAtGoal = true;
            }
        }
        else if (collision.transform.parent.CompareTag("PlayerTwo"))
        {
            PlayerController controller = collision.transform.parent.GetComponentInChildren<PlayerController>();
            if (controller != null && !controller.dead)
            {
                playerTwoAtGoal = true;
            }
        }

        if(playerOneAtGoal && playerTwoAtGoal)
        {
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
    */

    /*-------------- ALT GOAL CODE -------------------*/
    //akoshy

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if(audioSource == null)
        {
            throw new System.Exception("Goal Missing Audio Source");
        }
        spriteRenderer = GetComponent<SpriteRenderer>();
        if(spriteRenderer == null)
        {
            throw new System.Exception("Goal Missing Sprite Renderer wtf");
        }
        spriteRenderer.sprite = doorClosedSprite;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.parent == null)
        {
            return;
        }
        if (collision.transform.parent.CompareTag("PlayerOne"))
        {
            PlayerController controller = collision.transform.parent.GetComponentInChildren<PlayerController>();
            if (controller != null && !controller.dead)
            {
                playerOneAtGoal = true;
            }
        }
        else if (collision.transform.parent.CompareTag("PlayerTwo"))
        {
            PlayerController controller = collision.transform.parent.GetComponentInChildren<PlayerController>();
            if (controller != null && !controller.dead)
            {
                playerTwoAtGoal = true;
            }
        }

        if (playerOneAtGoal && playerTwoAtGoal)
        {
            print("yayBoth");
            StartCoroutine(loadNextScene());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerOne"))
        {
            playerOneAtGoal = false;
        }
        else if (collision.gameObject.CompareTag("PlayerTwo"))
        {
            playerTwoAtGoal = false;
        }
        else if(collision.gameObject.CompareTag("Player"))
        {
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
                if (current.transform.parent.CompareTag("PlayerOne"))
                {
                    playerOneAtGoal = false;
                }
                else if (current.transform.parent.CompareTag("PlayerTwo"))
                {
                    playerTwoAtGoal = false;
                }
            }
        }
    }

    IEnumerator loadNextScene()
    {
        //print("loaded");
        if(!audioSource.isPlaying)
        {
            //print("audioASource");
            audioSource.Play();
        }
        spriteRenderer.sprite = doorOpenSprite;
        yield return new WaitForSeconds(loadDelay);
        LevelManager.lm.lvlEnd = true;
        yield return null;

    }
}
