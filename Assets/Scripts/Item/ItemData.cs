using UnityEngine;

public enum ItemType
{
    Consumable,   // 식량, 의약품처럼 사용하면 사라지는 아이템
    Ammo,         // 총알
    Battery,      // 손전등 충전용 배터리
    Equipment     // 총, 손전등, 가방 같은 기본 장비
}

[CreateAssetMenu(fileName = "NewItemData", menuName = "Item/Item Data")]
public class ItemData : ScriptableObject
{
    public string itemName;        // 아이템 이름
    public string description;     // 아이템 설명
    public Sprite icon;            // 인벤토리 아이콘
    public GameObject dropPrefab;  // 바닥에 떨어질 때 사용할 프리팹
    public ItemType itemType;      // 아이템 종류

    public int healAmount;         // 체력 회복량
    public int ammoAmount;         // 총알 개수
    public float batteryAmount;    // 배터리 충전량
}