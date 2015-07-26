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
		print ("sub");
		foreach (LightOnOff light in lights) {
			light.turnOff();
		}
	}
	public override void startNight(){
		foreach (LightOnOff light in lights) {
			light.turnOn();
		}
	}
}
