using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparo : MonoBehaviour {
	public GameObject balaNueva;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.P)){
			GameObject clonBala= Instantiate (balaNueva, transform.position, transform.rotation) as GameObject;
		}
		if(ControlDisparo.disparoVirtual&Input.GetMouseButtonDown(0)){
			GameObject clonBala = Instantiate (balaNueva, transform.position, transform.rotation) as GameObject;
		}
	}
}
