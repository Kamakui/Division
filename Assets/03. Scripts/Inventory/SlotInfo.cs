using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotInfo : MonoBehaviour
{
    [SerializeField] private Item item;
    [SerializeField] private Image mainImage;
    [SerializeField] private Sprite nullImage;

    private void FixedUpdate()
    {
        item = GetComponent<Slot>().item;

        if(item == null)
        {
            mainImage.sprite = nullImage;
        }
        else
        {
            mainImage.sprite = item.mainImage;
        }
    }
}
