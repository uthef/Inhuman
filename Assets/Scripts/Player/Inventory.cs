using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public enum ItemType
    {
        COMMON, SPECIAL
    }

    List<Item> items = new List<Item>();
    Dictionary<string, Item> specialItems = new Dictionary<string, Item>();

    public void AddItem(Item item)
    {
        if (item.type == ItemType.COMMON)
        {
            items.Add(item);
        }
        else if (item.type == ItemType.SPECIAL)
        {
            specialItems.Add(item.invName, item);
        }
    }

    public bool SpecialItemExists(string name)
    {
        return specialItems.ContainsKey(name);
    }
}
