using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ArmorSlot : Slot
{
    public ArmorType armorType;
    
    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        InventoryUIManager.Instance.armorListUI.SetActive(!InventoryUIManager.Instance.armorListUI.activeSelf);
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);

        if (item == null)
            return;

        infoBox.armorInfo.SetActive(true);
        infoBox.shield.text = ((ArmorItem)item).shieldValue.ToString();
    }
}
