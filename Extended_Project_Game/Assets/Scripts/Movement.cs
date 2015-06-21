using UnityEngine;
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

	public float heightBalanceVelocityMultiplier = 0.2f;
	public float heightBalanceDisplacementMultiplier = 0.2f;

	public float maxPower = 5;
	public float minPower = 0;

	public static Vector3 directionUp  = new Vector3(0, 1, 0);

	private Rigidbody rb;

	private bool canReset = true;

	private bool balanceHeightButtonPressed = false;
	public byte balanceHeightButtonState = 0;
	public float originHight;

	
	GameObject fader;

	/**Deals with the four main inputs:
	* Power
	* Roll
	* Pitch
	* Yaw
	This only puts the values in the appropriate variables 
	and does not do anything with them
	*/
	private void getMainInputs(){
		power += Input.GetAxis("power")*powerMultiplier;
		yaw = Input.GetAxis ("yaw") * yawMultiplier;
		pitch = Input.GetAxis ("pitch") * pitchMultiplier;
		roll = Input.GetAxis ("roll") * rollMultiplier;
	}

	private void getToggleInputs(){
		if (Input.GetAxisRaw ("powerBalance") == 1) {
			if (!balanceHeightButtonPressed) {
				balanceHeightButtonState = (byte)(balanceHeightButtonState<1 ? 1 : 0);
				balanceHeightButtonPressed = true;
			}
		} else {
			balanceHeightButtonPressed = false;
		}

		if (power > maxPower) {
			power = maxPower;
		}
		if (power < minPower) {
			power = minPower;
		}
		
		/**
		 * Reset Inputs
		*/
		if (canReset && Input.GetAxisRaw ("reset") == 1) {
			StartCoroutine(Waiting(fader));
		}
	}

	private void calculateForce(){

		if (balanceHeightButtonState != 1) {
			originHight=rb.position.y;
		}

		if (balanceHeightButtonState>0 && Input.GetAxis ("power") == 0) {
			float deltaHeight = originHight - rb.position.y;
			float deltaPower = rb.velocity.y * heightBalanceVelocityMultiplier 
				  			 - deltaHeight * heightBalanceDisplacementMultiplier;
			//if (Mathf.Abs(deltaPower) > 0.07f) {
				power -= deltaPower;
			//}
			balanceHeightButtonState = (byte)(balanceHeightButtonState>0 ? 1 : 0);
		} else if (balanceHeightButtonState>0) {
			balanceHeightButtonState = 2;
		}

		if (power > maxPower) {
			power = maxPower;
		}
		if (power < minPower) {
			power = minPower;
		}
	}

	void Start () {
		rb = GetComponent<Rigidbody>();
		fader = GameObject.Find ("ScreenFadeCanvas");
		Debug.developerConsoleVisible = true;
	}

	void Update () {
	}

	void FixedUpdate(){

		getMainInputs ();
		getToggleInputs ();
		calculateForce ();

		if((Mathf.Abs(yaw)>0) || (Mathf.Abs(roll)>0) || (Mathf.Abs(pitch)>0)){
			rb.AddRelativeTorque (torqueCoefficient*Mathf.Pow(power, 0.5f)*(new Vector3 (-roll, yaw, -pitch)));
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
