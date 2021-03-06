﻿using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectManager : MonoBehaviour {

	public PlayerJoin[] players;
	public GameObject startButton;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//if ()
		if (Input.GetButtonDown("Submit")) {
			AddPlayers();
			if (JoinData.JoinedPlayers.Count > 1) {
				LoadGameScene();
			}
			
		}
	}
	private bool hasEnoughPlayersJoined() {
		int joined = 0;
		for (int i = 0; i<players.Count(); i++) {
			if(players[i].IsJoined()) {
				joined += 1;
			}
		}
		if (joined > 1) {
			return true;
		}
		return false;

	}

	private void AddPlayers() {
		List<PlayerData> joinedPlayers = new List<PlayerData>();
		for(int i = 0; i<players.Length; i++) {
			if(players[i].IsJoined()) {
				// TODO: constructor using string id for controls
				Color greyedColor = Color.Lerp(players[i].playerColor, Color.black, 0.4f); 
				joinedPlayers.Add(
					new PlayerData(i+1, players[i].nick, players[i].IsJoystick, greyedColor )
				);
			}
		}
		JoinData.JoinedPlayers = joinedPlayers;
	}

	private void LoadGameScene() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}
