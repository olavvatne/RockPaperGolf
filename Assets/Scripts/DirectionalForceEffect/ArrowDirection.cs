using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowDirection : MonoBehaviour {
	public float joySpeed = 100f;
	private int _forceMask;
	private float _camRayLength = 100f;
	private bool isJoystick = false;
	public PlayerData player;

	private Vector3 joyPos = new Vector3(0f, 0f, 1f);
	// Use this for initialization
	void Start () {
		_forceMask = LayerMask.GetMask("Force");
		CheckIfJoystick();
	}
	
	void CheckIfJoystick() {
		isJoystick = Input.GetJoystickNames().Length > 0 ? true : false;
		Debug.Log(Input.GetJoystickNames());
	}
	
	// Update is called once per frame
	void Update () {
		if (isJoystick) {
			UpdateArrowJoyStick();
		}
		else {
			if (player.control == "P1") {
				UpdateArrow(Input.mousePosition);
			}
		}
		
	}

	private void UpdateArrowJoyStick() {
		//TODO: here? and better way of controller it.
		float hor = Input.GetAxis("Horizontal_" + player.control);
		float ver = Input.GetAxis("Vertical_" + player.control);
		Vector3 dir = new Vector3(hor, ver, 0f);
		joyPos = Vector3.Min(joyPos + (dir * joySpeed), new Vector3(Screen.width, Screen.height));
		UpdateArrow(joyPos);
	}

	private void UpdateArrow(Vector3 screenPos) {
		Ray camRay = Camera.main.ScreenPointToRay(screenPos);
		RaycastHit floorHit;

		if (Physics.Raycast (camRay, out floorHit, _camRayLength, _forceMask)) {
			Vector3 playerToMouse = floorHit.point - transform.parent.position;
			playerToMouse.y = 0f;

			SetArrowOrientation(playerToMouse);
			SetArrowLength(floorHit.point, playerToMouse, playerToMouse.magnitude);
		}
	}

	private void SetArrowOrientation(Vector3 direction) {
		Quaternion newRotation = Quaternion.LookRotation (direction);
		transform.parent.rotation = newRotation;
	}

	private void SetArrowLength(Vector3 mousePos, Vector3 ptm, float magnitude) {

		// Retain the y, and offset the arrow by an amount
		Vector3 centerPos = new Vector3( 
			mousePos.x + transform.parent.position.x,
			0f,
			mousePos.z + transform.parent.position.z ) / 2f;
			
		centerPos = centerPos + (2f * Vector3.Normalize(ptm));
		centerPos.y = transform.position.y;
		transform.position = centerPos;
		transform.localScale = new Vector3((magnitude/10)*0.9f, transform.localScale.y, transform.localScale.z);
	}
}
