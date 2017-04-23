using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowDirection : MonoBehaviour {
	private int _forceMask;
	private float _camRayLength = 100f;
	// Use this for initialization
	void Start () {
		_forceMask = LayerMask.GetMask("Force");
	}
	
	// Update is called once per frame
	void Update () {
		updateArrow();
	}

	private void updateArrow() {
		Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
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
