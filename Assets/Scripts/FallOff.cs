using UnityEngine;

public class FallOff : MonoBehaviour
{
    [Header("Thresholds")]
    public float fallThresholdY = -0.05f;
    public float leftBoundaryX = -8f;      
    public float rightBoundaryX = 8f;      

    private bool _hasTriggered = false;

    private void Update()
    {
        if (_hasTriggered) return;

        if (HasFallenBelowThreshold() || HasLeftTrackBounds())
        {
            HandlePlayerOutOfBounds();
        }
    }
    
    private bool HasFallenBelowThreshold()
    {
        return transform.position.y < fallThresholdY;
    }

    
    private bool HasLeftTrackBounds()
    {
        return transform.position.x < leftBoundaryX || transform.position.x > rightBoundaryX;
    }
    
    private void HandlePlayerOutOfBounds()
    {
        _hasTriggered = true;

        if (PlayerMovement.Instance != null)
        {
            PlayerMovement.Instance.StopPlayerMovement();
        }

        // Debug.Log($"[FallOff] Igrač je napustio stazu ili pao! Pos: {transform.position}");
        PlayerHealth.Instance.KillPlayer();
    }
}

