using UnityEngine;

public class ItemPoolingSpawner : MonoBehaviour
{
    private GameObject[] itemPool = new GameObject[30];

    private void Start()
    {
        for(int i = 0; i < itemPool.Length; i++)
        {
            //GameObject item = ItemFactory.Instance.SpawnItem(transform.position, "Mana", 0f, this.transform);
            //item.SetActive = false;
            //itemPool[i] = item;
        }
    }
}
