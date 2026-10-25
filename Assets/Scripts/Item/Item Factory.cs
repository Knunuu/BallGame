using UnityEngine;
using System.Collections;

public class ItemFactory : MonoBehaviour
{
    public static ItemFactory Instance { get; private set; }

    [SerializeField] private Item[] itemPrefabs;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SpawnRandomItem(Vector3 position, float spawnTime, Transform parent = null, bool gravityEnabled = false) // Spawns a random item from the itemPrefabs array
    {
        int randomIndex = Random.Range(1, itemPrefabs.Length); //start from 1 to avoid spawning another main ball item
        StartCoroutine(SpawnItemDelayed(position, randomIndex, spawnTime, parent, gravityEnabled));
    }

    public void SpawnItem(Vector3 position, string itemName, float spawnTime, Transform parent = null, bool gravityEnabled = false) // Spawns a specific item based on its name
    {
        int itemIndex = System.Array.FindIndex(itemPrefabs, item => item.name == itemName);

        if (itemIndex < 0)
        {
            Debug.LogWarning("Item with name " + itemName + " not found.");
            return;
        }
        
        StartCoroutine(SpawnItemDelayed(position, itemIndex, spawnTime, parent, gravityEnabled));
    }

    private IEnumerator SpawnItemDelayed(Vector3 position, int itemIndex, float delay, Transform parent = null, bool gravityEnabled = false)
    {
        yield return new WaitForSeconds(delay);
        Item itemInstance = Instantiate(itemPrefabs[itemIndex], position, Quaternion.identity);
        
        if (parent != null)
        {
            itemInstance.transform.SetParent(parent);
        }

        if (gravityEnabled)
        {
            Rigidbody rb = itemInstance.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.useGravity = true;
            }
        }
    }
}
