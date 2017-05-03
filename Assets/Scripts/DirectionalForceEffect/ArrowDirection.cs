using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowDirection : MonoBehaviour {

	[HideInInspector] public PlayerData data;
	private int _forceMask;
	private float _camRayLength = 100f;

	private Vector3 joyPos;

	public void SetJoyPosition(Vector3 newPos) {
		this.joyPos = newPos;
	}
	// Use this for initialization
	void Start () {
		joyPos = Vector3.zero;
		_forceMask = LayerMask.GetMask("Force");
	}

	// Update is called once per frame
	void Update () {
		if (data != null) {
			if (data.isJoystick) {
				UpdateJoyArrow(joyPos);
			}
			else {
				UpdateArrow(Input.mousePosition);
			}
		}	
	}

	private void UpdateJoyArrow(Vector3 v) {
		Vector3 nv = new Vector3(v.x, 0f, v.y);
		SetArrowOrientation(nv);
		SetArrowLength(transform.parent.position + nv, nv, nv.magnitude);
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
