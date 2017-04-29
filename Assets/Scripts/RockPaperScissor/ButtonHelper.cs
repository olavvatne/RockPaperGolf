using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHelper : MonoBehaviour {

	public string controller = "Xbox 360";
	public string controlButton = "X";
	public string buttonText = "Button";
	private Text _label;
	private RawImage _icon;
	// Use this for initialization
	void Start () {
		_label = GetComponentInChildren<Text>();
		_icon = GetComponentInChildren<RawImage>(); 
		_label.text = buttonText;
		string path = "ControllerIcons/" + controller + "/360_" + controlButton;
		_icon.texture = Resources.Load<Texture2D>(path);
	}
	
}
