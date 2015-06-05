using UnityEngine;
using System.Collections;

public class CamMove : MonoBehaviour {

	public Vector3 originalCentre = new Vector3(-0.5f, 0.5f*Mathf.Tan((1/9)*Mathf.PI), 0);
	Vector3 newCentre;
	public int rotMultiplier=2;


	public Transform target;

	void start(){
		newCentre = originalCentre;
	}
	
	void LateUpdate()	{
		transform.position = target.position + originalCentre;
		print (target.position);
	}
}
