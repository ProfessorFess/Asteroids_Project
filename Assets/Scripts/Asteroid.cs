using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [Header("Asteroid Types")]
    public AsteroidType type = AsteroidType.Large;
    
    [Header("Movement Settings")]
    public float moveSpeed = 2f;
    public float rotationSpeed = 30f;
    
    [Header("Screen Bounds")]
    public float screenWidth = 10f;
    public float screenHeight = 6f;
    
    private Vector2 randomDirection;
    private float randomRotation;
    
    public enum AsteroidType
    {
        Large,
        Medium,
        Small
    }
    
    void Start()
    {
        // Set random movement direction
        randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        randomRotation = Random.Range(-rotationSpeed, rotationSpeed);
        
        // Set visual properties based on type
        SetupAsteroidType();
    }
    
    void Update()
    {
        // Move in constant direction (like original Asteroids)
        transform.Translate(randomDirection * moveSpeed * Time.deltaTime, Space.World);
        
        // Rotate
        transform.Rotate(0, 0, randomRotation * Time.deltaTime);
        
        // Keep asteroid on screen
        KeepOnScreen();
    }
    
    void SetupAsteroidType()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        
        switch (type)
        {
            case AsteroidType.Large:
                transform.localScale = Vector3.one * 1.5f;
                sr.color = Color.gray;
                moveSpeed = 1.5f;
                break;
            case AsteroidType.Medium:
                transform.localScale = Vector3.one * 1f;
                sr.color = Color.gray;
                moveSpeed = 2.5f;
                break;
            case AsteroidType.Small:
                transform.localScale = Vector3.one * 0.6f;
                sr.color = Color.gray;
                moveSpeed = 3.5f;
                break;
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
        if (other.CompareTag("Player"))
        {
            // Asteroid hit player - this is handled in Player script
        }
    }
}
