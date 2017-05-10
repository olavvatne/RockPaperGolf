using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerJoin : MonoBehaviour {

	public GameObject ball;
	public GameObject actionHelper;
	public Text joinedText;
	public GameObject noPlayer;
	public RawImage frame;
	public Color playerColor;

	public string player = "P1";
	public string nick = "Joe";

	private bool playerJoined = false;
	// Use this for initialization
	void Start () {
		MeshRenderer renderer = ball.GetComponent<MeshRenderer>();
		renderer.material.color = playerColor;
		frame.color = playerColor;
		ball.SetActive(playerJoined);
		noPlayer.SetActive(!playerJoined);
		joinedText.gameObject.SetActive(playerJoined);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Fire2_" + player)) {
			TogglePlayer();
		}
	}

	void TogglePlayer() {
		playerJoined = !playerJoined;
		ball.SetActive(playerJoined);
		actionHelper.SetActive(!playerJoined);
		joinedText.gameObject.SetActive(playerJoined);
		joinedText.text = nick + " joined!";
		noPlayer.SetActive(!playerJoined);
	}
}
