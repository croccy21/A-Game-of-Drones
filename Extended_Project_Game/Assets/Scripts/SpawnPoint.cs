using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour {
    public Vector3 getLocation()
    {
        return transform.position + new Vector3(0, 0.003f, 0);
    }
}
