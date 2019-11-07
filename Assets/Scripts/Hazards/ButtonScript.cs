using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public bool isLever;
    public bool buttonActive;
    private List<GameObject> entryList;
    public bool stayOn;

    public SpriteRenderer spriteRend;
    public GameObject buttonBoxCollider;
    public Sprite buttonpressedsprite;

    public Sprite buttonunpressedspirte;

    public Sprite leverOnSprite;

    public Sprite leverOffSprite;

    private bool initClick;

    private bool buttonGateOn;
    private bool buttonGateOff;

    public AudioSource audiosource;

    public AudioClip buttonOnClip, buttonOffClip;

    public AudioClip leverOnClip, LeverOffClip;
    // Start is called before the first frame update
    void Start()
    {
        entryList = new List<GameObject>();
        if (isLever == false)
        {
            spriteRend.sprite = buttonunpressedspirte;
            buttonBoxCollider.SetActive(true);
        }

        if (isLever)
        {
            spriteRend.sprite = leverOffSprite;
            buttonBoxCollider.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isLever == false && buttonActive == false)
        {
            spriteRend.sprite = buttonunpressedspirte;
            buttonGateOn = true;
            if (initClick && buttonGateOff)
            {
                
                audiosource.PlayOneShot(buttonOffClip);
                buttonGateOff = false;
            }
        }
       
        if (entryList.Count > 0)
        {
            
            if (isLever == false)
            {
                buttonGateOff = true;
                buttonActive = true;
                spriteRend.sprite = buttonpressedsprite;
                initClick = true;
                if (buttonGateOn)
                {
                    audiosource.PlayOneShot(buttonOnClip);
                    buttonGateOn = false;
                }
               
            }

            if (isLever)
            {
                if ((Input.GetKey(KeyCode.E) || Input.GetButton("P1Interact") || Input.GetButton("P2Interact") && entryList.Count > 0))
                {
                    buttonActive = true;
                    buttonGateOff = true;
                    spriteRend.sprite = leverOnSprite;
                    if (buttonGateOn)
                    {
                        audiosource.PlayOneShot(leverOnClip);
                        buttonGateOn = false;
                    }
                   
                }
                else
                {
                    buttonActive = false;
                    buttonGateOn = true;
                    spriteRend.sprite = leverOffSprite;
                    if (buttonGateOff)
                    {
                        audiosource.PlayOneShot(LeverOffClip);
                        buttonGateOff = false;
                    }
                    
                }
            }
        }
        else
        {
            buttonActive = false;
        }

     

        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isLever == false)
        {
            entryList.Add(other.gameObject);
        }
        
        if (isLever && other.CompareTag("Player"))
        {
            entryList.Add(other.gameObject);
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
      Debug.Log(other);
        

      if (isLever == false)
      {
          entryList.Remove(other.gameObject);
      }
        
      if (isLever && other.CompareTag("Player"))
      {
          entryList.Remove(other.gameObject);
      }
    }

   
}
