using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponListUI : Singleton<WeaponListUI>
{
    public List<WeaponSlot> weaponList = new List<WeaponSlot>();

    private void OnEnable()
    {
        int count = 0;

        if (Inventory.Instance == null)
            return;

        foreach (Item item in Inventory.Instance.itemList)
        {
            if (item is WeaponItem)
            {
                if (count == ObjectPoolingManager.Instance.poolingDic["WeaponItem"].Count)
                    ObjectPoolingManager.Instance.Init(ObjectPoolingManager.Instance.dataDic["WeaponItem"], ObjectPoolingManager.Instance.dataDic["WeaponItem"].size);

                weaponList[count].item = item;
                weaponList[count].gameObject.SetActive(true);
                count++;
            }
        }
    }

    private void OnDisable()
    {
        foreach (WeaponSlot slot in weaponList)
        {
            slot.item = null;
            slot.gameObject.SetActive(false);
        }
    }
}
