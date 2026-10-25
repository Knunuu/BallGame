using UnityEngine;
using System.Collections;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private Vector2 spawnAreaMin = new Vector2(-5f, -5f);
    [SerializeField] private Vector2 spawnAreaMax = new Vector2(5f, 5f);
    [SerializeField] private float minSpawnTime = 1f;
    [SerializeField] private float maxSpawnTime = 5f;

    private bool isSpawning = true;

    private void Start()
    {
        StartCoroutine(SpawnItems());
    }

    private IEnumerator SpawnItems()
    {
        while (isSpawning)
        {
            float spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(spawnTime);

            Vector3 spawnPosition = new Vector3(
                Random.Range(spawnAreaMin.x, spawnAreaMax.x) + transform.position.x,
                Random.Range(spawnAreaMin.y, spawnAreaMax.y) + transform.position.y,
                0f
            );

            bool gravityEnabled = Random.value > 0.5f; // Randomly decide if gravity should be enabled for this item

           ItemFactory.Instance.SpawnRandomItem(spawnPosition, 0f, this.transform, gravityEnabled); //spawn item as child to disable when needed
        }
    }

    private void OnEnable()
    {
        isSpawning = true;
        StartCoroutine(SpawnItems());
    }

    private void OnDisable()
    {
        isSpawning = false;
        StopAllCoroutines();
    }
}
