using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GravityBalanceToggle : MonoBehaviour {

	public Texture Normal;
	public Texture Pressed;

	private GameObject drone = GameObject.Find("drone");
	private Movement movementScript;
	private RawImage currentTexture;

	private bool lastState;

	// Use this for initialization
	void Start () {
		movementScript = drone.GetComponent<Movement>();
		currentTexture = GetComponent<RawImage> ();
		lastState = true;
	}
	
	// Update is called once per frame
	void Update () {
		print ("l:" + lastState + ", b:"+movementScript.balanceHeight);
		if (lastState != movementScript.balanceHeight) {
			print ("stuff2");
			if (movementScript.balanceHeight) {
				currentTexture.texture = Pressed;
				lastState = true;
				print ("stuff3");
			} else {
				currentTexture.texture = Normal;
				lastState = false;
			}
		}
	
	}
}
