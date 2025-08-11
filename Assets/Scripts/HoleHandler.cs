using UnityEngine;

public class HoleHandler : MonoBehaviour
{
    public int normalSphereLayer, fallingSphereLayer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == normalSphereLayer && other.gameObject.CompareTag("Player")){
            
            if (CameraController.Instance != null && CameraController.Instance.CurrentMode != CameraMode.SideView){
                other.gameObject.layer = fallingSphereLayer;
                // Debug.Log($"[HoleHandler] Mijenjam layer na FallingSphere jer je kamera u modu {CameraController.Instance.CurrentMode}");
            }
            else {
                Debug.Log("[HoleHandler] Kamera je SideView, igrač ne propada.");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == fallingSphereLayer){
            other.gameObject.layer = normalSphereLayer;
            // Debug.Log("[HoleHandler] Vraćam layer na NormalSphere");
        }
    }
    
}