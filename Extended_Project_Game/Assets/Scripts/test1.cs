using UnityEngine;
using System.Collections;

public class test1 : MonoBehaviour {

	// Use this for initialization
	private Rigidbody rb;
	public bool forward;
	public bool backward;
	public bool left;
	public bool right;
	public bool up;
	public bool down;
	
	void Start () {
		rb = GetComponent<Rigidbody>();
		forward = true;
		backward = true;
		left = true;
		right = true;
		up = true;
		down = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate(){
		if (forward) {
			rb.AddRelativeForce (new Vector3 (1, 0, 0));
			forward=false;
		}
		if (backward) {
			rb.AddRelativeForce (new Vector3 (-1, 0, 0));
			backward = false;
		}
		if (left) {
			rb.AddRelativeForce (new Vector3 (0, 0, 1));
			left = false;
		}
		if (right) {
			rb.AddRelativeForce (new Vector3 (0, 0, -1));
			right = false;
		}
		if (up) {
			rb.AddRelativeForce (new Vector3 (0, 1, 0));
			up = false;
		}
		if (down) {
			rb.AddRelativeForce (new Vector3 (0, -1, 0));
			down = false;
		}
	}
}
