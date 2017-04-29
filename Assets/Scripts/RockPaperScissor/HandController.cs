using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

 public enum HandPosition{
    left = -90, right = 90, top = -180, bottom = 0
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
	private Texture[] _handTexs;
	private int idx = 0;

	// Use this for initialization
	void Start () {
		_playerName = GetComponentInChildren<Text>();
		_playerName.text = _data.playerName;

		_hand = transform.Find("Hand").gameObject;
		_handImage = _hand.GetComponent<RawImage>();
		SetHandOrientation();
		
		 _handTexs = new Texture[3] { rock, scissor, paper };
	}
	
	private void SetHandOrientation() {
		_hand.GetComponent<RectTransform>().rotation =  Quaternion.Euler(0,0, (float)current);
	}
	// Update is called once per frame
	void Update () {
		if(Input.GetButton("Fire1")) {
			_handImage.texture = _handTexs[idx];
			idx = (idx + 1) % _handTexs.Length;
		}
	}
}
