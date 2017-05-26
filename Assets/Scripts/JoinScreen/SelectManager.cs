﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectManager : MonoBehaviour {

	public PlayerJoin[] players;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Submit")) {
			AddPlayers();
			if (JoinData.JoinedPlayers.Count > 0) {
				LoadGameScene();
			}
			
		}
	}

	private void AddPlayers() {
		List<PlayerData> joinedPlayers = new List<PlayerData>();
		for(int i = 0; i<players.Length; i++) {
			if(players[i].IsJoined()) {
				// TODO: constructor using string id for controls
				joinedPlayers.Add(
					new PlayerData(i+1, players[i].nick, players[i].IsJoystick )
				);
			}
		}
		JoinData.JoinedPlayers = joinedPlayers;
	}

	private void LoadGameScene() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}
