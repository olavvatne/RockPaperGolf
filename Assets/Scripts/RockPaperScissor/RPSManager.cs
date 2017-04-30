using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RPSManager : MonoBehaviour {

	public int secondsCount = 3;
	public float startDelay = 2f;
	public HandController[] players;
	public CountDown CountDown;
	
	public void Start() {
		this.gameObject.SetActive(false);
	}
	public void SetPlayers() {
	}

	public IEnumerator StartGame() {

		this.gameObject.SetActive(true);
		ShowAll();
		yield return StartCoroutine( RockPaperScissor() );

	}

	IEnumerator RockPaperScissor() {
		bool winner = false;
		HandController[] remainingPlayers = (HandController[])players.Clone();
		List<HandController> winnerHand;

		yield return new WaitForSeconds(startDelay);
		while (!winner) {
			yield return StartCoroutine(CountDown.StartCountDown(secondsCount));

			//Either it's a draw, some player lost and are pruned or 1 player won the game.
			winnerHand = Getwinners(remainingPlayers);
			if (winnerHand == null) {
				yield return StartCoroutine(CountDown.ShowInfo("Draw", 2));
			}
			else if (winnerHand.Count > 1) {
				remainingPlayers = winnerHand.ToArray();
				string pruneText = "Only " + remainingPlayers.Length + " left!";
				yield return StartCoroutine(CountDown.ShowInfo(pruneText, 2));
				hideLosers(remainingPlayers);
			}
			else {
				winner = true;
				remainingPlayers = winnerHand.ToArray();
				hideLosers(remainingPlayers);
				string winnerText = winnerHand[0].GetPlayer().playerName + " wins!";
				yield return StartCoroutine(CountDown.ShowInfo(winnerText, 2));
			}

			yield return new WaitForSeconds(1);
		}
	}

	private void ShowAll() {
		for ( var i = 0; i< players.Length; i++) {
			players[i].Show();
		}
	}
	private void hideLosers(HandController[] remainingPlayers) {
		for ( var i = 0; i< players.Length; i++) {
			if (!remainingPlayers.Contains(players[i])) {
				players[i].Hide();
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
		for (int i = 0; i<playerStates.Length; i++) {
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
		for (int i = 1; i<playerStates.Length; i++) {
			if (playerStates[i-1] != playerStates[i]) {
				return false;
			}
		}
		return true;
	}

	private List<HandController> Getwinners(HandController[] remaining) {
		HandState[] playerStates = new HandState[remaining.Length];
		for (int i = 0; i< remaining.Length; i ++) {
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
		for (int i = 0; i < players.Length; i ++) {
			bool playerIsWinner = true;
			for (int j = 0; j<players.Length; j++) {
				if (i != j) {
					if (HasLost(playerStates[i], playerStates[j])) {
						playerIsWinner = false;
					}
				}
			}
			if (playerIsWinner) {
				winnerList.Add(players[i]);
			}
		}

		return winnerList;
	}
}
