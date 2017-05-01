using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfBallMovement : MonoBehaviour {

	public float magnitude = 100f;
	public float maxForce = 20;

	public float stopVelocity = 0.05f;
	[HideInInspector] public PlayerData data;
	private int _floorMask;
	private int _forceMask;
	private bool _isGrounded;
	private Rigidbody _rb;
	private float _camRayLength = 100f;
	private float _ballRadius;
	private bool isJoystick = false; 
	private Vector3 joyPos = new Vector3(0f, 0f, 1f);
	public float joySpeed = 100f;

	// class contains handling for both mouse and joystick controls

	void Start () {
		_forceMask = LayerMask.GetMask("Force");
		_floorMask = LayerMask.GetMask("Floor");
		_rb = GetComponent<Rigidbody>();
		CheckIfJoystick();

		//Get the distance from the center of the ball to the ground
		SphereCollider col = GetComponent<SphereCollider>();
		_ballRadius = col.bounds.extents.y;
	}

	void CheckIfJoystick() {
		isJoystick = Input.GetJoystickNames().Length > 0 ? true : false;
	}

	void Update () {
		UpdateJoyStickPosition();
		UpdateStopStatus();
		if (Input.GetButtonDown("Fire1_" + GetControl())  && data.hits < data.maxHits) {
			if (IsGrounded() == true) {
				Vector3 force = Vector3.zero;
				if (isJoystick) {
					force = GetForceJoystickDirection();
				}
				else {
					force = GetForceDirection(Input.mousePosition);
				}
				ApplyForce(force);
				data.hits += 1;
				data.ballStopped = false;
			} 
			else {
				Debug.Log("Not Grounded");
			}
		}
	}
	
	private void UpdateStopStatus() {
		data.ballStopped = _rb.velocity.magnitude < stopVelocity;
	}

	private string GetControl() {
		if (data != null) {
			return data.control;
		}
		return "P1";
	}

	private void UpdateJoyStickPosition() {
		//TODO: here? and better way of controller it.
		string playerControl = GetControl();
		float hor = Input.GetAxis("Horizontal_" + playerControl);
		float ver = Input.GetAxis("Vertical_" + playerControl);
		Vector3 dir = new Vector3(hor, ver, 0f);
		joyPos = Vector3.Min(joyPos + (dir * joySpeed), new Vector3(Screen.width, Screen.height));
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
      
	private Vector3 GetForceJoystickDirection() {
		return GetForceDirection(joyPos);
	}
	
	private Vector3 GetForceDirection(Vector3 screenPos) {
		Ray camRay = Camera.main.ScreenPointToRay(screenPos);
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
		_rb.AddForce(newForce * magnitude);
	}
}
