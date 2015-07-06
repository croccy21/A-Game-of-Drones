using UnityEngine;
using System.Collections;

public class Navball : MonoBehaviour {

	public GameObject drone;
	private Drone droneScript;
	private RectTransform transformRect;

	// Use this for initialization
	void Start () {
		droneScript = drone.GetComponent<Drone> ();
	}
	
	// Update is called once per frame
	void FixedUpdate (){	
		Vector3 rotation = droneScript.getRotation ();
		transform.eulerAngles = new Vector3 (rotation.x, 0, rotation.z);
	}
}
