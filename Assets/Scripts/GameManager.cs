using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum GameState {
    golfGame, RPSGame, StopGame, MenuGame
}

public class GameManager : MonoBehaviour {
    public GameObject GameOverText;
    public RPSManager RockPaperScissor;
    public GameObject GolfballPrefab;
    public CameraFollow cameraController;
    public GolfballMananger[] golfballs;
    
    private GolfballMananger _current = null;
    private GameState _currentState = GameState.RPSGame;

    void Start () {
        SpawnAllPlayers();
        SetCameraTargets();
        StartCoroutine(GameLoop());
	}
	
    private IEnumerator GameLoop() {
        bool ballInHole = false;
        _currentState = GameState.RPSGame;
        DisableAll();
        yield return new WaitForSeconds(2);

        yield return StartCoroutine(RockPaperScissor.StartGame());
        _currentState = GameState.golfGame;
        
        // Get winner, and enable control of that player.
        if (ballInHole) {
            _currentState = GameState.StopGame;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else {
            StartCoroutine(GameLoop());
        }
        
    }
	
    private void DisableAll() {
        for(int i = 0; i<golfballs.Length; i++) {
            golfballs[i].DisableControl();
        }
    }

    private void EnableAll() {
         for(int i = 0; i<golfballs.Length; i++) {
            golfballs[i].enableControl();
        }
    }
    private void SpawnAllPlayers() {
        for (int i = 0; i<golfballs.Length; i++) {
            GolfballMananger player = golfballs[i];
            player.instance = Instantiate(
                GolfballPrefab, 
                player.spawnPoint.position, 
                player.spawnPoint.rotation
            ) as GameObject;
            player.playerNumber = i + 1;
            player.Setup();
        }
    }
    private void SetCameraTargets() {
        Transform[] targets = new Transform[golfballs.Length];
        
        for(int i = 0; i< targets.Length; i++) {
            targets[i] = golfballs[i].instance.transform; 
        }
        cameraController.targets = targets;
    }
    public void GameEnded()
    {
        var txt = GameOverText.GetComponent<Text>();
        txt.text = "Game Over";
        txt.enabled = true;
    }
}
