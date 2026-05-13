using UnityEngine;
using TMPro;

public class DroppedItem : MonoBehaviour
{
    public ItemData itemData;

    // 🔥 Inspector에서 직접 연결
    [SerializeField] private TextMeshProUGUI pickupTextUI;

    private bool canPickup = false;
    private PlayerStatus playerStatus;

    private void Awake()
    {
        // UI가 연결 안 되어 있으면 경고
        if (pickupTextUI == null)
        {
            Debug.LogWarning("PickupText UI가 Inspector에 연결되지 않았습니다.");
            return;
        }

        // 처음에는 꺼두기
        pickupTextUI.gameObject.SetActive(false);
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