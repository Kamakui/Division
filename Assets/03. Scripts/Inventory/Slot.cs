using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Slot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] protected ItemInfo infoBox;
    public Item item;
    public AudioClip clip;
    public virtual void OnPointerClick(PointerEventData eventData)
    {
        InventoryUIManager.Instance.mainUI.SetActive(!InventoryUIManager.Instance.mainUI.activeSelf);

        if (InventoryUIManager.Instance.selectSlot == null)
            InventoryUIManager.Instance.selectSlot = this;
        else
        {
            InventoryUIManager.Instance.selectSlot.item = this.item;
            if(InventoryUIManager.Instance.selectSlot.TryGetComponent<WeaponSlot>(out WeaponSlot weaponSlot))
            {
                weaponSlot.weaponObj = Instantiate(InventoryUIManager.Instance.selectSlot.item.gameObject, Inventory.Instance.handle.transform);
                weaponSlot.weaponObj.SetActive(false);
            }
            else if(InventoryUIManager.Instance.selectSlot.TryGetComponent<ArmorSlot>(out ArmorSlot armorSlot))
            {
                Inventory.Instance.ApplyArmor();
            }
            InventoryUIManager.Instance.selectSlot = null;
            InventoryUIManager.Instance.playerDataUI.ApplyInfo();

        }

        GameObject soundObj = ObjectPoolingManager.Instance.Pop("Sound");
        soundObj.GetComponent<SoundComponent>().Play(clip, transform);
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        this.GetComponent<RectTransform>().localScale *= 1.1f;
        infoBox.upper.SetActive(true);
        infoBox.lower.SetActive(true);

        if (item != null)
        {
            infoBox.itemName.text = item.itemName;
            infoBox.toolTip.text = item.toolTip;
        }
        else
        {
            infoBox.itemName.text = "";
            infoBox.toolTip.text = "Empty";
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        InventoryOff();
    }

    public virtual void OnDisable()
    {
        InventoryOff();
    }

    private void InventoryOff()
    {
        this.GetComponent<RectTransform>().localScale = Vector3.one;
        infoBox.weaponInfo.SetActive(false);
        infoBox.armorInfo.SetActive(false);
        infoBox.upper.SetActive(false);
        infoBox.lower.SetActive(false);
    }
}
