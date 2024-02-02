using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WeaponBoxInfo : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] Image typeIcon;
    [SerializeField] TextMeshProUGUI itemName;
    [SerializeField] TextMeshProUGUI level;
    [SerializeField] TextMeshProUGUI damage;
    [SerializeField] TextMeshProUGUI rpm;
    [SerializeField] TextMeshProUGUI ammo;
    private WeaponItem item;

    private void OnEnable()
    {
        item = (WeaponItem)(GetComponent<WeaponSlot>().item);

        icon.sprite = item.mainImage;
        typeIcon.sprite = item.typeImage;
        itemName.text = item.itemName;
        //level.text = item.level;
        damage.text = item.attackValue.ToString();
        rpm.text = item.rpm.ToString();
        ammo.text = item.maxAmmo.ToString();
    }
}
