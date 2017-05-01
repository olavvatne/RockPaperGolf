using System;
using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;

[Serializable]
public class GolfballMananger {
	public Color playerColor;
	public Transform spawnPoint;
	
	[HideInInspector] public int playerNumber;
	[HideInInspector] public GameObject instance;

	[HideInInspector] public PlayerData playerData;
	private GolfBallMovement _movement;
	private ArrowDirection _forceArrow;

	public void Setup() {
		Assert.IsNotNull(playerData);

		_movement = instance.GetComponent<GolfBallMovement>();
		_movement.data = playerData; 
		_forceArrow = instance.GetComponentInChildren<ArrowDirection>();
		_forceArrow.data = playerData;
		MeshRenderer renderer = instance.GetComponent<MeshRenderer>();
		renderer.material.color = playerColor;
		DisableControl();
	}

	public void DisableControl() {
		_movement.enabled = false;
		_forceArrow.enabled = false;
		_forceArrow.gameObject.SetActive(false);
	}

	public void enableControl() {
		_movement.enabled = true;
		_forceArrow.enabled = true;
		_forceArrow.gameObject.SetActive(true);
	}

	public IEnumerator StartPlaying() {
		//Player can play until any of the conditions below have been met.
		//The function will not return until the ball has stopped (magnitude below a threshold)
		enableControl();
		playerData.ResetGolfData();
		yield return new WaitUntil(() => playerData.hits >= 2 || GameManager.gameEnded == true );
		yield return new WaitUntil(() => playerData.ballStopped );
	}
}
