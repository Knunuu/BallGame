using UnityEngine;

public class Earth : Item
{
    [SerializeField] private float lifeTime = 5f;
    [SerializeField] private float speed = 5f;
    [SerializeField] private GameObject wallPrefab;

    private Vector3 startPosition;
    private bool wallStopped = false;
    private GameObject wallInstance;
    private bool spawnProtection = true;

    private void Start()
    {
        startPosition = transform.position;
        wallInstance = Instantiate(wallPrefab, startPosition, Quaternion.identity);
        wallInstance.transform.localScale = new Vector3(0f, wallPrefab.transform.localScale.y, wallPrefab.transform.localScale.z);
        Invoke(nameof(DisableSpawnProtection), 0.2f); // Disable spawn protection after a short delay
    }

    private void OnEnable()
    {
        Invoke(nameof(DisableSpawnProtection), 0.2f); // Disable spawn protection after a short delay
        wallStopped = false; // Reset wallStopped when the object is enabled
        startPosition = transform.position; // Reset start position when the object is enabled
        if (wallInstance != null)
        {
            wallInstance.SetActive(true);
        }
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(DisableSpawnProtection));
        spawnProtection = true; // Reset spawn protection when the object is disabled
        if (wallInstance != null)
        {
            wallInstance.transform.localScale = new Vector3(0f, wallPrefab.transform.localScale.y, wallPrefab.transform.localScale.z); // Reset the wall scale
            wallInstance.SetActive(false); // Hide the wall instance when the object is disabled
        }
    }

    private void DisableSpawnProtection()
    {
        spawnProtection = false;
    }

    private void Update()
    {
        if (!wallStopped)//moves right until the player releases the button, stretches wall as it goes
        {
            CreateWall();
            transform.position += Vector3.right * Time.deltaTime * speed; // Move right at a speed of 5 units per second
        }
    }

    private void CreateWall()
    {
        // creates a wall from start position to current position
        Vector3 currentPosition = transform.position;
        
        wallInstance.transform.position = (startPosition + currentPosition) * 0.5f;
        wallInstance.transform.localScale = new Vector3(Vector3.Distance(startPosition, currentPosition), wallInstance.transform.localScale.y, wallInstance.transform.localScale.z);
    }

    private void WallStop()
    {
        wallStopped = true;
        Invoke(nameof(DestroyEarth), lifeTime);
    }

    public override void OnHit(Vector3 direction, float force)
    {
        WallStop();
    }

    private void DestroyEarth()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (spawnProtection)
        {
            return; // Ignore collisions during spawn protection
        }

        WallStop();
    }
}
