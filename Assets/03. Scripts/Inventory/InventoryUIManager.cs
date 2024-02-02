using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIManager : Singleton<InventoryUIManager>
{
    public GameObject mainUI;
    public GameObject weaponListUI;
    public GameObject armorListUI;
    public PlayerDataUI playerDataUI;
    public Slot selectSlot;

    private void OnDisable()
    {
        mainUI.SetActive(true);
        weaponListUI.SetActive(false);
        armorListUI.SetActive(false);
        selectSlot = null;
    }
}
