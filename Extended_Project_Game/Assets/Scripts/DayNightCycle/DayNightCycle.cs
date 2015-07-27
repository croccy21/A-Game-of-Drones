using UnityEngine;
using System.Collections;

public class DayNightCycle : MonoBehaviour {

	public float timeConstant = 0.1f;
	public float worldTime = 0f;
	private bool setNight = false;
	private int day = 0;
	public GameObject[] dayNightUpdateList;
	public DayNightBase[] dayNightUpdate;

	// Use this for initialization
	void Start () {
		dayNightUpdate = new DayNightBase[dayNightUpdateList.Length];
		for (int i = 0; i<dayNightUpdateList.Length; i++) {
			dayNightUpdate[i] = dayNightUpdateList[i].GetComponent<DayNightBase>();
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		worldTime += Time.deltaTime * timeConstant;
		if (worldTime > 360) {
			worldTime+=1;
			worldTime=worldTime-360;
		}
		transform.rotation =  Quaternion.Euler(new Vector3 (worldTime, 205, 0));

		if (worldTime >= 90 && worldTime < 270 && !setNight) {
			foreach(DayNightBase item in dayNightUpdate){
				item.startNight();
				setNight = true;
			}
		}
		if ((worldTime >= 270 || worldTime < 90) && setNight) {
			foreach(DayNightBase item in dayNightUpdate){
				item.startDay();
				setNight = false;
			}
		}
	}

	public float getTime(){
		return worldTime;
	}
	public void setTime(float time){
		worldTime = time;
	}
	public bool isNight(){
		return (worldTime > 90 && worldTime < 270);
	}
	public bool isDay(){
		return !isNight ();
	}
}
