using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData {

	public string control = "";
	public int id = 1;

	public string name = "Placeholder";

	public int maxHits = 2;
	public int hits = 0;
	public bool hitAnotherBall = false;
	public bool ballStopped = false;
	public bool isJoystick = false;
	public bool eligibleToPlay = true;

	public PlayerData(int id, string name, bool isJoystick) {
		this.control = "_P" + id;
		this.id = id;
		this.name = name;
		this.isJoystick = isJoystick;
	}
	
	public void ResetGolfData() {
		hits = 0;
		hitAnotherBall = false;
		ballStopped = false;
		eligibleToPlay = true;
	}

}
