using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowDirection : MonoBehaviour {
	public float joySpeed = 100f;
	[HideInInspector] public PlayerData data;
	private int _forceMask;
	private float _camRayLength = 100f;

	private Vector3 joyPos = new Vector3(0f, 0f, 1f);
	// Use this for initialization
	void Start () {
		_forceMask = LayerMask.GetMask("Force");
	}

	// Update is called once per frame
	void Update () {
		if (data != null) {
			if (data.isJoystick) {
				UpdateArrowJoyStick();
			}
			else {
				UpdateArrow(Input.mousePosition);
			}
		}
		
		
	}

	private void UpdateArrowJoyStick() {
		//TODO: find player control from manager
		string playerControl = data.control;
		float hor = Input.GetAxis("Horizontal" + playerControl);
		float ver = Input.GetAxis("Vertical" + playerControl);
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
