using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorListUI : Singleton<ArmorListUI>
{
    public List<ArmorSlot> armorList = new List<ArmorSlot>();

    private void OnEnable()
    {
        int count = 0;

        if(Inventory.Instance == null)
            return;

        foreach (Item item in Inventory.Instance.itemList)
        {
            if (item is ArmorItem)
            {
                if (((ArmorItem)item).armorType == ((ArmorSlot)InventoryUIManager.Instance.selectSlot).armorType)
                {
                    if (count == ObjectPoolingManager.Instance.poolingDic["ArmorItem"].Count)
                        ObjectPoolingManager.Instance.Init(ObjectPoolingManager.Instance.dataDic["ArmorItem"], ObjectPoolingManager.Instance.dataDic["ArmorItem"].size);

                    armorList[count].item = item;
                    armorList[count].gameObject.SetActive(true);
                    count++;
                }
            }
        }
    }

    private void OnDisable()
    {
        foreach (ArmorSlot slot in armorList)
        {
            slot.item = null;
            slot.gameObject.SetActive(false);
        }
    }
}
