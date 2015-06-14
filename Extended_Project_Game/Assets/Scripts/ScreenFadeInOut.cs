using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScreenFadeInOut : MonoBehaviour {

	

	public CanvasGroup canvasGroup;
	public float fadeSpeed = 2f;

	void Start (){
		GameObject image =  GameObject.Find ("screenFader");
		canvasGroup = GetComponent<CanvasGroup> ();
		image.GetComponent<RectTransform> ().sizeDelta = new Vector2 (Screen.width, Screen.height);
		canvasGroup.alpha = 0;
	}


	public void fadeToBlack(){
		//print ("Start fadeout");
		StartCoroutine (DoFadeOut ());
	}
	public void fadeToClear(){
		StartCoroutine (DoFadeIn ());
	}


	public IEnumerator DoFadeOut(){
		while (canvasGroup.alpha<1) {
			float newAlpha = canvasGroup.alpha + Time.deltaTime / fadeSpeed;
			if(newAlpha>1f){
				newAlpha = 1;
			}
			canvasGroup.alpha = newAlpha;
			yield return null;
		}
		canvasGroup.alpha = 1f;
		//canvasGroup.interactable = false;
		//print ("Faded out");
		yield return null;
	}

	public IEnumerator DoFadeIn(){
		while (canvasGroup.alpha>0) {
			canvasGroup.alpha -= Time.deltaTime / fadeSpeed;
			yield return null;
		}
		canvasGroup.alpha = 0f;
		//print ("Faded in");
		//canvasGroup.interactable = false;
		yield return null;
	}
}
