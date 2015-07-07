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
		Quaternion rotation = droneScript.getRotation ();
		transform.rotation = rotation;
	}
}
