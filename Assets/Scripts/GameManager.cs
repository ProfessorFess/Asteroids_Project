using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [Header("Game Objects")]
    public GameObject playerPrefab;
    public GameObject asteroidPrefab;
    
    [Header("Spawn Settings")]
    public int initialAsteroids = 5;
    public float respawnDelay = 2f;
    public float asteroidSpawnInterval = 3f;
    public int maxAsteroids = 15;
    
    [Header("Screen Bounds")]
    public float screenWidth = 10f;
    public float screenHeight = 6f;
    
    public static GameManager Instance;
    
    private GameObject currentPlayer;
    private bool gameActive = true;
    private Keyboard keyboard;
    
    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        keyboard = Keyboard.current;
        SpawnPlayer();
        SpawnInitialAsteroids();
        StartCoroutine(ContinuousAsteroidSpawning());
    }
    
    void SpawnPlayer()
    {
        if (playerPrefab != null && currentPlayer == null)
        {
            currentPlayer = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        }
    }
    
    void SpawnInitialAsteroids()
    {
        if (asteroidPrefab != null)
        {
            for (int i = 0; i < initialAsteroids; i++)
            {
                SpawnRandomAsteroid();
            }
        }
    }
    
    void SpawnRandomAsteroid()
    {
        if (asteroidPrefab != null)
        {
            // Random position on screen edge
            Vector3 randomPos = GetRandomEdgePosition();
            
            // Random asteroid type
            GameObject asteroid = Instantiate(asteroidPrefab, randomPos, Quaternion.identity);
            Asteroid asteroidScript = asteroid.GetComponent<Asteroid>();
            asteroidScript.type = (Asteroid.AsteroidType)Random.Range(0, 3);
        }
    }
    
    Vector3 GetRandomEdgePosition()
    {
        // Spawn from screen edges
        float side = Random.Range(0, 4);
        Vector3 pos = Vector3.zero;
        
        switch ((int)side)
        {
            case 0: // Top
                pos = new Vector3(Random.Range(-screenWidth/2, screenWidth/2), screenHeight/2, 0);
                break;
            case 1: // Bottom
                pos = new Vector3(Random.Range(-screenWidth/2, screenWidth/2), -screenHeight/2, 0);
                break;
            case 2: // Left
                pos = new Vector3(-screenWidth/2, Random.Range(-screenHeight/2, screenHeight/2), 0);
                break;
            case 3: // Right
                pos = new Vector3(screenWidth/2, Random.Range(-screenHeight/2, screenHeight/2), 0);
                break;
        }
        
        return pos;
    }
    
    IEnumerator ContinuousAsteroidSpawning()
    {
        while (true)
        {
            yield return new WaitForSeconds(asteroidSpawnInterval);
            
            // Only spawn if we're under the max limit
            GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Asteroid");
            if (asteroids.Length < maxAsteroids)
            {
                SpawnRandomAsteroid();
            }
        }
    }
    
    public void PlayerDied()
    {
        if (gameActive)
        {
            Debug.Log("Player died! Respawning in " + respawnDelay + " seconds...");
            gameActive = false;
            
            // Hide the player
            if (currentPlayer != null)
            {
                currentPlayer.SetActive(false);
            }
            
            StartCoroutine(RespawnPlayer());
        }
    }
    
    IEnumerator RespawnPlayer()
    {
        // Wait for respawn delay
        yield return new WaitForSeconds(respawnDelay);
        
        // Respawn player
        if (currentPlayer != null)
        {
            // Reactivate existing player
            currentPlayer.SetActive(true);
            currentPlayer.transform.position = Vector3.zero;
        }
        else
        {
            // Create new player
            SpawnPlayer();
        }
        
        gameActive = true;
        Debug.Log("Player respawned!");
    }
    
    void Update()
    {
        // Restart game with R key
        if (keyboard != null && keyboard.rKey.wasPressedThisFrame)
        {
            RestartGame();
        }
    }
    
    void RestartGame()
    {
        // Destroy current player
        if (currentPlayer != null)
        {
            Destroy(currentPlayer);
        }
        
        // Destroy all asteroids
        GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Asteroid");
        foreach (GameObject asteroid in asteroids)
        {
            Destroy(asteroid);
        }
        
        // Respawn everything
        SpawnPlayer();
        SpawnInitialAsteroids();
        gameActive = true;
    }
}
