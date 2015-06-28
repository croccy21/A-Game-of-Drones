using UnityEngine;
using System.Collections;

public class Drone : MonoBehaviour {

	//Does not deal with button detection

	private static float RAD_2_DEG = Mathf.PI/180;

	private Rigidbody drone;

	public float torqueCoefficient = 1f;
	public float forceCoefficient = 1f;
	
	public float maxForce = 5;
	public float minForce = 0;

	private float force = 0;
	private float deltaYaw = 0;
	public float deltaPitch = 0;
	public float deltaRoll = 0;

	private static Vector3 directionUp  = new Vector3(0, 1, 0);

	public int balanceGravityMode = 0;
	private float balanceGravityDisplacement = 0f;
	public float balanceGravityVelocityCoefficient = .2f;
	public float balanceGravityDisplacementCoefficient = .3f;

	private int balanceRotationMode = 0;
	public Vector3 balanceRotationPosition = new Vector3(0, 0, 0);

	void Start () {
		drone = GetComponent<Rigidbody>();
	}

	public void changeForce(float deltaForce){
		force += deltaForce * forceCoefficient;
		if (balanceGravityMode > 0) {
			if (deltaForce!=0){
				balanceGravityMode = 2;
			} else{
				balanceGravityMode = 1;
			}
		}
	}

	public float getForce(){
		return force;
	}

	public int getBanlanceGravityMode(){
		return balanceGravityMode;
	}

	public int getBanlanceRotationMode(){
		return balanceRotationMode;
	}

	private void calculateForce(){
		if (balanceGravityMode != 1) {
			balanceGravityDisplacement=drone.position.y;
		}
		
		if (balanceGravityMode==1) {
			float deltaHeight = balanceGravityDisplacement - drone.position.y;
			float deltaForce = drone.velocity.y * balanceGravityVelocityCoefficient 
				- deltaHeight * balanceGravityDisplacementCoefficient;
			force -= deltaForce;
		}
		
		if (force > maxForce) {
			force = maxForce;
		}
		if (force < minForce) {
			force = minForce;
		}
	}

	public void changeRotation(float roll, float yaw, float pitch){
		this.deltaRoll += roll;
		this.deltaYaw += yaw;
		this.deltaPitch += pitch;
	}

	private void calculateRotation(){
		if (balanceRotationMode == 1) {
			deltaRoll+=Mathf.Sin(drone.rotation.eulerAngles.x*RAD_2_DEG);
			deltaPitch+=Mathf.Sin(drone.rotation.eulerAngles.z*RAD_2_DEG);
		}
		print (Mathf.Sin(drone.rotation.eulerAngles.x*RAD_2_DEG) + " --> " + deltaRoll + ", " 
		       + Mathf.Sin(drone.rotation.eulerAngles.z*RAD_2_DEG) + " --> " + deltaPitch);


	}

	public void resetRotation(){
		drone.MoveRotation(Quaternion.Euler (new Vector3 (0, 0, 0)));
	}

	public void toggleBalanceGravity(){
		if (balanceGravityMode > 0) {
			balanceGravityMode = 0;
		} else {
			balanceGravityMode = 1;
		}
	}

	public void toggleBalanceRotation(){
		if (balanceRotationMode == 1) {
			balanceRotationMode = 0;
		} else {
			balanceRotationMode = 1;
		}
	}





	
	// Update is called once per frame
	void FixedUpdate () {
		calculateForce ();
		calculateRotation ();

		drone.AddRelativeTorque (torqueCoefficient*Mathf.Pow(force, 0.5f)*(new Vector3 (-deltaRoll, deltaYaw, -deltaPitch)));
		deltaRoll = 0;
		deltaPitch = 0;
		deltaYaw = 0;
		drone.AddRelativeForce (force * directionUp);

	}
}
