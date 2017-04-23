using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerName : MonoBehaviour {

	public Transform target;

	private string _name = "Player";
	private Text _text;
	// Use this for initialization
	void Start () {
		_text = GetComponent<Text>();
		SetPlayerNamePostion();
	}
	
	public void SetName(string name) {
		_name = name;
	}
	// Update is called once per frame
	void Update () {
		SetPlayerNamePostion();
	}

	private void SetPlayerNamePostion() {
		Vector3 screenPos = Camera.main.WorldToScreenPoint(target.position);
		transform.position = screenPos;
	}
}
