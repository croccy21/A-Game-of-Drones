using UnityEngine;
using System.Collections;

public class MovementScript : MonoBehaviour {

	public float powerMultiplier = 1f;
	public int yawMultiplier = 1;
	public int pitchMultiplier = 1;
	public int rollMultiplier = 1;

	private Rigidbody rb;

	private Blade frontLeft = new Blade (new Vector3(0.01f, 0, -0.055f));
	private Blade frontRight = new Blade (new Vector3(0.01f, 0, 0.055f));
	private Blade backLeft = new Blade (new Vector3(-0.01f, 0, -0.055f));
	private Blade backRight = new Blade (new Vector3(-0.01f, 0, 0.055f));

	float totalPower = 0;
	float deltaPower = 0;
	/*float yaw = 0;
	float pitch = 0;
	float roll = 0;*/

	void Start(){
		rb = GetComponent<Rigidbody>();
		Debug.developerConsoleVisible = true;
		Debug.Log (Physics.gravity);
	}

	void Update(){
		totalPower += Input.GetAxis ("power")*powerMultiplier;
		deltaPower += Input.GetAxis ("power")*powerMultiplier;
		if (totalPower < 0) {
			deltaPower -= totalPower;
			totalPower -= totalPower;
		}



		if (Input.GetAxisRaw("powerBalance") == 1) {
			float diff = 9.81f*rb.mass - totalPower;
			deltaPower = diff;
			totalPower = 9.81f*rb.mass;
		}

	}
	
	void FixedUpdate(){
		/*rb.AddForceAtPosition(new Vector3(0, deltaPower/4, 0), frontLeft.getPosition());
		rb.AddForceAtPosition(new Vector3(0, deltaPower/4, 0), frontRight.getPosition());
		rb.AddForceAtPosition(new Vector3(0, deltaPower/4, 0), backLeft.getPosition());
		rb.AddForceAtPosition(new Vector3(0, deltaPower/4, 0), backRight.getPosition());*/

		rb.AddRelativeForce (new Vector3(0, deltaPower,0));

		deltaPower = 0;
		//rb.AddForceAtPosition(new Vector3(0, -oldPower/4, 0), frontLeft.getPosition());
		//rb.AddForceAtPosition(new Vector3(0, -oldPower/4, 0), frontRight.getPosition());
		//rb.AddForceAtPosition(new Vector3(0, -oldPower/4, 0), backLeft.getPosition());
		//rb.AddForceAtPosition(new Vector3(0, -oldPower/4, 0), backRight.getPosition());

		//oldPower = power;
		/*yaw += Input.GetAxis ("yaw");
		roll += Input.GetAxis ("roll");
		pitch += Input.GetAxis ("pitch");*/
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