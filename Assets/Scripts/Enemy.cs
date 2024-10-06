using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 5f;
    public int scoreValue = 10; // Points awarded for destroying this enemy
    public float damage = 0.1f; // Damage dealt to HQ (10% of total health)

    private GameManager gameManager;

    private void Start()
    {
        // Find the GameManager in the scene
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in the scene!");
        }
    }

    private void Update()
    {
        // Move forward (towards the player) in a straight line
        transform.Translate(Vector3.back * speed * Time.deltaTime);

        // Check if the enemy has moved past the player
        if (transform.position.z < -60f) // Adjust this value based on your game's scale
        {
            Destroy(gameObject);
        }
    }

    public void InitVelocity()
    {
        // This method is called by the WaveManager after instantiation
        // You can add any initialization logic here if needed
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Handle collision with player
            Debug.Log("Enemy hit the player!");
            // You might want to damage the player here
            Destroy(gameObject);
        }
        else if (other.CompareTag("PlayerBullet"))
        {
            // Handle being hit by player's bullet
            Debug.Log("Enemy destroyed by player's bullet!");
            if (gameManager != null)
            {
                gameManager.UpdateScore(scoreValue);
            }
			Destroy(other.gameObject);
            Destroy(gameObject);
        }
        else if (other.CompareTag("HeadQuarters"))
        {
            Debug.Log("Enemy reached HQ");
            if (gameManager != null)
            {
                gameManager.DamageHQ(damage);
            }
            Destroy(gameObject);
        }
    }
}