using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour
{
    [SerializeField] private float transitionSpeed = 5f;
    private GameManager subscribedGameManager;

    private void OnEnable()
    {
        SubscribeToRoomLoaded();
    }

    private void Start()
    {
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
        Transform target = GameManager.Instance.GetCameraStartLocation();

        if (target == null)
        {
            Debug.LogWarning("No camera start location found for the current room.");
            return;
        }

        StopAllCoroutines(); // Stop any ongoing camera movement
        StartCoroutine(MovePosition(target));
    }

    private IEnumerator MovePosition(Transform target)
    {
        while (Vector3.Distance(transform.position, target.position) > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, transitionSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = target.position;
    }
}
