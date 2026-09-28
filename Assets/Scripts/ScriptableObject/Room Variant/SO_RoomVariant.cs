using UnityEngine;

[CreateAssetMenu(fileName = "New Room Variant", menuName = "ScriptableObjects/Room Variant", order = 1)]
public class SO_RoomVariant : ScriptableObject
{
    public string variationName;
    public GameObject variationPrefab;
}
