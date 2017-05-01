using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerName : MonoBehaviour {

	public Transform target;

	//public PlayerData _data;
	private Text _text;
	// Use this for initialization
	void Start () {
		_text = GetComponent<Text>();
		//_text.text = _data.playerName;
		_text.text ="temp";
		SetPlayerNamePostion();
	}

	// Update is called once per frame
	void Update () {
		SetPlayerNamePostion();
	}

	private void SetPlayerNamePostion() {
		if ( target ) {
			Vector3 screenPos = Camera.main.WorldToScreenPoint(target.position);
			transform.position = screenPos;
		}
		
	}
}
