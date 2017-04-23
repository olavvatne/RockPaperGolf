using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfBallMovement : MonoBehaviour {

	public float magnitude = 100f;
	public float maxForce = 20;

	private int _forceMask;
	private bool _isGrounded;
	private Rigidbody _rb;
	private float _camRayLength = 100f;
	private float _ballRadius;
	
	void Start () {
		_forceMask = LayerMask.GetMask("Force");
		_rb = GetComponent<Rigidbody>();

		//Get the distance from the center of the ball to the ground
		SphereCollider col = GetComponent<SphereCollider>();
		_ballRadius = col.bounds.extents.y;
	}

	void FixedUpdate () {
		if (Input.GetButtonDown("Fire1") && IsGrounded()) {
			Vector3 force = GetForceDirection();
			ApplyForce(force);
		}
	}
	
	private bool IsGrounded() {
		return _isGrounded;
	}
	 void OnCollisionStay (Collision collisionInfo)
	{
		_isGrounded = true;
	}
 
 void OnCollisionExit (Collision collisionInfo)
 {
	 _isGrounded = false;
 }
      
	private Vector3 GetForceDirection() {
		Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit floorHit;

		if (Physics.Raycast (camRay, out floorHit, _camRayLength, _forceMask)) {

			Vector3 playerToMouse = floorHit.point - transform.position;
			playerToMouse.y = 0f;
			return playerToMouse;
		}
		return Vector3.zero;
	}
	private void ApplyForce(Vector3 force) {
		//Vector3 newForce = Vector3.Normalize(force);
		Vector3 newForce = Vector3.ClampMagnitude(force, maxForce);
		_rb.AddForce(force * magnitude);
	}
}
