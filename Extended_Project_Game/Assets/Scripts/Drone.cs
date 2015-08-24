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
	public float maxDeltaForce = 1;

	private static Vector3 directionUp  = new Vector3(0, 1, 0);

	public int balanceGravityMode = 0;
	private float balanceGravityDisplacement = 0f;
	public float balanceGravityVelocityCoefficient = .2f;
	public float balanceGravityDisplacementCoefficient = .3f;

	private int balanceRotationMode = 0;
	public Vector3 balanceRotationPosition = new Vector3(0, 0, 0);

    public SpawnPoint lastSpawnpoint;

	void Start () {
		drone = GetComponent<Rigidbody>();
	}

	public void changeForce(float deltaForce){
		force += deltaForce * forceCoefficient;
		if (balanceGravityMode > 0) {
			if (deltaForce!=0){
				if (balanceGravityMode==1){
					force = calculateRequiredForce();
				}
				balanceGravityMode = 2;
			} else{
				balanceGravityMode = 1;
			}
		}
	}

    public Vector3 getCoords()
    {
        return drone.position;
    }

	public void setForce(float force){
		this.force = force;
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

	private float calculateRequiredForce(){
		Vector3 model = drone.rotation * directionUp;
		float verticleRequiredForce = Physics.gravity.y * drone.mass;
		float ratio = verticleRequiredForce / model.y;
		model = ratio * model;
		return Mathf.Sqrt (Mathf.Pow (model.x, 2) + Mathf.Pow (model.y, 2) + Mathf.Pow (model.z, 2));
	}

	private void calculateForce(){

		if (balanceGravityMode != 1) {
			balanceGravityDisplacement=drone.position.y;
		}
		
		if (balanceGravityMode==1) {
			float requiredForce = calculateRequiredForce();
			float deltaHeight = balanceGravityDisplacement - drone.position.y;
			float deltaForce = drone.velocity.y * balanceGravityVelocityCoefficient 
				- deltaHeight * balanceGravityDisplacementCoefficient;

			float estimatedForce = force - deltaForce;

			if (estimatedForce>requiredForce+maxDeltaForce){
				force = requiredForce+maxDeltaForce;
			}
			else if(estimatedForce<requiredForce-maxDeltaForce){
				force = requiredForce-maxDeltaForce;
			}
			else {
				force -= deltaForce;
			}
		}
		
		if (force >= maxForce) {
			force = maxForce;
		}
		if (force <= minForce) {
			force = minForce;
		}
	}

	public void changeRotation(float roll, float yaw, float pitch){
		this.deltaRoll += roll;
		this.deltaYaw += yaw;
		this.deltaPitch += pitch;
	}

	private float angleToTorque(float theta){
		float sinTheta = Mathf.Sin (theta);
		return Mathf.Sign(sinTheta)*Mathf.Pow (sinTheta*2, 2f);
	}

	private void calculateRotation(){
		if (balanceRotationMode == 1) {
			deltaRoll+=angleToTorque(drone.rotation.eulerAngles.x*RAD_2_DEG);
			deltaPitch+=angleToTorque(drone.rotation.eulerAngles.z*RAD_2_DEG);
		}
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

	public Quaternion getRotation(){
		return drone.rotation;
	}

    public void respawn()
    {
        respawn(lastSpawnpoint);
    }

    public void respawn(SpawnPoint spawnPoint)
    {
        resetRotation();
        drone.position = spawnPoint.getLocation();
        setForce(0);
        drone.velocity = new Vector3(0, 0, 0);
        drone.angularVelocity = new Vector3(0, 0, 0);
        balanceGravityMode = 0;
        balanceRotationMode = 0;
        //drone.
    }

    public void setSpawn(SpawnPoint spawnPoint)
    {
        lastSpawnpoint = spawnPoint;
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
