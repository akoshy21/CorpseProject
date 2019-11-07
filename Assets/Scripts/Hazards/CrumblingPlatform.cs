using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrumblingPlatform : MonoBehaviour
{
    public bool crumbling;
    public float crumbleTimeIntervalBetween4Stages;
    private int crumbleStage; //out of 4 to add in sprites
    public bool reset;
    public float resetTime;
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider2D;
    public Sprite spriteS;
    public Sprite spriteCrumbling;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        spriteRenderer.sprite = spriteCrumbling;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            crumbling = true;
            StartCoroutine(crumble());
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        crumbling = false;
    }

    private IEnumerator crumble()
    {
        if (crumbling)
        {
            
            if (crumbleStage == 4)
            {
                crumbling = false;
                spriteRenderer.sprite = null;
                Destroy(boxCollider2D);
                if (reset)
                {
                    StartCoroutine(resetPlatform());
                }
            }
            else
            {
                spriteRenderer.sprite = spriteCrumbling;
                yield return new WaitForSeconds(crumbleTimeIntervalBetween4Stages);
                crumbleStage++;
                StartCoroutine(crumble());
            } 
        }
        else
        {
            
            crumbleStage = 0;
        }
    }

    private IEnumerator resetPlatform()
    {
        yield return new WaitForSeconds(resetTime);
        spriteRenderer.sprite = spriteS;
        
        crumbling = false;
        boxCollider2D = gameObject.AddComponent<BoxCollider2D>();
    }
}
