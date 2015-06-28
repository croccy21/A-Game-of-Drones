﻿using UnityEngine;
using System.Collections;

public class Keying : MonoBehaviour {

	public GameObject controlDrone;
	public GameObject gravityButton;
	public GameObject rotationButton;
	private DisplayButton gravityDisplay;
	private DisplayButton rotationDisplay;


	private Drone drone;
	private GameObject fader;
	private ScreenFadeInOut faderScript;

	private bool canReset = true;
	private bool canToggleBalanceGravity = true;
	private bool canToggleBalanceRotation = true;

	// Use this for initialization
	void Start () {
		drone = controlDrone.GetComponent<Drone>();
		fader = GameObject.Find ("ScreenFadeCanvas");
		faderScript = fader.GetComponent<ScreenFadeInOut> ();
		gravityDisplay = gravityButton.GetComponent<DisplayButton> ();
		rotationDisplay = rotationButton.GetComponent<DisplayButton> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		drone.changeForce (	 Input.GetAxis ("power"));
		drone.changeRotation(Input.GetAxis ("roll"), 
		                     Input.GetAxis ("yaw"), 
		                     Input.GetAxis ("pitch"));

		if (Input.GetAxisRaw("reset") == 1 && canReset) {
			StartCoroutine(Waiting(fader));
		}

		if (Input.GetAxisRaw ("powerBalance") == 1) {
			if (canToggleBalanceGravity){
				drone.toggleBalanceGravity();
				canToggleBalanceGravity = false;
			}
		} else {
			canToggleBalanceGravity = true;
		}

		if (Input.GetAxisRaw ("rotationBalance") == 1) {
			if (canToggleBalanceRotation){
				drone.toggleBalanceRotation();
				canToggleBalanceRotation = false;
			}
		} else {
			canToggleBalanceRotation = true;
		}

		gravityDisplay.setTexture (drone.getBanlanceGravityMode ());
		rotationDisplay.setTexture (drone.getBanlanceRotationMode ());
	}

	IEnumerator Waiting(GameObject fader){
		canReset = false;
		yield return StartCoroutine(faderScript.DoFadeOut ());
		drone.resetRotation();
		yield return StartCoroutine(faderScript.DoFadeIn ());
		canReset = true;
	}
}