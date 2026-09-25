using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        GameManager.Instance.RegisterBall(this);
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
