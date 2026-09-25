using System.Collections.Generic;
using UnityEngine;

public class RoomFactory : MonoBehaviour
{
    //creates rooms with variations and spawns players and the ball in the correct locations

    [SerializeField] private GameObject baseRoom; // Assign the base room prefab in the inspector
    public List<RoomVariation> roomVariations = new List<RoomVariation>(); // Assign the room variations in the inspector

    //creates a room with a specified number of variations at the given location
    public Room CreateRandomRoom(Transform location)
    {
        if (roomVariations == null || roomVariations.Count < 2)
        {
            Debug.LogWarning("Need at least 2 room variations to create a unique room setup.");
            return null;
        }

        GameObject roomInstance = Instantiate(baseRoom, location.position, location.rotation);

        Transform variationParent = new GameObject("RoomVariations").transform;
        variationParent.SetParent(roomInstance.transform);

        int firstIndex = Random.Range(0, roomVariations.Count);
        int secondIndex = firstIndex;

        while (secondIndex == firstIndex)
        {
            secondIndex = Random.Range(0, roomVariations.Count);
        }

        SpawnVariationIfValid(roomVariations[firstIndex], location, variationParent);
        SpawnVariationIfValid(roomVariations[secondIndex], location, variationParent);
        return roomInstance.GetComponent<Room>();
    }

    public Room CreateVariationRoom(Transform location, int variationIndex1, int variationIndex2)
    {
        GameObject roomInstance = Instantiate(baseRoom, location.position, location.rotation);
        Transform variationParent = new GameObject("RoomVariations").transform;
        variationParent.SetParent(roomInstance.transform);

        SpawnVariationIfValid(roomVariations[variationIndex1], location, variationParent);
        SpawnVariationIfValid(roomVariations[variationIndex2], location, variationParent);

        return roomInstance.GetComponent<Room>();
    }

    private void SpawnVariationIfValid(RoomVariation variation, Transform location, Transform parent)
    {
        if (variation == null)
            return;

        if (variation.variationName == "None") //this is here for the empty variation option for normal rooms
            return;

        if (variation.variationPrefab == null)
            return;

        GameObject variationInstance = Instantiate(variation.variationPrefab, location.position, location.rotation);
        variationInstance.transform.SetParent(parent);
    }
}