using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FeedbackButtonAnim : MonoBehaviour {

	static float smallWidth = 29;
	static float largeWidth = 84;
	static float smallTextWidth = 9;
	static float largeTextWidth = 64;
	static float height = 30;

	private RectTransform buttonRect;
	private RectTransform textRect;

	public float speed = 0.1f;

	// Use this for initialization
	void Start () {
		//GameObject button = GameObject.FindGameObjectWithTag("FeedbackButton");
		buttonRect = GetComponent<RectTransform> ();
		GameObject text = GameObject.Find("FeedbackText");
		textRect = text.GetComponent<RectTransform> ();
	}

	IEnumerator doStretchAnim(){
		while (buttonRect.rect.width<largeWidth) {
			float deltaWidth = Time.deltaTime * speed;
			float newButtonWidth = buttonRect.rect.width + deltaWidth;
			float newTextWidth = textRect.rect.width + deltaWidth;

			if (newButtonWidth>largeWidth){
				newButtonWidth=largeWidth;
			}
			if (newTextWidth>largeTextWidth){
				newTextWidth=largeTextWidth;
			}

			buttonRect.sizeDelta = new Vector2(newButtonWidth, height);
			textRect.sizeDelta = new Vector2(newTextWidth, height);
			yield return null;
		}
		yield return null;
	}

	IEnumerator doContractAnim(){
		while (buttonRect.rect.width>smallWidth) {
			float deltaWidth = Time.deltaTime * speed;
			float newButtonWidth = buttonRect.rect.width - deltaWidth;
			float newTextWidth = textRect.rect.width - deltaWidth;

			if (newButtonWidth<smallWidth){
				newButtonWidth=smallWidth;
			}
			if (newTextWidth<smallTextWidth){
				newTextWidth=smallTextWidth;
			}

			buttonRect.sizeDelta = new Vector2(newButtonWidth, height);
			textRect.sizeDelta = new Vector2(newTextWidth, height);
			yield return null;
		}
		yield return null;
	}

	public void animationStretch(){
		StartCoroutine (doStretchAnim ());
	}

	public void animationContract(){
		StartCoroutine (doContractAnim ());
	}

	public void beat(string var){
		print (var);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
