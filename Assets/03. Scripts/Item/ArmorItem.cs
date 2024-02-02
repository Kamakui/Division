using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ArmorType
{ 
    Hat,
    Vest,
    Knee,
    Bag,
    Glove,
    Shoose
}

public class ArmorItem : Item
{
    public int shieldValue;
    public ArmorType armorType;

    public void Use()
    {
        Equip();
    }

    public void Equip()
    {
        
    }
}
