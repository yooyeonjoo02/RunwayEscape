using UnityEngine;
using TMPro;

public class DroppedItem : MonoBehaviour
{
    public ItemData itemData;

    private TextMeshProUGUI pickupTextUI;
    private bool canPickup = false;
    private PlayerStatus playerStatus;

    private void Awake()
    {
        pickupTextUI = FindInactiveTMP("PickupText");

        if (pickupTextUI != null)
        {
            pickupTextUI.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("PickupText UI를 찾지 못했습니다.");
        }
    }

    private TextMeshProUGUI FindInactiveTMP(string objectName)
    {
        TextMeshProUGUI[] texts = Resources.FindObjectsOfTypeAll<TextMeshProUGUI>();

        foreach (TextMeshProUGUI text in texts)
        {
            if (text.gameObject.name == objectName && text.gameObject.scene.IsValid())
            {
                return text;
            }
        }

        return null;
    }

    private void Update()
    {
        if (canPickup && Input.GetKeyDown(KeyCode.E))
        {
            ApplyItemEffect();

            if (pickupTextUI != null)
            {
                pickupTextUI.gameObject.SetActive(false);
            }

            Destroy(gameObject);
        }
    }

    private void ApplyItemEffect()
    {
        if (playerStatus == null || itemData == null) return;

        switch (itemData.itemType)
        {
            case ItemType.Consumable:
                playerStatus.Heal(itemData.healAmount);
                break;

            case ItemType.Ammo:
                playerStatus.AddAmmo(itemData.ammoAmount);
                break;

            case ItemType.Battery:
                playerStatus.ChargeBattery(itemData.batteryAmount);
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerStatus = other.GetComponent<PlayerStatus>();

            if (playerStatus == null) return;

            canPickup = true;

            if (pickupTextUI != null)
            {
                pickupTextUI.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPickup = false;
            playerStatus = null;

            if (pickupTextUI != null)
            {
                pickupTextUI.gameObject.SetActive(false);
            }
        }
    }
}