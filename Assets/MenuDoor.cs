using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuDoor : MonoBehaviour
{
    public Sprite open;
    public AudioSource aud;

    public enum Menu {Start, Tutorial, LvlSelect}
    public Menu men;

    bool pOne, pTwo, endStart = true;

    private void Start()
    {
        aud = this.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.parent.CompareTag("PlayerTwo"))
        {
            pTwo = true;
        }
        if (col.transform.parent.CompareTag("PlayerOne"))
        {
            pOne = true;
        }   
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.transform.parent.CompareTag("PlayerTwo"))
        {
            pTwo = false;
        }
        if (col.transform.parent.CompareTag("PlayerOne"))
        {
            pOne = false;
        }
    }

    private void Update()
    {
        if (endStart && pOne && pTwo)
        {
            if (men == Menu.Tutorial)
            {
                StartCoroutine(ToLevels("LevelSelect"));
            }
            else if (men == Menu.Start)
            {
                StartCoroutine(ToLevels("Tutorial"));
            }
            endStart = false;
        }
    }

    IEnumerator ToLevels(string lvl)
    {
        this.GetComponent<SpriteRenderer>().sprite = open;
        aud.Play();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(lvl);

    }
}
