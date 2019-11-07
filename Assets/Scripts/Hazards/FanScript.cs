using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanScript : MonoBehaviour
{
    public GameObject rightTrigger;
    public GameObject leftTrigger;

    public enum State { Left, Right, Off };
    public State status;
    // Start is called before the first frame update
    
    //Luke Brockmann
    void Start()
    {
        ParticleSystem wind = GetComponentInChildren<ParticleSystem>();
        
        if (status == State.Left)
        {
            rightTrigger.SetActive(false);
            leftTrigger.SetActive(true);

            Vector3 tempScale = wind.transform.localScale;
            tempScale.y *= -1;
            wind.transform.localScale = tempScale;

            this.transform.localScale = new Vector3(-1, transform.localScale.y);
        }

        if (status == State.Right)
        {
            rightTrigger.SetActive(true);
            leftTrigger.SetActive(false);
            this.transform.localScale = new Vector3(1, transform.localScale.y);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
