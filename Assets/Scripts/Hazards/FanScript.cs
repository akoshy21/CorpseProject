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

        wind.transform.localScale = this.transform.localScale * 9;

        if (status == State.Left)
        {
            rightTrigger.SetActive(false);
            leftTrigger.SetActive(true);

            Vector3 tempScale = wind.transform.localScale;
            tempScale.x *= -1;
            wind.transform.localScale = tempScale;

            tempScale = this.transform.localScale;
            tempScale.x *= -1;
            this.transform.localScale = tempScale;
        }

        if (status == State.Right)
        {
            rightTrigger.SetActive(true);
            leftTrigger.SetActive(false);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
