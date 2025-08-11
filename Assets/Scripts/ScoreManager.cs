using UnityEngine;
using TMPro; 

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public TextMeshProUGUI scoreText;
    private float _scorePerSecond = 10f;
    private float _score;
    private float _maxScore;
        
    void Awake(){
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    void Start() {
        _score = 0;
        if(scoreText != null)
            scoreText.text = Mathf.FloorToInt(_score).ToString();
    }
    
    void Update()
    {
        if (GameManager.Instance == null) return; 
        if (GameManager.Instance.IsGameOver()) {
            SetHighScore(_score);
            return;
        }
        _score += _scorePerSecond * Time.deltaTime;
        scoreText.text = Mathf.FloorToInt(_score).ToString();
    }

    public void AddPoints(float points){
        _score += points;
    }

    public int GetScore() => Mathf.FloorToInt(_score);

    public void ResetScore(){
        _score = 0;
    }

    private void SetHighScore(float score) {
        if (score > _maxScore) _maxScore = score;
    }

    private float GetHighScore() => _maxScore;

    public void SetScorePerSecond(float seconds) {
        _scorePerSecond = seconds;        
    }
}