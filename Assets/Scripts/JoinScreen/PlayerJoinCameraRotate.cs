using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJoinCameraRotate : MonoBehaviour {
	public float speed = 2;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Rotate(Vector3.up * Time.deltaTime * speed);
	}
}
