using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour {
	
	public GameObject intro;

	// Use this for initialization
	void Start () {
		mostrarIntro ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}



	public void OK(){		
		Time.timeScale = 1;
		intro.SetActive(false);
        Debug.Log("Hola");

	}

	public void mostrarIntro(){
		Time.timeScale = 0;
		intro.SetActive(true);
	}
}
