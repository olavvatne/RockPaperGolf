using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfBallMovement : MonoBehaviour {

	public float magnitude = 100f;
	public float maxForce = 20;

	private int _floorMask;
	private int _forceMask;
	private bool _isGrounded;
	private Rigidbody _rb;
	private float _camRayLength = 100f;
	private float _ballRadius;
	
	void Start () {
		_forceMask = LayerMask.GetMask("Force");
		_floorMask = LayerMask.GetMask("Floor");
		_rb = GetComponent<Rigidbody>();

		//Get the distance from the center of the ball to the ground
		SphereCollider col = GetComponent<SphereCollider>();
		_ballRadius = col.bounds.extents.y;
	}

	void Update () {
		if (Input.GetButtonDown("Fire1")) {
			if (IsGrounded() == true) {
				Debug.Log("Grounded");
				Vector3 force = GetForceDirection();
				ApplyForce(force);
			} 
			else {
				Debug.Log("Not Grounded");
			}
			
		}
	}
	
	private bool IsGrounded() {
		// SphereCast uses world space. Checks downward to see if there is ground.
		RaycastHit hit;
        Vector3 center = transform.position;
		float rayMaxDist = _ballRadius + 1f;
		Vector3 down = new Vector3 (0,-1,0);
        if (Physics.SphereCast(center, _ballRadius - 0.1f, down , out hit, rayMaxDist, _floorMask ))
        {
            return true;
        }
		return false;
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
		Vector3 newForce = Vector3.ClampMagnitude(force, maxForce);

		//Exisiting force is reset, easier to change direction.
		_rb.velocity = Vector3.zero;
		_rb.AddForce(force * magnitude);
	}
}
