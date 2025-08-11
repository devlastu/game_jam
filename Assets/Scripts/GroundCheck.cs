using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public float downRayDistance = 1f;       // Raycast za tlo ispod
    public float forwardDownRayDistance = 1.5f; // Raycast dijagonalno ispred i dole
    public float forwardOffsetHeight = 0.5f; // Visina sa koje startuje forward raycast
    public float forwardOffsetDistance = 0.5f; // Koliko unaprijed gleda forward raycast

    public LayerMask groundLayer;  

    public int normalSphereLayer;
    public int fallingSphereLayer;

    private void FixedUpdate()
    {
        // Raycast pravo dole
        bool isGroundBelow = Physics.Raycast(transform.position, Vector3.down, downRayDistance, groundLayer);

        // Raycast dijagonalno ispred i dole
        Vector3 forwardRayOrigin = transform.position + Vector3.up * forwardOffsetHeight + transform.forward * forwardOffsetDistance;
        bool isGroundAhead = Physics.Raycast(forwardRayOrigin, Vector3.down, forwardDownRayDistance, groundLayer);

        if (!isGroundBelow || !isGroundAhead)
        {
            // Debug.Log("[GroundCheck] Nema tla ispod ili ispred, igrač propada!");

            if (gameObject.layer != fallingSphereLayer)
            {
                gameObject.layer = fallingSphereLayer;
            }

            PlayerMovement.Instance.StopPlayerMovement();
            PlayerHealth.Instance.KillPlayer();
        }
        else
        {
            // Ako si na tlu, vraćaš layer i omogućavaš kretanje ako treba
            if (gameObject.layer != normalSphereLayer)
            {
                // Debug.Log("[GroundCheck] Igrač na tlu, vraćam layer normalSphereLayer.");
                gameObject.layer = normalSphereLayer;
            }
        }
    }
}