using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	public float powerMultiplier = 1f;
	public float yawMultiplier = 1f;
	public float pitchMultiplier = 1f;
	public float rollMultiplier = 1f;

	public float power = 0;
	public float yaw = 0;
	public float pitch = 0;
	public float roll = 0;

	public static float maxPower = 5;
	public static float minPower = 0;

	public float currentYaw;

	private Rigidbody rb;

	private Blade frontLeft = new Blade (new Vector3(0.01f, 0, -0.055f));
	private Blade frontRight = new Blade (new Vector3(0.01f, 0, 0.055f));
	private Blade backLeft = new Blade (new Vector3(-0.01f, 0, -0.055f));
	private Blade backRight = new Blade (new Vector3(-0.01f, 0, 0.055f));

	bool resetVerticleVelocity = false;

	void Start () {
		rb = GetComponent<Rigidbody>();
		currentYaw = 0;
		//Debug.developerConsoleVisible = true;
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
		currentYaw += yaw;
		rb.MoveRotation (Quaternion.Euler (new Vector3 (roll, currentYaw, pitch)));
		
		rb.AddRelativeForce (new Vector3(0, power, 0));
		if (resetVerticleVelocity) {
			rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.y);
		} 
		//rb.angularVelocity = rb.rotation * new Vector3(0, yaw, 0);
	}

	class Blade{
		private Vector3 position;
		private Vector3 force;
		
		public Blade(Vector3 position){
			this.position = position;
			force = new Vector3(0, 0, 0);
		}
		
		public Vector3 getPosition(){
			return position;
		}
	}
}
