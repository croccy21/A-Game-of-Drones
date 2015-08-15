using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StaticControl : MonoBehaviour {

	public Image image;
	public Text countdown;
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

	public void setCountdown(int n){
		if (n > 0) {
			countdown.enabled = true;
			countdown.text = string.Format ("{0}", n);
		} else if (n == 0) {
			countdown.enabled = true;
			countdown.text = "Connection Timeout";
		} else {
			countdown.enabled=false;
		}
	}
}
