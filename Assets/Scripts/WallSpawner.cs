using UnityEngine;

public class WallSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject wallObstaclePrefab;
    public GameObject holeObstaclePrefab;

    [Header("Spawn Points")]
    public Transform wallSpawnPoint;
    public Transform holeSpawnPoint;

    [Header("Settings")]
    [Range(0f, 1f)] public float wallSpawnChance = 0.5f;

    void Start()
    {
        InitializeSpawnPoints();
        ValidateReferences();
        
        SpawnWallOrHole();
    }

    void InitializeSpawnPoints()
    {
        if (wallSpawnPoint == null)
            wallSpawnPoint = transform.Find("WallSpawn");

        if (holeSpawnPoint == null)
            holeSpawnPoint = transform.Find("HoleSpawn");
    }

    void ValidateReferences()
    {
        if (wallSpawnPoint == null)
            Debug.LogWarning("WallSpawn child nije pronađen! Proveri ime.");
        
        if (holeSpawnPoint == null)
            Debug.LogWarning("HoleSpawn child nije pronađen! Proveri ime.");
        
        if (wallObstaclePrefab == null)
            Debug.LogWarning("wallObstaclePrefab nije dodeljen u Inspectoru!");
        
        if (holeObstaclePrefab == null)
            Debug.LogWarning("holeObstaclePrefab nije dodeljen u Inspectoru!");
    }
    
    public void SpawnWallOrHole()
    {
        if (!ShouldSpawn()) return;

        bool spawnWall = Random.value < wallSpawnChance;
        GameObject prefabToSpawn = spawnWall ? wallObstaclePrefab : holeObstaclePrefab;
        Transform spawnPoint = spawnWall ? wallSpawnPoint : holeSpawnPoint;

        if (prefabToSpawn != null && spawnPoint != null)
        {
            Instantiate(prefabToSpawn, spawnPoint.position, Quaternion.identity, transform);
        }
    }

    bool ShouldSpawn()
    {
        return GameManager.Instance != null && 
               GameManager.Instance.State == GameState.Playing &&
               (CameraController.Instance == null || 
                CameraController.Instance.CurrentMode != CameraMode.TopDown);
    }
}