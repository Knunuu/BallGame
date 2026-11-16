using UnityEngine;

public class EarthPower : PlayerPower
{
    [SerializeField] private GameObject earthProjectile;
    [SerializeField] private float coolown;

    private float currentCooldown = 0f;
    
    private void Update()
    {
        if (currentCooldown > 0f)
        {
            currentCooldown -= Time.deltaTime;
        }
    }

    public override void ActivatePower()
    {
        if (currentCooldown <= 0f)
        {
            Transform spawnLocation = playerRef.GetHoldArea();
            Instantiate(earthProjectile, spawnLocation.position, playerRef.GetHoldArea().rotation);
            currentCooldown = coolown;
        }
    }
}
