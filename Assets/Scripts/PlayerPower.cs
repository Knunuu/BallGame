using UnityEngine;

public class PlayerPower : MonoBehaviour
{
    public virtual void ActivatePower()
    {
        // Implement the logic for activating the player's power here
        Debug.Log("Player power activated!");
    }

    public virtual void DeactivatePower()
    {
        // Implement the logic for deactivating the player's power here
        Debug.Log("Player power deactivated!");
    }

    // protected virtual void ChargePower(float chargeAmount)
    // {
    //     // Implement the logic for charging the player's power here
    //     Debug.Log("Player power charged by: " + chargeAmount);
    // }
}

public interface IPlayerCommand
{
    void Execute();
}

public sealed class ActivatePowerCommand : IPlayerCommand
{
    private readonly PlayerPower power;

    public ActivatePowerCommand(PlayerPower power)
    {
        this.power = power;
    }

    public void Execute()
    {
        if (power != null)
        {
            power.ActivatePower();
        }
    }
}
