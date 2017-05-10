using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRotate : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.RotateAroundLocal(Vector3.up + Vector3.left, 0.5f * Time.deltaTime);
	}
}
