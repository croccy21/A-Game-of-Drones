using UnityEngine;
using System.Collections;

public class RadioControl : MonoBehaviour {

	public class RadioRaycastData{
		public int mode;
		public bool hit;
		public float fadeRatio;
		public float distanceRatio;
		public RadioRaycastData(){
			
		}
		public static bool operator >(RadioRaycastData x, RadioRaycastData y){
			if (!x.hit && y.hit) {
				return false;
			}else if (x.hit && !y.hit){
				return true;
			} else if (x.mode < y.mode) {
				print (string.Format("x mode {}; y mode {}", x.mode, y.mode));
				return true;
			} else {
				if (x.mode == STATE_OUT_OF_RANGE) {
					return false;
				} else if (x.mode == STATE_ON_BORDER) {
					if (x.fadeRatio < y.fadeRatio) {
						return true;
					} else {
						return false;
					}
				} else if (x.mode == STATE_IN_RANGE){
					if(x.distanceRatio < y.distanceRatio){
						return true;
					} else{
						return false;
					}
				}
				else{
					return false;
				}
			}
		}
		public static bool operator <(RadioRaycastData x, RadioRaycastData y){
			if (!x.hit && y.hit) {
				return true;
			}else if (x.hit && !y.hit){
				return false;
			} else if (x.mode > y.mode) {
				return true;
			} else {
				if (x.mode == STATE_OUT_OF_RANGE) {
					return false;
				} else if (x.mode == STATE_ON_BORDER) {
					if (x.fadeRatio > y.fadeRatio) {
						return true;
					} else {
						return false;
					}
				} else if (x.mode == STATE_IN_RANGE){
					if(x.distanceRatio > y.distanceRatio){
						return true;
					} else{
						return false;
					}
				} else{
					return false;
				}
			}
		}

		public override string ToString(){
			return string.Format ("Obstructed:{0}, mode:{1}", !hit, mode);
		}
	}

	GameObject droneObject;
	Rigidbody drone;
	GameObject line;
	LineRenderer l;
	public bool active = true;
	
	public int maxRange = 100;
	public float fadeStartRatio = .2f;

	public static int STATE_IN_RANGE = 0;
	public static int STATE_ON_BORDER = 1;
	public static int STATE_OUT_OF_RANGE = 2;

	public static int ERROR_UNSPECIFIED = -1;

	void Start(){
		droneObject = GameObject.Find("drone");
		drone = droneObject.GetComponent<Rigidbody>();
		line = new GameObject ();
		Instantiate(line);
		line.AddComponent<LineRenderer>();
		l = line.GetComponent<LineRenderer> ();
		l.enabled = false;
		l.SetWidth(.001f, .001f);
		l.SetColors(Color.white, Color.white);
		l.SetVertexCount (2);
		l.material = new Material (Shader.Find("Particles/Additive"));
		
	}

	public RadioRaycastData checkLineOfSight(){
		if (drone != null) {
			Vector3 end = drone.position;
			Vector3 origin = transform.position;
			//print (origin.ToString() + "-->" + end.ToString());
			l.SetPosition (0, origin);
			l.SetPosition (1, end);
			float angle = Vector3.Angle (origin, end); 
			l.enabled=true;
			RaycastHit hit;
			RadioRaycastData data = new RadioRaycastData ();
			if (Physics.Linecast (origin, end, out hit)) {
				//print (hit.distance);
				if (hit.rigidbody==null || hit.rigidbody.position != drone.position) {
					data.hit = false;
					l.SetColors(Color.red, Color.red);
				} else {
					if (hit.distance > maxRange) {
						data.mode = STATE_OUT_OF_RANGE;
						l.SetColors(Color.grey, Color.grey);
					} else if (hit.distance > maxRange * (1 - fadeStartRatio)) {
						data.fadeRatio = (maxRange - hit.distance) / (maxRange * fadeStartRatio);
						data.mode = STATE_ON_BORDER;
						l.SetColors(Color.yellow, Color.yellow);
					} else {
						data.mode = STATE_IN_RANGE;
						l.SetColors(Color.green, Color.green);
					}
					data.hit = true;
					data.distanceRatio = hit.distance / maxRange;
				}
			} else {
				data.hit = false;
			}
			return data;
		} else {
			return null;
		}
	}


}
