using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public GameObject GameOverText;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void GameEnded()
    {
        var txt = GameOverText.GetComponent<Text>();
        txt.text = "Game Over";
        txt.enabled = true;
    }
}
