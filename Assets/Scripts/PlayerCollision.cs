using UnityEngine;

public class PlayerCollision : MonoBehaviour
{

    public GameObject roadSection;
    private Vector3 _spawnPosition = new Vector3(0, 0, 55f);
    
    
    
    private void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.CompareTag("Obstacle"))
        {
            if (!PlayerHealth.Instance.IsInvincible) {
                PlayerMovement.Instance.StopPlayerMovement();
                PlayerHealth.Instance.KillPlayer();
            }
        }
        else if (collisionInfo.collider.CompareTag("Wall")){
            if (!PlayerHealth.Instance.IsInvincible && CameraController.Instance != null && CameraController.Instance.CurrentMode != CameraMode.TopDown){
                PlayerMovement.Instance.StopPlayerMovement();
                PlayerHealth.Instance.KillPlayer();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MapTrigger"))
        {
            Instantiate(roadSection, _spawnPosition, Quaternion.identity);
        }
    }
}