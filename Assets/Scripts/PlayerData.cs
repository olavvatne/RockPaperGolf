using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData {

	public string control = "P1";
	public int id = 1;

	public string name = "Placeholder";

	public int maxHits = 2;
	public int hits = 0;
	public bool hitAnotherBall = false;
	public bool ballStopped = false;

	public PlayerData(string control, int id, string name) {
		this.control = control;
		this.id = id;
		this.name = name;
	}
	
	public void ResetGolfData() {
		hits = 0;
		hitAnotherBall = false;
		ballStopped = false;
	}

}
