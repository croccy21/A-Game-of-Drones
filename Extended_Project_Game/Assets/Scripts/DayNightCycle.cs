using UnityEngine;
using System.Collections;

public class DayNightCycle : MonoBehaviour {

	private Transform transform;
	public float timeConstant = 0.1f;

	// Use this for initialization
	void Start () {
		transform = GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.Rotate (new Vector3 (Time.deltaTime*timeConstant, 0 , 0));
	}
}
