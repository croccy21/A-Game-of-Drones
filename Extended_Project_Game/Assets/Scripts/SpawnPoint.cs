using UnityEngine;
using System.Collections;
using System;

public class SpawnPoint : MonoBehaviour {
    public Vector3 getLocation()
    {
        return transform.position + new Vector3(0, 0.003f, 0);
    }

    public Vector3 getCoords()
    {
        return transform.position;
    }
}
