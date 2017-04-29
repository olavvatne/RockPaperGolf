using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CountDown : MonoBehaviour {

	private Text _text;

	// Use this for initialization
	void Start () {
		_text = GetComponent<Text>();
		_text.text = "";
	}
	
	public IEnumerator StartCountDown(int startCount) {
		_text.text = startCount + "";
		for(int i = startCount; i>0; i--) {
			_text.text = i + " ";
			yield return new WaitForSeconds(1);
		}
		_text.text = "";
	}

	public IEnumerator ShowInfo(string text, int duration) {
		yield return new WaitForSeconds(1);
		_text.fontSize = 60;
		_text.text = text;
		yield return new WaitForSeconds(duration);
		_text.text = "";
		_text.fontSize = 100;
	}
}
