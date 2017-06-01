using System;
using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;

[Serializable]
public class GolfballMananger {
	public Color playerColor = Color.red;
	
	[HideInInspector] public int playerNumber;
	[HideInInspector] public GameObject instance;

	[HideInInspector] public PlayerData playerData;
	private GolfBallMovement _movement;
	private ArrowDirection _forceArrow;

	public GolfballMananger(PlayerData data) {
		this.playerNumber = data.id;
		this.playerColor = data.color;
		this.playerData = data;
	}
	public void Setup() {
		Assert.IsNotNull(instance);
		Assert.IsNotNull(playerData);

		_movement = instance.GetComponent<GolfBallMovement>();
		_movement.data = playerData; 
		_forceArrow = _movement.arrowGraphics;
		_forceArrow.data = playerData;
		MeshRenderer renderer = instance.GetComponent<MeshRenderer>();
		renderer.material.color = playerColor;

		Assert.IsNotNull(_movement);
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
