﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capsula : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (new Vector3 (75, 50, 25) * Time.deltaTime);
	}
}
