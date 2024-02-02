using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDataUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI level;
    [SerializeField] TextMeshProUGUI damage;
    [SerializeField] TextMeshProUGUI rpm;
    [SerializeField] TextMeshProUGUI ammo;
    [SerializeField] TextMeshProUGUI shield;
    [SerializeField] TextMeshProUGUI hp;
    [SerializeField] private PlayerData plData;
    [SerializeField] private WeaponItem curWeapon;

    private void OnEnable()
    {
        ApplyInfo();
    }

    public void ApplyInfo()
    {
        if (plData.CurWeapon != null)
        {
            curWeapon = plData.CurWeapon.GetComponent<WeaponItem>();
            damage.text = curWeapon.attackValue.ToString();
            rpm.text = curWeapon.rpm.ToString();
            ammo.text = curWeapon.maxAmmo.ToString();
        }

        //level.text = plData.level.ToString();
        shield.text = plData.MaxShield.ToString();
        hp.text = plData.MaxHp.ToString();
    }
}
