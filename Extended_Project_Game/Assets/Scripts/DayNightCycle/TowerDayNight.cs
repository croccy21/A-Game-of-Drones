using UnityEngine;
using System.Collections;

public class TowerDayNight : DayNightBase {

	public GameObject[] lightObjects;
	private LightOnOff[] lights;

	void Start(){
		lights = new LightOnOff[lightObjects.Length];
		for (int i = 0; i<lightObjects.Length; i++) {
			lights[i] = lightObjects[i].GetComponent<LightOnOff>();
		}
	}

	public override void startDay(){
		StopCoroutine ("flash");
		foreach (LightOnOff light in lights) {
			light.turnOff();
		}
	}
	public override void startNight(){
		StartCoroutine ("flash", Random.Range(0f,4f));
	}
	private IEnumerator flash(float startDelay){
		yield return new WaitForSeconds (startDelay);
		while (true) {
			foreach (LightOnOff light in lights) {
				light.turnOn();
			}
			yield return new WaitForSeconds(2);
			foreach (LightOnOff light in lights) {
				light.turnOff();
			}
			yield return new WaitForSeconds(2);
		}

	}
}
