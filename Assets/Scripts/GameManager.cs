using System.Collections;
using System.Linq;
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
    public float gameEndWait = 2.0f;
    private List<GolfballMananger> golfballs;
    public Transform[] spawnpoints;
    private GameState _currentState = GameState.RPSGame;
    private PlayerData[] players;
    void Start () {
        golfballs = new List<GolfballMananger>();
        if(JoinData.JoinedPlayers != null) {
            SpawnJoinedPlayers(JoinData.JoinedPlayers.ToArray());
        }
        else {
            SpawnDebugPlayers();
        }

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
        GolfballMananger winnerBall = golfballs.Find((x) => x.playerNumber == winner.id);
        cameraController.MoveTo(winnerBall.instance.transform);
        yield return StartCoroutine(winnerBall.StartPlaying());

        bool hasHit = winnerBall.playerData.hitAnotherBall;
        if ( !hasHit && golfballs.Count > 2) {
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
        for(int i = 0; i<golfballs.Count; i++) {
            golfballs[i].DisableControl();
        }
    }

    private void SpawnDebugPlayers() {
        //Init debug player datas
        string[] tempNames = {"Pete", "Jon"};
        Color[] tempColor = {Color.red, Color.yellow};
        List<PlayerData> debugPlayers = new List<PlayerData>();
        bool isJoystick = Input.GetJoystickNames().Length > 0 ? true : false;
        for (int i = 0; i<tempNames.Length; i++) {
            int nr = i + 1;
            PlayerData data = new PlayerData(nr, tempNames[i], isJoystick, tempColor[i]);
            debugPlayers.Add(data);
        }
        SpawnJoinedPlayers(debugPlayers.ToArray());
    }

     private void SpawnJoinedPlayers(PlayerData[] data) {
        players = data;
        for (int i = 0; i<players.Length; i++) {
            GolfballMananger ball = new GolfballMananger(players[i]);
            // TODO: random selection with no replacement.
            Transform spawnpoint = spawnpoints[(int)Random.Range(0f, (float)spawnpoints.Length)];
            ball.instance = Instantiate(
                GolfballPrefab, 
                spawnpoint.position, 
                spawnpoint.rotation
            ) as GameObject;
            golfballs.Add(ball);
            ball.Setup();
        }
        RockPaperScissor.SetPlayers(players);
    }
    private IEnumerator Restart() {
         _currentState = GameState.StopGame;
        yield return new WaitForSeconds(gameEndWait);
        GameManager.gameEnded = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
