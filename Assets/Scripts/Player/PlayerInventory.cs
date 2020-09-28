using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// PlayerInventory manages the items of the player.
/// </summary>
public class PlayerInventory : MonoBehaviour
{
    /// <summary>
    /// All items of the player.
    /// </summary>
    public List<Item> inventory;

    /// <summary>
    /// The weapon the player is using.
    /// </summary>
    public Weapon currentWeapon;

    private void Start()
    {
        Item[] items = GetComponentsInChildren<Item>();
        inventory = items.ToList();
        currentWeapon = null;
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

    /// <summary>
    /// Sets the weapon of the player.
    /// </summary>
    /// <param name="name">the name of the weapon</param>
    /// <returns>is the weapon set sucessfully</returns>
    public bool SetWeapon(string name)
    {
        Item weaponItem = GetItem(name);

        if (weaponItem != null)
        {
            Weapon weapon = weaponItem.GetComponent<Weapon>();

            if (weapon != null)
            {
                currentWeapon = weapon;

                return true;
            }
        }

        return false;
    }
}
