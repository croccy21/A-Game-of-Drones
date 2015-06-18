using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CustomToggleSwitch : MonoBehaviour {
	
	public Texture Normal;
	public Texture Pressed;
	
	private GameObject drone;
	private Movement movementScript;
	private RawImage currentTexture;
	
	private bool lastState;
	
	// Use this for initialization
	void Start () {
		drone = GameObject.Find("drone");
		movementScript = drone.GetComponent<Movement>();
		currentTexture = GetComponent<RawImage> ();
		lastState = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (lastState != movementScript.balanceHeight) {
			if (movementScript.balanceHeight) {
				currentTexture.texture = Pressed;
				lastState = true;
			} else {
				currentTexture.texture = Normal;
				lastState = false;
			}
		}
		
	}
}
