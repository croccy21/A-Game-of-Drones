using UnityEngine;
using System.Collections;

public class LightOnOff : MonoBehaviour {
	private Light light;

	// Use this for initialization
	void Start () {
		light = GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void turnOn(){
		light.enabled = true;
	}
	public void turnOff(){
		light.enabled = false;
	}
	public void toggleLight(){
		light.enabled = !light.enabled;
	}
	public bool isLightOn(){
		return light.enabled;
	}
}
