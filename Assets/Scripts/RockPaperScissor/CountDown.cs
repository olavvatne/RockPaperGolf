using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CountDown : MonoBehaviour {

	public AudioClip tick;
	public AudioClip timeout;
	private Text _text;
	private AudioSource _source;
	

	// Use this for initialization
	void Start () {
		_text = GetComponent<Text>();
		_text.text = "";

		_source = GetComponent<AudioSource>();
	}
	
	public IEnumerator StartCountDown(int startCount) {
		_text.text = startCount + "";
		for(int i = startCount; i>0; i--) {
			_text.text = i + " ";
			_source.PlayOneShot(tick);
			yield return new WaitForSeconds(1);
		}
		_source.PlayOneShot(timeout);
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
