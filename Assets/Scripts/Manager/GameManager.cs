using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public event Action RoomLoaded;
    public bool HasLoadedRoom => currentRoom != null;

    private RoomFactory roomFactory;

    [SerializeField] private Transform[] roomLocations = new Transform[5];
    private Room[] rooms = new Room[5];

    private Room currentRoom;
    private int currentRoomIndex;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        roomFactory = GetComponent<RoomFactory>();
        CreateRooms();
    }

    private void CreateRooms()
    {
        if (roomFactory == null)
            return;

        rooms[2] = roomFactory.CreateVariationRoom(roomLocations[2], "None", "None"); //creates the first room with the empty variation

        for (int i = 0; i < roomLocations.Length; i++)
        {
            if (i == 2) // Skip the middle room since it's already created
                continue;

            rooms[i] = roomFactory.CreateRandomRoom(roomLocations[i]);
        }

        ItemFactory.Instance.SpawnItem(rooms[2].GetBallSpawnPoint().position, "Ball", 0f); //creates the ball in the middle room
        // ball spawned as null parent so that it is destroyed when changing scenes, but not when changing rooms
        LoadRoom(2); // Load the middle room at the start
    }

    private void Update() //for testing purposes, allows switching between rooms using number keys 1-5
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            LoadRoom(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            LoadRoom(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            LoadRoom(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            LoadRoom(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            LoadRoom(4);
        }
    }

    public void OnGoalScored(int teamNumber)
    {
        Debug.Log("Goal scored by Team " + teamNumber + " in Room " + currentRoomIndex);

        if (teamNumber == 1)
        {
           LoadRoom(currentRoomIndex - 1); // Load the next room when a goal is scored
        }
        else if (teamNumber == 2)
        {
            LoadRoom(currentRoomIndex + 1); // Load the next room when a goal is scored
        }

    }

    private void LoadRoom(int roomIndex)
    {
        if (roomIndex < 0 || roomIndex >= rooms.Length)
        {
            Debug.LogError("Invalid room index: " + roomIndex);
            return;
        }

        // Disable all non-active rooms
        for (int i = 0; i < rooms.Length; i++)
        {
            if (rooms[i] != null)
            {
                rooms[i].gameObject.SetActive(i == roomIndex);
            }
        }

        currentRoomIndex = roomIndex;
        currentRoom = rooms[roomIndex];

        NotifyRoomLoaded();
        Debug.Log("Loaded Room: " + currentRoom.name);
    }

    private void NotifyRoomLoaded()
    {
        RoomLoaded?.Invoke();
    }

    public Transform[] GetSpawnPoints(int team)
    {
        if (currentRoom == null)
        {
            return null;
        }

        if (team == 1)
        {
            return currentRoom.GetSpawnPointsTeam1();
        }

        if (team == 2)
        {
            return currentRoom.GetSpawnPointsTeam2();
        }

        return null;
    }

    public Transform GetBallSpawnPoint()
    {
        return currentRoom != null ? currentRoom.GetBallSpawnPoint() : null;
    }

    public Transform GetCameraStartLocation()
    {
        return currentRoom != null ? currentRoom.GetCameraStartLocation() : null;
    }

}
