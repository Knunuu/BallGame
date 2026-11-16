using UnityEngine;
using System.Collections;

public class ItemPoolingSpawner : MonoBehaviour
{
    private Item[] itemPool = new Item[10];
    
    [SerializeField] private ItemFactory itemFactory;

    [SerializeField] private Vector2 spawnAreaMin = new Vector2(-5f, -5f);
    [SerializeField] private Vector2 spawnAreaMax = new Vector2(5f, 5f);
    [SerializeField] private float minSpawnTime = 1f;
    [SerializeField] private float maxSpawnTime = 5f;

    private bool isSpawning = true;

    private void Start()
    {
        for(int i = 0; i < itemPool.Length; i++)
        {
            Item item = itemFactory.CreateItem(transform.position, false, this.transform);
            item.gameObject.SetActive(false);
            itemPool[i] = item;
        }

        StartCoroutine(SpawnLoop());
    }

    private void SpawnItem(Vector3 spawnPosition)
    {
        for(int i = 0; i < itemPool.Length; i++)
        {
            if(!itemPool[i].gameObject.activeInHierarchy)
            {
                itemPool[i].transform.position = spawnPosition;
                itemPool[i].gameObject.SetActive(true);
                break;
            }
        }
    }

    private IEnumerator SpawnLoop()
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

            SpawnItem(spawnPosition); //spawn item as child to disable when needed
        }
    }

    private void OnEnable()
    {
        isSpawning = true;
        StartCoroutine(SpawnLoop());
    }

    private void OnDisable()
    {
        isSpawning = false;
        StopAllCoroutines();
    }
}
