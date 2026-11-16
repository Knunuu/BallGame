using UnityEngine;

public class EarthPower : PlayerPower
{
    [SerializeField] private GameObject earthProjectile;
    [SerializeField] private float cooldown;
    [SerializeField] private int maxCharge = 2;

    private float currentCooldown = 0f;
    private int charge;
    private GameObject[] objectPool;
    private Player playerRef;

    private void Awake()
    {
        objectPool = new GameObject[maxCharge];
        playerRef = GetComponent<Player>();
    }

    private void Start()
    {
        charge = maxCharge; // Initialize charge to maxCharge at the start

        for (int i = 0; i < objectPool.Length; i++)
        {
            objectPool[i] = Instantiate(earthProjectile);
            objectPool[i].SetActive(false); // Deactivate the projectile initially
        }
    }
    
    private void Update()
    {
        if (currentCooldown > 0f)
        {
            currentCooldown -= Time.deltaTime;
        }
        else if (currentCooldown <= 0f && charge < maxCharge)
        {
            charge++;
            currentCooldown = cooldown;
        }
    }

    public override void ActivatePower()
    {
        if (charge > 0)
        {
            Transform spawnLocation = playerRef.GetHoldArea();
            charge--;

            for (int i = 0; i < objectPool.Length; i++)
            {
                if (!objectPool[i].activeInHierarchy)
                {
                    objectPool[i].transform.position = spawnLocation.position;
                    objectPool[i].transform.rotation = spawnLocation.rotation;
                    objectPool[i].SetActive(true);
                    break;
                }
            }
        }
    }
}
