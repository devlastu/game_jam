using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    public static EnvironmentManager Instance { get; private set; }

    void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
    }

    public void EnableFog(float density = 0.06f) {
        RenderSettings.fog = true;
        RenderSettings.fogDensity = density;
        // Debug.Log("[EnvironmentManager] Fog enabled");
    }

    public void DisableFog() {
        RenderSettings.fogDensity = 0.03f;
        // Debug.Log("[EnvironmentManager] Fog disabled");
    }

    // Ovde možeš dodati još metoda za environment, kao npr:
    // ChangeLighting(), EnableRainEffect(), itd.
}