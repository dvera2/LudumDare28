using UnityEngine;
using System.Collections;

public class CameraSettings : MonoBehaviour {

	public float size = 5;

	// Use this for initialization
	void Start () {
		var camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
		camera.transform.position = transform.position;
		camera.orthographicSize = size;
	}
}
