using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuDoor : MonoBehaviour
{
    public Sprite open;
    public AudioSource aud;

    bool pOne, pTwo, endStart;

    private void Start()
    {
        aud = this.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        //if(col.transform.parent.)
        
    }

    private void Update()
    {
        if (endStart && pOne && pTwo)
        {
            StartCoroutine(ToLevels());
            endStart = false;
        }
    }

    IEnumerator ToLevels()
    {
        this.GetComponent<SpriteRenderer>().sprite = open;
        aud.Play();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("LevelSelect");

    }
}
