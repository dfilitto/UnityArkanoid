using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum GameState {Stop, Play, Win, GameOver,Pause};
public class GameController : MonoBehaviour {
    public static GameController instance;
    //Coisas da tela
    [SerializeField]
    Text txtScore;
    [SerializeField]
    Text txtMsg;
    //Bola e player
    [SerializeField]
    private GameObject Ball;
    [SerializeField]
    private GameObject Player;
    public GameState gameState;
    private float score;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            if(instance !=this)
            {
                Destroy(this);
            }
        }
    }
    void Start () {
        gameState = GameState.Stop;
	}
	
	// Update is called once per frame
	void Update () {
        txtScore.text = "SCORE: " + score;
        if (Input.GetKeyUp(KeyCode.Space) && gameState == GameState.Stop)
        {
            StartGame();
        }

        if ((Input.GetKeyDown(KeyCode.Space) && gameState == GameState.Play)
            ||
            (Input.GetKeyDown(KeyCode.Space) && gameState == GameState.Pause))
        {
            PauseGame();
        }

        if (BlockController.instance.GetTLBlocks() <= 0 && gameState == GameState.Play)
        {
            LoadEndGame(GameState.Win);
        }
    }
    public void StartGame()
    {
        gameState = GameState.Play;
        score = 0;
        Ball.GetComponent<Ball>().StartBall();
        txtMsg.gameObject.SetActive(false);
    }

    public void LoadEndGame(GameState valor)
    {
        
        gameState = valor;
        txtMsg.gameObject.SetActive(true);
        if(gameState == GameState.GameOver)
        {
            txtMsg.text = "GAME OVER";
        }
        else
        {
            txtMsg.text = "WIN!!!!";
        }
        if(Ball != null)
        {
            Ball.SetActive(false);
        }
        Invoke("RestartGame", 5);
    }

    public void AddPoints(float valor)
    {
        this.score += valor;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PauseGame()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            gameState = GameState.Play;
            txtMsg.gameObject.SetActive(false);
        }
        else
        {
            Time.timeScale = 0;
            gameState = GameState.Pause;
            txtMsg.gameObject.SetActive(true);
            txtMsg.text = "PAUSE \n PRESS SPACE TO CONTINUE";
        }
    }
}
