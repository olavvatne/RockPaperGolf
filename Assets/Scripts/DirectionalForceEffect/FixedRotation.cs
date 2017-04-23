using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedRotation : MonoBehaviour {

	private Quaternion  _rotation;

	// Use this for initialization
	void Start () {
		_rotation = transform.rotation;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.rotation = _rotation;
	}
}
