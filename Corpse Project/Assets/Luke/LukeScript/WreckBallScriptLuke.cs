using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WreckBallScriptLuke : MonoBehaviour
{

    public Rigidbody2D rigbod;

    public float initPush;

    public float normalPush;
    // Start is called before the first frame update
    void Start()
    {
        rigbod.AddForce(new Vector2(initPush,0f), ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
    }
}
