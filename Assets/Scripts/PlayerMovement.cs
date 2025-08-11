using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance { get; private set; }

    public Rigidbody rb;
    public float sidewaysForce;
    private bool _invertedControls = false;
    private bool _canMove = true;
    
    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }
    
    public void SetNormalControls()
    {
        _invertedControls = false;
    }

    public void SetInvertedControls()
    {
        _invertedControls = true;
        
    }
    
    public void ToggleControls()
    {
        _invertedControls = !_invertedControls;
        // Debug.Log("[PlayerMovement] Kontrole su sada " + (_invertedControls ? "invertovane" : "normalne"));
    }
    

    public void StopPlayerMovement()
    {
        _canMove = false;
    }

    public void ResumePlayerMovement()
    {
        _canMove = true;
    }

    void FixedUpdate()
    {
        if (!_canMove) return;
        
        float moveMultiplier = _invertedControls ? -1f : 1f;
        
        rb.MovePosition(rb.position + Vector3.forward * 0.001f);
        rb.MovePosition(rb.position + Vector3.back * -0.001f);
        
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.AddForce(sidewaysForce * moveMultiplier, 0, 0, ForceMode.VelocityChange);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.AddForce(-sidewaysForce * moveMultiplier, 0, 0, ForceMode.VelocityChange);
        }
    }

}