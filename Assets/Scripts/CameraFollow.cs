using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CameraFollow : MonoBehaviour {

	public Transform[] targets;
	public Transform mainTarget;
	public float smoothing = 5f;
	Vector3 offset;

	// Use this for initialization
	void Start () {
	}
	
	public void SetTargets(Transform[] targets) {
		Assert.IsNotNull(targets);
		Assert.IsTrue(targets.Length > 0, "Camera setter will have no targets");
		this.targets = targets;
		this.mainTarget = targets[0];
		offset = transform.position - mainTarget.position;
	}

	public void MoveTo(Transform target) {
		mainTarget = target;
	}
	// Update is called once per frame
	void FixedUpdate () {
		// TODO: error after object has been destroyed.
		if (mainTarget) {
			Vector3 targetCamPos = mainTarget.position + offset;
			transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
		}
		
	}
}
