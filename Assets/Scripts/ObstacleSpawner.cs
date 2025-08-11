using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObstacleSpawner : MonoBehaviour {
    public GameObject[] allObstacles; 
    
    private List<SpawnPoint> _spawnPoints;

    void Start() {
        _spawnPoints = GetComponentsInChildren<SpawnPoint>().ToList();

        if (_spawnPoints.Count == 0) {
            enabled = false;
            return;
        }

        SpawnObstacles();
    }

    void Update() {
        if (GameManager.Instance == null || GameManager.Instance.State != GameState.Playing) return;
    }

    void SpawnObstacles() {
        if (allObstacles == null || allObstacles.Length == 0) return;

        
        var groupsByZ = _spawnPoints
            .GroupBy(sp => Mathf.RoundToInt(sp.transform.position.z))
            .ToList();

        foreach (var group in groupsByZ) {
            var shuffled = group.OrderBy(x => Random.value).ToList();

            
            foreach (var spawnPoint in shuffled.Take(2)) {
                GameObject obstaclePrefab = allObstacles[Random.Range(0, allObstacles.Length)];
                Instantiate(obstaclePrefab, spawnPoint.transform.position, spawnPoint.transform.rotation, transform);

                Debug.Log($"[ObstacleSpawner] Spawn na lane {spawnPoint.lane} (Z={spawnPoint.transform.position.z})");
            }
        }
    }
}