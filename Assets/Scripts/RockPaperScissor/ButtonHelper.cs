using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHelper : MonoBehaviour {

	public string controller = "360";
	public string controlButton = "X";
	public string keyboardButton = "1";
	public string buttonText = "Button";
	private Text _label;
	private RawImage _icon;
	// Use this for initialization
	void Start () {
		// TODO: need to dynamically set helper
		bool isJoy = CheckIfJoystick();
		string btnName = controlButton;
		string iconPrefix = controller;
		if (!isJoy) {
			controller = "KeyboardMouse/Dark";
			iconPrefix = "Keyboard_Black";
			btnName = keyboardButton;
		}

		_label = GetComponentInChildren<Text>();
		_icon = GetComponentInChildren<RawImage>(); 

		_label.text = buttonText;
		string path = "ControllerIcons/" + controller + "/"+ iconPrefix + "_" + btnName;
		Debug.Log(path);
		_icon.texture = Resources.Load<Texture2D>(path);
	}
	
		// TODO: fix this at some point
	bool CheckIfJoystick() {
		return Input.GetJoystickNames().Length > 0 ? true : false;
	}
}
