using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Assertions;
public enum GameState {
    golfGame, RPSGame, StopGame, MenuGame
}

public class GameManager : MonoBehaviour {

    public static bool gameEnded = false;

    public GameObject GameOverText;
    public RPSManager RockPaperScissor;
    public GameObject GolfballPrefab;
    public CameraFollow cameraController;

    public float gameStartWait = 2.0f;
    public float gameEndWait = 4.0f;
    public GolfballMananger[] golfballs;
    
    private GameState _currentState = GameState.RPSGame;
    private PlayerData[] players;
    void Start () {
        if(JoinData.JoinedPlayers != null) {
            SpawnJoinedPlayers();
        }
        else {
            SpawnAllPlayers();
        }

        SetCameraTargets();
        StartCoroutine(GameLoop());
	}
	
    private IEnumerator GameLoop() {
        DisableAll();

        //Rock paper scissor game execution
        _currentState = GameState.RPSGame;
        yield return new WaitForSeconds(gameStartWait);
        yield return StartCoroutine(RockPaperScissor.StartGame());
        PlayerData winner = RockPaperScissor.GetWinner();
        RockPaperScissor.StopGame();

        //Golf game execution
        _currentState = GameState.golfGame;
        Assert.IsNotNull(winner, "No winner in RPS");
        GolfballMananger winnerBall = golfballs[winner.id - 1];
        cameraController.MoveTo(winnerBall.instance.transform);
        yield return StartCoroutine(winnerBall.StartPlaying());

        bool hasHit = winnerBall.playerData.hitAnotherBall;
        if ( !hasHit && golfballs.Length > 2) {
            winnerBall.playerData.eligibleToPlay = false;
        } 

        if (GameManager.gameEnded) {
           StartCoroutine(Restart());
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

    private bool CheckIfJoystick() {
		return Input.GetJoystickNames().Length > 0 ? true : false;
	}
    private void SpawnAllPlayers() {
        //TODO: only for debug or remove.
        string[] tempNames = {"Pete", "Jon", "Thomas", "Danny"};
        players = new PlayerData[golfballs.Length];
        bool isJoystick = CheckIfJoystick(); // TODO: right way?

        for (int i = 0; i<golfballs.Length; i++) {
            GolfballMananger player = golfballs[i];
            player.instance = Instantiate(
                GolfballPrefab, 
                player.spawnPoint.position, 
                player.spawnPoint.rotation
            ) as GameObject;

            int nr = i + 1;
            PlayerData data = new PlayerData(nr, tempNames[i], isJoystick);
            player.playerNumber = data.id;
            player.playerData = data;
            player.Setup();
            players[i] = data;
        }

        RockPaperScissor.SetPlayers(players);
    }

     private void SpawnJoinedPlayers() {
        players = JoinData.JoinedPlayers.ToArray();

        for (int i = 0; i<players.Length; i++) {
            GolfballMananger player = golfballs[i];
            player.instance = Instantiate(
                GolfballPrefab, 
                player.spawnPoint.position, 
                player.spawnPoint.rotation
            ) as GameObject;

            player.playerNumber = players[i].id;
            player.playerData = players[i];
            player.Setup();
            players[i] = players[i];
        }

        RockPaperScissor.SetPlayers(players);
    }
    private IEnumerator Restart() {
         _currentState = GameState.StopGame;
        yield return new WaitForSeconds(gameEndWait);
        GameManager.gameEnded = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void SetCameraTargets() {
        Transform[] targets = new Transform[players.Length];
        
        for(int i = 0; i< targets.Length; i++) {
            targets[i] = golfballs[i].instance.transform; 
        }
        cameraController.SetTargets(targets);
    }
    public void GameEnded()
    {
        GameManager.gameEnded = true;
        var txt = GameOverText.GetComponent<Text>();
        txt.text = "Somebody won and somebody lost";
        txt.enabled = true;
        StartCoroutine(Restart());
    }
}
