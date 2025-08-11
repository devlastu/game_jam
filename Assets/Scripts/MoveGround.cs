using UnityEngine;

public class MoveGround : MonoBehaviour
{
    private CameraController _cameraController;
    private float _baseSpeed = 15f;

    void FixedUpdate() {
        if (GameManager.Instance == null || GameManager.Instance.State != GameState.Playing) return;

        if (_cameraController == null) {
            _cameraController = CameraController.Instance;
            if (_cameraController == null) return;
        }

        float speedMultiplier = _cameraController.IsSwitching ? 0.2f : 1f;

        
        Vector3 movement = Vector3.back * _baseSpeed * speedMultiplier * Time.deltaTime;
        transform.position = transform.position + movement;
    } 


    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("DestroyWall")) {
            Destroy(gameObject);
        }
    }
}