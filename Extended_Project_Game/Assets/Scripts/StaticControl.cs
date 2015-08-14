using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StaticControl : MonoBehaviour {

	public Image image;
	public Canvas canvas;
	CanvasGroup canvasGroup;

	// Use this for initialization
	void Start () {
		canvasGroup = canvas.GetComponent<CanvasGroup> ();
	}

	public void deactivate(){
		//canvas.enabled = false;
		canvasGroup.alpha = 0;
		//print ("Deactivated");
		image.enabled = false;
	}

	public void activate(){
		//canvas.enabled = true;
		image.enabled = true;
		//print ("Activated");
	}

	public void setAlpha(float a){
		canvasGroup.alpha = a;
	}
}
