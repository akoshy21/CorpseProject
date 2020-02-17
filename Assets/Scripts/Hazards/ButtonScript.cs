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
        bool p1 = false, p2 = false;
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

            if ((Input.GetKey(KeyCode.E) || Input.GetButton("P1Interact")) && checkIfPlayer(true))
            {
                p1 = true;
            }
            if ((Input.GetButton("P2Interact") || Input.GetKey(KeyCode.O)) && checkIfPlayer(false))
            {
                p2 = true;
            }

            if (isLever)
            {
                if (entryList.Count > 0 && (p1 || p2))
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

    bool checkIfPlayer(bool pOne)
    {
        for (int i = 0; i < entryList.Count; i++)
        {
            if (pOne)
            {
                if (entryList[i].transform.parent.CompareTag("PlayerOne"))
                {
                    return true;
                }
            }
            else
            {
                if (entryList[i].transform.parent.CompareTag("PlayerTwo"))
                {
                    return true;
                }
            }
        }
        return false;
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
      //Debug.Log(other);
        

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
