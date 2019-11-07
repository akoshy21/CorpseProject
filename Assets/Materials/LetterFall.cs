using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterFall : MonoBehaviour
{
    public GameObject letter;
    public Rigidbody2D rigbod;

    public bool letterfall;
    
    private float timer;

    public float dropTime;
    
    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
        rigbod.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.anyKeyDown && !letterfall)
        {
            letterfall = true;
            StartCoroutine(Delay(0.1f));
        }
    }

    IEnumerator Delay(float del)
    {
        yield return new WaitForSeconds(del);
        rigbod.isKinematic = false;
    }
}
