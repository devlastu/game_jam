using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameState State { get; private set; }

    public float gameOverDelay = 0f;

    private int _score = 0;
    private string _message = "";
    private int _highScore = 0;

    private const string HighScoreKey = "HighScore";

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        State = GameState.StartMenu;

        _highScore = PlayerPrefs.GetInt(HighScoreKey, 0);
    }

    private void GameOver()
    {
        if (ScoreManager.Instance != null)
        {
            _score = ScoreManager.Instance.GetScore();
        }

        if (SetHighScore(_score))
        {
            _message = "Congratulations! New record!";
        }
        else
        {
            _message = "";
        }

        if (GameOverUIManager.Instance != null)
        {
            GameOverUIManager.Instance.ShowGameOverUI(score: _score, message: _message);
        }
    }

    public int GetHighScore(){
        return _highScore;
    }

    public bool SetHighScore(int value)
    {
        if (value > _highScore)
        {
            _highScore = value;
            PlayerPrefs.SetInt(HighScoreKey, _highScore);
            PlayerPrefs.Save();
            return true;
        }
        return false;
    }

    public void SetState(GameState newState)
    {
        if (State == newState) return;

        State = newState;

        if (newState == GameState.GameOver)
        {
            GameOver();
        }
    }

    public bool IsGameOver()
    {
        return State == GameState.GameOver;
    }

    public void Restart()
    {
        State = GameState.Playing;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    
    public void StartGame() {
        Debug.Log("Starting Game");
        State = GameState.Playing;
    }
}
