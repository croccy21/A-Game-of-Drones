using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour {

    public GameObject cameraArm;
    public GameObject camera;
    public GameObject drone;

    public float rotSpeed;
    public float modSpeed;

    public bool armMoving = false;
    public bool cameraMoving = false;

    private Vector3 cameraRotation;
    public Vector3 armRotation;

	// Use this for initialization
	void Start () {
        cameraRotation = camera.transform.localRotation.eulerAngles;
        armRotation = cameraArm.transform.localRotation.eulerAngles;
    }
	
	// Update is called once per frame
	void Update () {
        cameraArm.transform.position = drone.transform.position;

        if (!armMoving)
        {
            cameraArm.transform.rotation = drone.transform.rotation;
        }

        if (Input.GetAxisRaw("cameraRot") == 1)
        {
            if (Input.GetAxisRaw("cameraRotMod") == 1)
            {
                float deltaX = Input.GetAxis("cameraX") * modSpeed * Time.deltaTime;
                float deltaY = Input.GetAxis("cameraY") * modSpeed * Time.deltaTime;

                cameraRotation += new Vector3(-deltaY, deltaX, 0);
                armMoving = false;
            }
            else
            {
                float deltaX = Input.GetAxis("cameraX") * rotSpeed * Time.deltaTime;
                float deltaY = Input.GetAxis("cameraY") * rotSpeed * Time.deltaTime;

                armRotation += new Vector3(0, deltaX, deltaY);
                armMoving = true;
            }
        }

        cameraArm.transform.localRotation = Quaternion.Euler(armRotation);
        camera.transform.localRotation = Quaternion.Euler(cameraRotation);
    }
}
