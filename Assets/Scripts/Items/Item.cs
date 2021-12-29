using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

public class Item : MonoBehaviour
{
    public string invName;
    public Inventory.ItemType type;
    public bool pickable;
    public LocalizedString displayName;

    public AudioClip speech;

    public void PickUp(Inventory inv)
    {
        inv.AddItem(this);
        Destroy(gameObject);
    }
}
