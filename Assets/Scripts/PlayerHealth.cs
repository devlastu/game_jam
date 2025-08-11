using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance { get; private set; }

    private bool _invincible = false;
    public bool IsInvincible => _invincible;

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public void SetInvincible(bool state)
    {
        _invincible = state;
    }

    public void KillPlayer(){
        GameManager.Instance.SetState(GameState.GameOver);
    }
}
