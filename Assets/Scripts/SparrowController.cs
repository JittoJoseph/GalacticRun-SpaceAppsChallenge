using UnityEngine;

public class SparrowController : MonoBehaviour
{
    public Transform[] positionObjects;
    public float transitionSpeed = 5.0f;
    public GameObject bulletPrefab;
    public float fireRate = 0.25f; // seconds

    private int currentPositionIndex = 1; // Start at the middle position
    private Vector3 targetPosition;
    private float nextFireTime = 0.0f;

    private void Start()
    {
        if (positionObjects.Length != 3)
        {
            Debug.LogError("Please assign exactly 3 position objects in the inspector.");
            return;
        }

        ResetPosition();
    }

    private void Update()
    {
        // Only allow movement and shooting if the script is enabled (controlled by GameManager)
        if (!enabled) return;

        HandleMovement();
        HandleShooting();
    }

    private void HandleMovement()
    {
        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && currentPositionIndex < 2)
        {
            currentPositionIndex++;
            targetPosition = positionObjects[currentPositionIndex].position;
        }
        else if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && currentPositionIndex > 0)
        {
            currentPositionIndex--;
            targetPosition = positionObjects[currentPositionIndex].position;
        }

        transform.position = Vector3.Lerp(transform.position, targetPosition, transitionSpeed * Time.deltaTime);
    }

    private void HandleShooting()
    {
        if ((Input.GetMouseButton(0) || Input.GetKeyDown(KeyCode.Space)) && Time.time > nextFireTime)
        {
            SpawnBullet();
            nextFireTime = Time.time + fireRate;
        }
    }

    private void SpawnBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        // You can add any additional bullet initialization here if needed
    }

    public void ResetPosition()
    {
        currentPositionIndex = 1;
        transform.position = positionObjects[currentPositionIndex].position;
        targetPosition = transform.position;
    }
}