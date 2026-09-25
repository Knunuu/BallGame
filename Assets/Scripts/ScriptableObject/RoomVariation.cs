using UnityEngine;

[CreateAssetMenu(fileName = "New Room Variation", menuName = "Room Variation")]
public class RoomVariation : ScriptableObject
{
    public string variationName;
    public GameObject variationPrefab;
}
