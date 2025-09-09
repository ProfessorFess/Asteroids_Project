using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 180f;
    
    [Header("Screen Bounds")]
    public float screenWidth = 10f;
    public float screenHeight = 6f;
    
    private Rigidbody2D rb;
    private Keyboard keyboard;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        keyboard = Keyboard.current;
    }
    
    void Update()
    {
        // Keep player on screen
        KeepOnScreen();
    }
    
    void FixedUpdate()
    {
        // Check if keyboard is available
        if (keyboard == null) return;
        
        // Move forward/backward
        if (keyboard.wKey.isPressed || keyboard.upArrowKey.isPressed)
        {
            // Use MovePosition for Kinematic rigidbodies
            Vector2 newPosition = rb.position + (Vector2)transform.up * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(newPosition);
        }
        
        // Rotate left/right
        if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed)
        {
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }
        if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed)
        {
            transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
        }
    }
    
    void KeepOnScreen()
    {
        Vector3 pos = transform.position;
        
        // Wrap around screen edges
        if (pos.x > screenWidth / 2)
            pos.x = -screenWidth / 2;
        else if (pos.x < -screenWidth / 2)
            pos.x = screenWidth / 2;
            
        if (pos.y > screenHeight / 2)
            pos.y = -screenHeight / 2;
        else if (pos.y < -screenHeight / 2)
            pos.y = screenHeight / 2;
            
        transform.position = pos;
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Asteroid"))
        {
            Debug.Log("Player hit asteroid!");
            // Player died - notify GameManager
            GameManager.Instance.PlayerDied();
        }
    }
}
