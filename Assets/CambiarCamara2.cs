using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambiarCamara2 : MonoBehaviour {
	Camera[] camaras;
	public Camera cam1;
	public Camera cam2;

	// Use this for initialization
	void Start () {
		cam1.enabled = true;
		//camaras[1].gameObject.SetActive = true;
		cam2.enabled = false;
	//	cam2.gameObject.SetActive = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.C)) {
			cam1.enabled = !cam1.enabled;

	//		cam1.gameObject.SetActive = !cam1.gameObject.SetAtive;
			cam2.enabled = !cam2.enabled;
	//		cam2.gameObject.SetActive = !cam2.gameObject.SetAtive;
		}
	}
}
