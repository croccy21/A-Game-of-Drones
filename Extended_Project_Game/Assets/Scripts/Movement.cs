﻿using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	public float powerMultiplier = 1f;
	public float yawMultiplier = 1f;
	public float pitchMultiplier = 1f;
	public float rollMultiplier = 1f;

	public float torqueCoefficient = 0.5f;

	public float power = 0;
	public float yaw = 0;
	public float pitch = 0;
	public float roll = 0;

	public float heightBalanceMultiplier = 0.2f;

	public static float maxPower = 5;
	public static float minPower = 0;

	public static Vector3 directionUp  = new Vector3(0, 1, 0);

	private Rigidbody rb;

	private bool canReset = true;

	public bool balanceHeight = false;
	public bool balanceHeightButtonPressed = false;

	
	GameObject fader;

	void Start () {
		rb = GetComponent<Rigidbody>();
		fader = GameObject.Find ("ScreenFadeCanvas");
		Debug.developerConsoleVisible = true;
	}

	void Update () {

	}

	void FixedUpdate(){

		power += Input.GetAxis("power")*powerMultiplier;
		yaw = Input.GetAxis ("yaw") * yawMultiplier;
		pitch = Input.GetAxis ("pitch") * pitchMultiplier;
		roll = Input.GetAxis ("roll") * rollMultiplier;
		
		if (Input.GetAxisRaw ("powerBalance") == 1) {
			if (!balanceHeightButtonPressed) {
				balanceHeight = !balanceHeight;
				balanceHeightButtonPressed = true;
				print (balanceHeight);
			}
		} else {
			balanceHeightButtonPressed = false;
		}

		if (balanceHeight && Input.GetAxis("power")==0){
			power -= rb.velocity.y*heightBalanceMultiplier;
		}

		if((Mathf.Abs(yaw)>0) || (Mathf.Abs(roll)>0) || (Mathf.Abs(pitch)>0)){
			rb.AddRelativeTorque (torqueCoefficient*Mathf.Pow(power, 0.5f)*(new Vector3 (-roll, yaw, -pitch)));
		}

		if (canReset && Input.GetAxisRaw ("reset") == 1) {
			StartCoroutine(Waiting(fader));
		}

		if (power > maxPower) {
			power = maxPower;
		}
		if (power < minPower) {
			power = minPower;
		}

		rb.AddRelativeForce (power*directionUp);

	}
	IEnumerator Waiting(GameObject fader){
		canReset = false;
		yield return StartCoroutine(fader.GetComponent<ScreenFadeInOut> ().DoFadeOut ());
		rb.MoveRotation (Quaternion.Euler (new Vector3 (0, 0, 0)));
		rb.MovePosition (new Vector3 (rb.position.x, rb.position.y, rb.position.z));
		yield return StartCoroutine(fader.GetComponent<ScreenFadeInOut> ().DoFadeIn ());
		canReset = true;
	}
}
