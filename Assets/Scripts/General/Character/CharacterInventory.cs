using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Inventory manages the items of the character.
/// </summary>
public class CharacterInventory : MonoBehaviour
{
    /// <summary>
    /// All items of the character.
    /// </summary>
    public List<Item> inventory;

    /// <summary>
    /// The weapon the character is using.
    /// </summary>
    public List<Weapon> weapons;

    private void Start()
    {
        Item[] items = GetComponentsInChildren<Item>();
        inventory = items.ToList();
        weapons = new List<Weapon>();
    }

    /// <summary>
    /// Gets the item with the given name.
    /// </summary>
    /// <param name="name">the name of the item</param>
    /// <returns>the item</returns>
    public Item GetItem(string name)
    {
        foreach (Item item in inventory)
        {
            if (item.itemName.Equals(name))
            {
                return item;
            }
        }

        return null;
    }

    public Weapon GetWeapon(string name)
    {
        return GetItem(name).GetComponent<Weapon>();
    }
}
