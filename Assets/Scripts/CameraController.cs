using UnityEngine;

[System.Serializable]
public struct CameraSettings {
    public CameraMode mode;
    public Vector3 offset;
}

public class CameraController : MonoBehaviour {

    public static CameraController Instance { get; private set; }

    public Transform player;
    public CameraSettings[] cameraModes;

    private int _currentModeIndex = 0;
    private Vector3 _currentOffset;
    private Vector3 _targetPosition;
    private bool _isSwitching = false;
    public float smoothSpeed = 2f;
    
    private CameraMode _currentMode;
    public CameraMode CurrentMode {
        get { return _currentMode; }
        set { _currentMode = value; }
    }

    public bool IsSwitching => _isSwitching;

    void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
    }

    void Start() {
        if (!GameManager.Instance) {
            // Debug.LogError("GameManager not found");
        }
        if (cameraModes == null || cameraModes.Length == 0) {
            cameraModes = new CameraSettings[] {
                new CameraSettings { mode = CameraMode.FollowBehind, offset = new Vector3(0, 2, -5) },
                new CameraSettings { mode = CameraMode.TopDown, offset = new Vector3(0, 20, -2) },
                new CameraSettings { mode = CameraMode.SideView, offset = new Vector3(20, 0, 0) }
            };
        }

        _currentMode = cameraModes[_currentModeIndex].mode;
        _currentOffset = cameraModes[_currentModeIndex].offset;
        _targetPosition = player.position + _currentOffset;
        transform.position = _targetPosition;
        transform.LookAt(player);
    }

    void Update() {
        if (GameManager.Instance == null || GameManager.Instance.State != GameState.Playing) 
            return;

        if (cameraModes == null) return; 

        for (int i = 0; i < cameraModes.Length; i++) {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i)) {
                SwitchToModeByIndex(i);
                break;
            }
        }

        if (player == null) return;

        if (_isSwitching) {
            transform.position = Vector3.Lerp(transform.position, _targetPosition, smoothSpeed * Time.deltaTime);
            transform.LookAt(player);

            if (Vector3.Distance(transform.position, _targetPosition) < 0.01f) {
                transform.position = _targetPosition;
                _isSwitching = false;
            }
        } else {
            transform.position = player.position + _currentOffset;
            transform.LookAt(player);
        }
    }


    private void SwitchToModeByIndex(int index) {
        if (index < 0 || index >= cameraModes.Length) {
            
            return;
        }
        _currentModeIndex = index;
        CurrentMode = cameraModes[_currentModeIndex].mode;
        _currentOffset = cameraModes[_currentModeIndex].offset;
        _targetPosition = player.position + _currentOffset;
        _isSwitching = true;
    }

    public void SetCameraModeWithTransition(CameraMode mode)
    {
        int index = System.Array.FindIndex(cameraModes, cs => cs.mode == mode);

        if (index < 0) {
            // Debug.LogWarning($"Camera mode {mode} nije pronađen!");
            return;
        }

        _currentModeIndex = index;
        CurrentMode = cameraModes[_currentModeIndex].mode;
        _currentOffset = cameraModes[_currentModeIndex].offset;
        _targetPosition = player.position + _currentOffset;
        _isSwitching = true;
    }
}
