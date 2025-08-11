using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverUIManager : MonoBehaviour{
    
    public static GameOverUIManager Instance { get; private set; }
    
    [Header("UI Elements")]
    public GameObject gameOverPanel;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI messageText;
    public Button restartButton;
    
    void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        gameOverPanel.SetActive(false);
    
        restartButton.onClick.AddListener(RestartGame);
    }

    public void ShowGameOverUI(int score, string message = "") {
        scoreText.text = "Score: " + score;
        messageText.text = message;
        gameOverPanel.SetActive(true);
    }

    private void RestartGame(){
        GameManager.Instance.Restart();
    }
}
