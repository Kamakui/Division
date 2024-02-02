using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ArmorBoxInfo : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] Image typeIcon;
    [SerializeField] TextMeshProUGUI itemName;
    [SerializeField] TextMeshProUGUI level;
    [SerializeField] TextMeshProUGUI shieldValue;
    private ArmorItem item;

    private void OnEnable()
    {
        item = (ArmorItem)(GetComponent<ArmorSlot>().item);

        icon.sprite = item.mainImage;
        //typeIcon.sprite = item.typeImage;
        itemName.text = item.itemName;
        //level.text = item.level;
        shieldValue.text = item.shieldValue.ToString();
    }
}
