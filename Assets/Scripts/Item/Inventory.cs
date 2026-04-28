using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int maxSlotCount = 6;
    public List<ItemData> items = new List<ItemData>();

    public bool AddItem(ItemData itemData)
    {
        if (items.Count >= maxSlotCount)
        {
            return false;
        }

        items.Add(itemData);

        return true;
    }
}