using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrationFunction : MonoBehaviour
{
    public GameObject narratorPlayer;
    public AudioSource player;
    public AudioClip clip;
    private bool startCheck;
    private bool turnOn;

    public bool dontDestroy;
    // Start is called before the first frame update
    void Start()
    {
       narratorPlayer.SetActive(true);
       startCheck = false;
       turnOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey && startCheck == false)
        {
           turnOn = true;
           startCheck = true;
        }

        if (turnOn == true)
        {
            player.PlayOneShot(clip);
            turnOn = false;
        }

        if (dontDestroy)
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
