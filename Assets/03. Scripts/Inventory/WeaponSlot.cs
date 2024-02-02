using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WeaponSlot : Slot
{
    public GameObject weaponObj;
    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        InventoryUIManager.Instance.weaponListUI.SetActive(!InventoryUIManager.Instance.weaponListUI.activeSelf);
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);

        if (item == null)
            return;

        infoBox.weaponInfo.SetActive(true);
        infoBox.dmg.text = ((WeaponItem)item).attackValue.ToString();
        infoBox.rpm.text = ((WeaponItem)item).rpm.ToString();
        infoBox.ammo.text = ((WeaponItem)item).maxAmmo.ToString();
    }
}
