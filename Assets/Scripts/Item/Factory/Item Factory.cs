using UnityEngine;
using System.Collections;

public class ItemFactory : MonoBehaviour
{
    [SerializeField] private GameObject baseItem; // Assign the base item prefab in the inspector
    [SerializeField] private bool gravityEnabled = false;

    public virtual Item CreateItem(Vector3 location, bool enableGravity = false, Transform parent = null)
    {
        GameObject itemInstance = Instantiate(baseItem, location, Quaternion.identity, parent);
        Item itemComponent = itemInstance.GetComponent<Item>();

        if (itemComponent != null)
        {
            itemComponent.GetComponent<Rigidbody>().useGravity = enableGravity;
        }

        return itemComponent;
    }
}
