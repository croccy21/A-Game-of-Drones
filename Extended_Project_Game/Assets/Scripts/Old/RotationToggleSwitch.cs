using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RotationToggleSwitch : MonoBehaviour {
	
	public Texture Normal;
	public Texture Pressed;
	public Texture Deactivated;
	
	private GameObject drone;
	private Movement movementScript;
	private RawImage currentTexture;
	
	private int lastState;
	
	// Use this for initialization
	void Start () {
		drone = GameObject.Find("drone");
		movementScript = drone.GetComponent<Movement>();
		currentTexture = GetComponent<RawImage> ();
		lastState = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (lastState != movementScript.balanceRotateButtonState) {
			if (movementScript.balanceRotateButtonState==0) {
				currentTexture.texture = Normal;
				lastState = 0;
			}
			if (movementScript.balanceRotateButtonState==1) {
				currentTexture.texture = Pressed;
				lastState = 1;
			} 
			if (movementScript.balanceRotateButtonState==2) {
				currentTexture.texture = Deactivated;
				lastState = 2;
			}
		}
		
	}
}
