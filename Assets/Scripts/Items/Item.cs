using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string invName;
    public Inventory.ItemType type;
    public string displayName;

    public AudioClip speech;

    public void PickUp(Inventory inv)
    {
        Destroy(gameObject);
        inv.AddItem(this);
    }
}
