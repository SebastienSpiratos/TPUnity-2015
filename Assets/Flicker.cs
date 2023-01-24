using UnityEngine;
using System.Collections;

public class Flicker : MonoBehaviour {

	private float timeOn = 0.1f;
	private float timeOff = 0.5f;
	private float changeTime = 0f;
	Light light;
	
	void Start () {
		light  = GetComponent<Light>();
	}
	
	// Fait flasher la lumere et donne un effet de lumiere defectueuse	
	void Update() {
		timeOn = Random.Range(0.01f, 0.2f); 
		timeOff = Random.Range(0.01f, 0.2f); 
		if (Time.time > changeTime) {
			light.enabled = !light.enabled;
			if (light.enabled) {
				changeTime = Time.time + timeOn;
			} else {
				changeTime = Time.time + timeOff;
			}
		}

	
	}
}
