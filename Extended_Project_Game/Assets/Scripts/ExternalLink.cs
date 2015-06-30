using UnityEngine;
using System.Collections;

public class ExternalLink : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	public void openLink(string url){
		print ("opening: " + url);
		Application.OpenURL (url);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
