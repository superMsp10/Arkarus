﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody>().centerOfMass = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
