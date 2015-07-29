using UnityEngine;
using System.Collections;

public class PauseMenuController : MonoBehaviour {

	public GameObject[] menu;
	private GameObject currentMenu;
	private int currentID = -1;
	private Canvas globalCanvas;

	private bool pauseButtonPressed = false;

	// Use this for initialization
	void Start () {
		globalCanvas = GetComponent<Canvas> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxisRaw ("pause") == 1) {
			if (!pauseButtonPressed) {
				if (currentID < 0) {
					setMenu (0);
				} else {
					setMenu (-1);
				}
				pauseButtonPressed = true;
			}
		} else if (pauseButtonPressed) {
			pauseButtonPressed = false;
		}
	}

	public void setMenu(int menuID){
		if (menuID < menu.Length) {
			currentID = menuID;
			if (menuID >= 0) {
				if (!globalCanvas.enabled) {
					globalCanvas.enabled = true;
					Time.timeScale=0;
				}
				if (currentMenu != null) {
					currentMenu.SetActive (false);
				}
				menu [menuID].SetActive (true);
				currentMenu = menu [menuID];
			} else if (menuID == -1) {
				globalCanvas.enabled = false;
				if (currentMenu != null) {
					currentMenu.SetActive (false);
				}
				Time.timeScale=1;
				currentMenu = null;
			}
		}
	}
}
