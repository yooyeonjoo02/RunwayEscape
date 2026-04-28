using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [Header("Health")]
    public float maxHealth = 100f;
    public float currentHealth = 100f;

    [Header("Battery")]
    public float maxBattery = 100f;
    public float currentBattery = 100f;

    [Header("Ammo")]
    public int ammo = 0;

    public void Heal(float amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void ChargeBattery(float amount)
    {
        currentBattery += amount;

        if (currentBattery > maxBattery)
        {
            currentBattery = maxBattery;
        }
    }

    public void AddAmmo(int amount)
    {
        ammo += amount;

        if (ammo < 0)
        {
            ammo = 0;
        }
    }
}