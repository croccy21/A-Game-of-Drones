using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class animatedImage : MonoBehaviour {

	public Sprite[] frames;
	public int fps = 10;
	private float timer = 0;
	
	// Update is called once per frame
	void Update () {
		if (enabled) {
			timer = (timer + Time.fixedDeltaTime * fps) % frames.Length;
            if (timer >= 3)
            {
                timer = 0;
            }
			GetComponent<Image> ().sprite = frames[Mathf.FloorToInt(timer)];
		}
	}
}
