using UnityEngine;
using System.Collections;

public class LightOnOff : MonoBehaviour {
	private Light scriptLight;

	// Use this for initialization
	void Start () {
		scriptLight = GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void turnOn(){
		scriptLight.enabled = true;
	}
	public void turnOff(){
		scriptLight.enabled = false;
	}
	public void toggleLight(){
		scriptLight.enabled = !scriptLight.enabled;
	}
	public bool isLightOn(){
		return scriptLight.enabled;
	}
}
