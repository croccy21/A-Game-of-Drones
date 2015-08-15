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

	private bool countdownOn = false;
	private RadioControl[] radioControlList;
	public RadioControl connectedTo;
	private StaticControl staticControl;


	void Start () {
		drone = GetComponent<Rigidbody>();
		radioControlList = (RadioControl[])Resources.FindObjectsOfTypeAll (typeof(RadioControl));

		GameObject c = GameObject.Find("StaticCanvas");
		staticControl = c.GetComponent<StaticControl> ();
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
	
	IEnumerator countdown(){
		countdownOn = true;
		for (int i=10; i>=0; i--) {
			staticControl.setCountdown(i);
			yield return new WaitForSeconds (1);
		}
		Time.timeScale = 0;
	}

	void connectionLost(){
		staticControl.setAlpha (1);
		staticControl.activate ();
		if (!countdownOn) {
			StartCoroutine("countdown");
		}
		//print ("Connection Lost");
	}

	void connectionBoarder(float ratio){
		staticControl.setAlpha (1-ratio);
		staticControl.activate ();
		if (countdownOn) {
			StopCoroutine("countdown");
            staticControl.setCountdown(-1);
			countdownOn=false;
		}
		//print ("Loosing Conenction");
	}

	void connectionCreated(){
		staticControl.deactivate();
		staticControl.setAlpha (0);
		if (countdownOn) {
            StopCoroutine("countdown");
			staticControl.setCountdown(-1);
			countdownOn=false;
		}
		//print ("Connected");
	}

	void updateRacasts(){
		bool tryReconect = false;
		RadioControl.RadioRaycastData current = null;
		if (connectedTo != null && connectedTo.isActiveAndEnabled) {
			current = connectedTo.checkLineOfSight ();
			//print(current);
			if (current.hit==false || current.mode != RadioControl.STATE_IN_RANGE) {
				tryReconect=true;
			}
		} else {
			tryReconect=true;
		}

		if (tryReconect) {
			RadioControl.RadioRaycastData best = null;
			RadioControl bestConnection = null;
			if (current != null) {
				best = current;
				bestConnection = connectedTo;
			}
			foreach (RadioControl r in radioControlList) {
				if (r.isActiveAndEnabled) {
					RadioControl.RadioRaycastData data = r.checkLineOfSight ();
					/*if(best!=null){
						print(data.ToString() + ">" + best.ToString());
						print (data>best);
					
					}*/
					if (best==null || data>best){
						best = data;
						bestConnection = r;
					}
				}
			}
			connectedTo = bestConnection;
			current = best;
		}

		if (!current.hit || current.mode==RadioControl.STATE_OUT_OF_RANGE) {
			connectionLost();
		}
		if (current.mode==RadioControl.STATE_ON_BORDER){
			connectionBoarder(current.fadeRatio);
		}
		
		if (current.mode==RadioControl.STATE_IN_RANGE && current.hit){
			connectionCreated();
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
		updateRacasts ();
	}
}
