using UnityEngine;
using UnityEngine.UI;

public class StartMenuManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject startPanel;
    public Button startButton;
    
    
    private void Start() {
        if (GameManager.Instance != null && GameManager.Instance.State == GameState.StartMenu) {
            if (startPanel != null)
                startPanel.SetActive(true);
        
            GameManager.Instance.SetState(GameState.StartMenu);
            startButton.onClick.AddListener(OnStartButtonClicked);
        }
    }

    
    public void OnStartButtonClicked() {
        if (startPanel != null)
            startPanel.SetActive(false);
        
        if (GameManager.Instance != null)
            GameManager.Instance.StartGame();
    }
}