using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CameraFollow : MonoBehaviour {

	public Transform[] targets;
	public float smoothing = 5f;
	Vector3 offset;

	// Use this for initialization
	void Start () {
		Assert.IsNotNull(targets);
		Assert.IsTrue(targets.Length > 0, "Targets on camera not set");
		offset = transform.position - targets[0].position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		// TODO: error after object has been destroyed.
		if (targets[0]) {
			Vector3 targetCamPos = targets[0].position + offset;
			transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
		}
		
	}
}
