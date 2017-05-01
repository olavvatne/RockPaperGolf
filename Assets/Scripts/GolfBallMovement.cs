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
	private AudioSource _source;
	public AudioClip fire;
	public AudioClip crash;

	private Vector3 joyPos = new Vector3(0f, 0f, 1f);
	public float joySpeed = 100f;

	// class contains handling for both mouse and joystick controls

	void Start () {
		_forceMask = LayerMask.GetMask("Force");
		_floorMask = LayerMask.GetMask("Floor");
		_rb = GetComponent<Rigidbody>();

		//Get the distance from the center of the ball to the ground
		SphereCollider col = GetComponent<SphereCollider>();
		_ballRadius = col.bounds.extents.y;

		_source = GetComponent<AudioSource>();
	}

	void OnCollisionEnter(Collision collision)
    {
		//Set flag if player is able to hit another player during gameplay
        if (collision.gameObject.tag == "Ball" && data != null) {
			data.hitAnotherBall = true;
			_source.PlayOneShot(crash, 0.7F);
		}
    }

	void Update () {
		if (data != null) {
			UpdateJoyStickPosition();
			UpdateStopStatus();
			string control = data.isJoystick ? "Fire1" + data.control : "Fire1";
			if (Input.GetButtonDown(control)  && data.hits < data.maxHits) {
				if (IsGrounded() == true) {
					Vector3 force = Vector3.zero;
					if (data.isJoystick) {
						force = GetForceJoystickDirection();
					}
					else {
						force = GetForceDirection(Input.mousePosition);
					}
					_source.PlayOneShot(fire, 1.0f);
					ApplyForce(force);
					data.hits += 1;
					data.ballStopped = false;
				} 
				else {
					Debug.Log("Not Grounded");
				}
			}
		}
		
	}
	
	private void UpdateStopStatus() {
		data.ballStopped = _rb.velocity.magnitude < stopVelocity;
	}

	private void UpdateJoyStickPosition() {
		if (data.isJoystick) {
			// TODO: joystick working?
			string playerControl = data.control;
			float hor = Input.GetAxis("Horizontal" + playerControl);
			float ver = Input.GetAxis("Vertical" + playerControl);
			Vector3 dir = new Vector3(hor, ver, 0f);
			joyPos = Vector3.Min(joyPos + (dir * joySpeed), new Vector3(Screen.width, Screen.height));
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
