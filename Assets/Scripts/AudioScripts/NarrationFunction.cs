using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrationFunction : MonoBehaviour
{
    public GameObject narratorPlayer;
    public AudioSource player;
    public AudioClip clip;

    private bool turnOff;
    // Start is called before the first frame update
    void Start()
    {
       narratorPlayer.SetActive(true); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
           player.PlayOneShot(clip);
           turnOff = true;
        }

        if (!player.isPlaying && turnOff)
        {
            narratorPlayer.SetActive(false);
        }
    }
}
