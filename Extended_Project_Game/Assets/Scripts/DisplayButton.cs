using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplayButton : MonoBehaviour {

	public Texture[] textures;
	private int textureID = 0;
	private bool changed = false;
	private RawImage textureComponent;

	// Use this for initialization
	void Start () {
		textureComponent = GetComponent<RawImage> ();
	}

	public void setTexture(int texture){
		if (textureID != texture) {
			changed = true;
		}
		textureID = texture;
	}
	
	// Update is called once per frame
	void Update () {
		if (changed) {
			textureComponent.texture = textures[textureID];
			changed = false;
		}
	}
}
