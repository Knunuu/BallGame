using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private RoomFactory roomFactory;

    [SerializeField] private Transform[] roomLocations = new Transform[5];
    private Room[] rooms = new Room[5];

    private Player[] team1Players = new Player[2];
    private Player[] team2Players = new Player[2];
    private Ball ball;
    private Camera cameraController;

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

        rooms[2] = roomFactory.CreateVariationRoom(roomLocations[2], 0, 0); //creates the first room with the empty variation

        for (int i = 0; i < roomLocations.Length; i++)
        {
            if (i == 2) // Skip the middle room since it's already created
                continue;

            rooms[i] = roomFactory.CreateRandomRoom(roomLocations[i]);
        }

        LoadRoom(2); // Load the middle room at the start
    }

    private void Update()
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

    public void LoadRoom(int roomIndex)
    {
        if (roomIndex < 0 || roomIndex >= rooms.Length)
        {
            Debug.LogError("Invalid room index: " + roomIndex);
            return;
        }

        currentRoomIndex = roomIndex;
        currentRoom = rooms[roomIndex];

        NotifyRoomLoaded();
        Debug.Log("Loaded Room: " + currentRoom.name);
    }

    public void NotifyRoomLoaded()
    {
        if (team1Players != null)
        {
            foreach (var player in team1Players)
            {
                if (player != null)
                    player.OnRoomLoaded();
            }
        }

        if (team2Players != null)
        {
            foreach (var player in team2Players)
            {
                if (player != null)
                    player.OnRoomLoaded();
            }
        }

        if (ball != null)
        {
            ball.OnRoomLoaded();
        }

        if (cameraController != null)
        {
            cameraController.OnRoomLoaded();
        }
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

    public void RegisterPlayer(Player player, int team)
    {
        if (player != null)
        {
            if (team == 1)
            {
                for (int i = 0; i < team1Players.Length; i++)
                {
                    if (team1Players[i] == null)
                    {
                        team1Players[i] = player;
                        break;
                    }
                }
            }
            else if (team == 2)
            {
                for (int i = 0; i < team2Players.Length; i++)
                {
                    if (team2Players[i] == null)
                    {
                        team2Players[i] = player;
                        break;
                    }
                }
            }
        }
    }

    public void RegisterBall(Ball ballInstance)
    {
        if (ballInstance != null)
        {
            ball = ballInstance;
        }
    }

    public void RegisterCamera(Camera cameraInstance)
    {
        if (cameraInstance != null)
        {
            cameraController = cameraInstance;
        }
    }
}
