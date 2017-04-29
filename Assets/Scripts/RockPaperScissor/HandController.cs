using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

 public enum HandPosition{
    left = -90, right = 90, top = -180, bottom = 0
 }

 public enum HandState {
    rock = 0, paper =1 , scissor = 2
 }
public class HandController : MonoBehaviour {

	public PlayerData _data;
	private GameObject _hand;
	private RawImage _handImage;
	public Texture rock;
	public Texture scissor;
	public Texture paper;
	public HandPosition current = HandPosition.left;
	private Text _playerName;

	private HandState _currentHandState = HandState.rock;

	public HandState GetHandState() {
		return this._currentHandState;
	}

	public PlayerData GetPlayer() {
		return _data;
	}

	public void Show() {
		this.gameObject.SetActive(true);
	}

	public void Hide() {
		this.gameObject.SetActive(false);
	}
	// Use this for initialization
	void Start () {
		_playerName = GetComponentInChildren<Text>();
		_playerName.text = _data.playerName;

		_hand = transform.Find("Hand").gameObject;
		_handImage = _hand.GetComponent<RawImage>();
		SetHandOrientation();
		SetHandStartState();
	}
	
	private void SetHandOrientation() {
		_hand.GetComponent<RectTransform>().rotation =  Quaternion.Euler(0,0, (float)current);
	}
	
	private void SetHandStartState() {
		_handImage.texture = rock;
		_currentHandState = HandState.rock;
	}
	// Update is called once per frame
	void Update () {
		string player = "_" + _data.control;
		if(Input.GetButton("Fire1" + player)) {
			// A button and paper
			_handImage.texture = paper;
			_currentHandState = HandState.paper;
		}
		else if(Input.GetButton("Fire2" + player)) {
			// B button and scissor
			_handImage.texture = scissor;
			_currentHandState = HandState.scissor;
		}
		else if(Input.GetButton("Fire3" + player)) {
			// X button and Rock
			_handImage.texture = rock;
			_currentHandState = HandState.rock;
		}
	}
}
