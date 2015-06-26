using UnityEngine;
using System.Collections;

public class Drone : MonoBehaviour {

	//Does not deal with button detection

	private Rigidbody drone;

	public float torqueCoefficient = 1f;
	public float forceCoefficient = 1f;
	
	public float maxForce = 5;
	public float minForce = 0;

	private float force = 0;
	private float yaw = 0;
	private float pitch = 0;
	private float roll = 0;

	private static Vector3 directionUp  = new Vector3(0, 1, 0);

	private int balanceGravityMode = 0;
	private float balanceGravityDisplacement = 0f;
	public float balanceGravityVelocityCoefficient = .2f;
	public float balanceGravityDisplacementCoefficient = .3f;

	private int balanceRotationMode = 0;
	public Vector3 balanceRotationPosition = new Vector3(0, 0, 0);

	// Use this for initialization
	void Start () {
		
	}

	public void changeForce(float deltaForce){
		force += deltaForce * forceCoefficient;
		if (balanceGravityMode > 0) {
			balanceGravityMode = 2;
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
		
		if (balanceGravityMode>0) {
			float deltaHeight = balanceGravityDisplacement - drone.position.y;
			float deltaForce = drone.velocity.y * balanceGravityVelocityCoefficient 
				- deltaHeight * balanceGravityDisplacementCoefficient;
			force -= deltaForce;
			balanceGravityMode = balanceGravityMode>0 ? 1 : 0;
		} else if (balanceGravityMode>0) {
			balanceGravityMode = 2;
		}
		
		if (force > maxForce) {
			force = maxForce;
		}
		if (force < minForce) {
			force = minForce;
		}
	}







	
	// Update is called once per frame
	void Update () {
	
	}
}
