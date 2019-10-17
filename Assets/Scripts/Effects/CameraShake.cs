using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

	private float shakeDuration = 0f;
	public float shakeMagnitude = 0.7f;
	//private float dampingSpeed = 1.0f;
	private Vector3 initialPosition;
	private float elapsed;
	
	// Use this for initialization
	void Start ()
	{
		initialPosition = transform.localPosition;
	}
	
	
	// Update is called once per frame
	void Update () {
		
		if (elapsed < shakeDuration) {
        
			elapsed += Time.deltaTime;          
        
			float percentComplete = elapsed / shakeDuration;         
			float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);
        
			// map value to [-1, 1]
			float x = Random.value * 2.0f - 1.0f;
			float y = Random.value * 2.0f - 1.0f;
			x *= shakeMagnitude * damper;
			y *= shakeMagnitude * damper;
        
			transform.localPosition = new Vector3(initialPosition.x + x, initialPosition.y + y, initialPosition.z);
            
		}
//		if (shakeDuration > 0)
//		{
//			transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;
//			shakeDuration -= Time.deltaTime * dampingSpeed;
//		}
//		else
//		{
//			shakeDuration = 0f;
//			transform.localPosition = initialPosition;
//		}
	}
	
	public void ShakeCamera(float shakeDuration) {
		this.shakeDuration = shakeDuration;
		elapsed = 0;
	}
}
