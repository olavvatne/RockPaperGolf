using System;
using UnityEngine;

[Serializable]
public class GolfballMananger {
	public Color playerColor;
	public Transform spawnPoint;
	// Use this for initialization
	[HideInInspector] public int playerNumber;
	[HideInInspector] public GameObject instance;

	private GolfBallMovement _movement;
	private ArrowDirection _forceArrow;
	public void Setup() {
		_movement = instance.GetComponent<GolfBallMovement>();
		_forceArrow = instance.GetComponentInChildren<ArrowDirection>();
		MeshRenderer renderer = instance.GetComponent<MeshRenderer>();
		renderer.material.color = playerColor;
		DisableControl();
	}

	public void DisableControl() {
		_movement.enabled = false;
		_forceArrow.enabled = false;
	}

	public void enableControl() {
		_movement.enabled = true;
		_forceArrow.enabled = true;
	}
}
