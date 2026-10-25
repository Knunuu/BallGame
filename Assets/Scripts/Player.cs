using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Range(1, 2)] [SerializeField] private int team; //not supposed to be changed in the inspector except for testing purposes
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private PlayerPower equippedPower;

    private Rigidbody rb;
    private Vector3 movementInput;
    private IPlayerCommand powerCommand;
    private GameManager subscribedGameManager;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        EquipPower(equippedPower);
    }

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


    private void FixedUpdate()
    {
        if (rb != null)
        {
            Move();
        }
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        movementInput = ctx.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && rb != null)
        {
            Jump();
        }
    }

    public void OnPower(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && powerCommand != null)
        {
            powerCommand.Execute();
        }
    }

    public void EquipPower(PlayerPower newPower)
    {
        equippedPower = newPower;
        powerCommand = equippedPower != null ? new ActivatePowerCommand(equippedPower) : null;
    }

    private void Move()
    {
        Vector3 move = new Vector3(movementInput.x, 0f, movementInput.y) * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + move);
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z); // Reset vertical velocity before applying jump force
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
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
