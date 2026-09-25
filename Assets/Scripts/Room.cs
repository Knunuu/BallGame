using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPointsTeam1;
    [SerializeField] private Transform[] spawnPointsTeam2;
    [SerializeField] private Transform ballSpawnPoint;
    [SerializeField] private Transform cameraStartLocation;

    public Transform[] GetSpawnPointsTeam1() => spawnPointsTeam1;
    public Transform[] GetSpawnPointsTeam2() => spawnPointsTeam2;
    public Transform GetBallSpawnPoint() => ballSpawnPoint;
    public Transform GetCameraStartLocation() => cameraStartLocation;
}
