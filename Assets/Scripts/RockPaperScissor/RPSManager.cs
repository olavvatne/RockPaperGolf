﻿using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
public class RPSManager : MonoBehaviour {

	public int secondsCount = 3;
	public float startDelay = 2f;
	private HandController[] _players;
	public CountDown CountDown;
	private PlayerData _winner;

	public void Start() {
		this.gameObject.SetActive(false);
	}
	
	public void SetPlayers(PlayerData[] players) {
		HandController[] hands = GetComponentsInChildren<HandController>();
		HandController[] activePlayers = new HandController[players.Length];
		Assert.IsTrue(hands.Length >= players.Length);
		
		//Hide all hands first.
		for(int j = 0; j<hands.Length; j++) {
			hands[j].Hide();
			hands[j].gameObject.SetActive(false);
		}

		int i = 0;
		for (i = 0; i<players.Length; i++) {
			hands[i].gameObject.SetActive(true);
			hands[i].SetPlayer(players[i]);
			activePlayers[i] = hands[i];
		}
		_players = activePlayers;
	}

	public IEnumerator StartGame() {

		this.gameObject.SetActive(true);
		HandController[] eligble = GetEligible();
		Show(eligble);
		yield return StartCoroutine( RockPaperScissor(eligble) );

	}

	public HandController[] GetEligible() {
		List<HandController> remaining = new List<HandController>();
		for (int i = 0; i < _players.Length; i++) {
			if (_players[i].GetPlayer().eligibleToPlay) {
							remaining.Add(_players[i]);
			}
		}
		Assert.IsTrue(remaining.Count() > 1);
		return remaining.ToArray();
	}

	public PlayerData GetWinner() {
		return _winner;
	}
	IEnumerator RockPaperScissor(HandController[] startPlayers) {
		_winner = null;
		bool isWinner = false;
		HandController[] remainingPlayers = (HandController[])startPlayers.Clone();
		KeepWinners(remainingPlayers); //Hide uneligible ones.
		List<HandController> winnerHand;

		yield return new WaitForSeconds(startDelay);
		while (!isWinner) {
			yield return StartCoroutine(CountDown.StartCountDown(secondsCount));

			//Either it's a draw, some player lost and are pruned or 1 player won the game.
			SetChangeable(remainingPlayers, false);
			winnerHand = Getwinners(remainingPlayers);
			if (winnerHand == null) {
				yield return StartCoroutine(CountDown.ShowInfo("Draw", 2));
			}
			else if (winnerHand.Count > 1) {
				remainingPlayers = winnerHand.ToArray();
				string pruneText = "Only " + remainingPlayers.Length + " left!";
				yield return StartCoroutine(CountDown.ShowInfo(pruneText, 2));
				KeepWinners(remainingPlayers);
			}
			else {
				isWinner = true;
				remainingPlayers = winnerHand.ToArray();
				_winner = winnerHand[0].GetPlayer();
				KeepWinners(remainingPlayers);
				string winnerText = winnerHand[0].GetPlayer().name + " wins!";
				yield return StartCoroutine(CountDown.ShowInfo(winnerText, 2));
			}
			SetChangeable(remainingPlayers, true);
			yield return new WaitForSeconds(1);
		}
	}
	public void StopGame() {
		this.gameObject.SetActive(false);
	}
	
	private void SetChangeable(HandController[] players, bool isUserAllowedToChange) {
		for ( var i = 0; i< players.Length; i++) {
			players[i].isUserInput = isUserAllowedToChange;
		}
	}
	private void Show(HandController[] eligble) {
		for ( var i = 0; i< eligble.Length; i++) {
			eligble[i].Show();
		}
	}
	private void KeepWinners(HandController[] remainingPlayers) {
		for ( var i = 0; i< _players.Length; i++) {
			if (!remainingPlayers.Contains(_players[i])) {
				_players[i].Hide();
			}
		}
	}

	private bool HasLost(HandState hand, HandState other) {
		if (((int)hand + 1) % 3 == (int)other) {
			return true;
		}
		return false;
	}

	private bool AllStatesPresent(HandState[] playerStates) {
		bool[] isPresent = new bool[3];
		for (int i = 0; i < playerStates.Length; i++) {
			isPresent[(int)playerStates[i]] = true;
		}
		for (int i = 0; i<isPresent.Length; i++) {
			if (!isPresent[i]) {
				return false;
			}
		}
		return true;
	}

	private bool OneStateOnly(HandState[] playerStates) {
		for (int i = 1; i < playerStates.Length; i++) {
			if (playerStates[i-1] != playerStates[i]) {
				return false;
			}
		}
		return true;
	}

	private List<HandController> Getwinners(HandController[] remaining) {
		HandState[] playerStates = new HandState[remaining.Length];
		for (int i = 0; i < remaining.Length; i ++) {
			HandState s = remaining[i].GetHandState();
			playerStates[i] = s;
		}

		//Draw since nobody has been beat
		if (AllStatesPresent(playerStates) || OneStateOnly(playerStates)) {
			return null;
		}

		//Know here that there are only two states present
		List<HandController> winnerList = new List<HandController>();

		//Determine winners
		for (int i = 0; i < remaining.Length; i ++) {
			bool playerIsWinner = true;
			for (int j = 0; j < remaining.Length; j++) {
				if (i != j) {
					if (HasLost(playerStates[i], playerStates[j])) {
						playerIsWinner = false;
					}
				}
			}
			if (playerIsWinner) {
				winnerList.Add(remaining[i]);
			}
		}

		return winnerList;
	}
}
