using UnityEngine;

public class Player : MonoBehaviour
{
    [Range(1, 2)] [SerializeField] private int team; //not supposed to be changed in the inspector except for testing purposes

    private void Start()
    {
        GameManager.Instance.RegisterPlayer(this, team);
    }
    
    public void OnRoomLoaded()
    {
        Respawn();
        // Additional logic for when a room is loaded can be added here
    }

    private void Respawn()
    {
        transform.position = GameManager.Instance.GetSpawnPoints(team)[0].position;
    }
}
