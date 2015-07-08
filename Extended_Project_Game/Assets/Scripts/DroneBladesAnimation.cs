using UnityEngine;
using System.Collections;

public class DroneBladesAnimation : MonoBehaviour {

	public GameObject drone;
	private Drone droneController;
	private Animator anim;
	private int paramHash = Animator.StringToHash("droneForce");

	// Use this for initialization
	void Start () {
		droneController = drone.GetComponent<Drone> ();
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		anim.SetFloat (paramHash, droneController.getForce ()*5);
	}
}
