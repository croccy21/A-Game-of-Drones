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

	public static float maxPower = 5;
	public static float minPower = 0;

	private Rigidbody rb;

	bool resetVerticleVelocity = false;

	void Start () {
		rb = GetComponent<Rigidbody>();
		Debug.developerConsoleVisible = true;
	}

	void Update () {
		power += Input.GetAxis("power")*powerMultiplier;
		yaw = Input.GetAxis ("yaw") * yawMultiplier;
		pitch = Input.GetAxis ("pitch") * pitchMultiplier;
		roll = Input.GetAxis ("roll") * rollMultiplier;

		if (power > maxPower) {
			power = maxPower;
		}
		if (power < minPower) {
			power = minPower;
		}
		//print (power);

		if (Input.GetAxisRaw ("powerBalance") == 1) {
			power = -Physics.gravity.y *rb.mass;
			resetVerticleVelocity = true;
		}
	}

	void FixedUpdate(){
		rb.AddRelativeForce (power*transform.up);
		//if (resetVerticleVelocity) {
		//	rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.y);
		//}
		Vector3 vec = rb.rotation * (power * transform.up);
		print (vec.x + "," + vec.y + "," + vec.z);

		if((Mathf.Abs(yaw)>0) || (Mathf.Abs(roll)>0) || (Mathf.Abs(pitch)>0)){
			rb.AddRelativeTorque (torqueCoefficient*Mathf.Pow(power, 0.5f)*(new Vector3 (-roll, yaw, -pitch)));
		}

	}
}
