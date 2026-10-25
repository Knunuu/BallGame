using UnityEngine;

public class Ball : Item
{
    private GameManager subscribedGameManager;

    private void OnEnable()
    {
        SubscribeToRoomLoaded();
    }

    protected override void Start()
    {
        base.Start();
        SubscribeToRoomLoaded();
        if (subscribedGameManager != null && subscribedGameManager.HasLoadedRoom)
        {
            OnRoomLoaded();
        }
    }

    private void OnDisable()
    {
        if (subscribedGameManager != null)
        {
            subscribedGameManager.RoomLoaded -= OnRoomLoaded;
            subscribedGameManager = null;
        }
    }

    private void SubscribeToRoomLoaded()
    {
        if (GameManager.Instance == null || subscribedGameManager == GameManager.Instance)
        {
            return;
        }

        subscribedGameManager = GameManager.Instance;
        subscribedGameManager.RoomLoaded += OnRoomLoaded;
    }

    public void OnRoomLoaded()
    {
        Respawn();
        // Additional logic for when a room is loaded can be added here
    }

    private void Respawn()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.position = GameManager.Instance.GetBallSpawnPoint().position;
    }
}
