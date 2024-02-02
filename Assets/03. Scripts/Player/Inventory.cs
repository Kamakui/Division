using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : Singleton<Inventory>
{
    [SerializeField] private PlayerData playerData;
    public List<Item> itemList = new List<Item>();
    public WeaponSlot[] weaponSlot = new WeaponSlot[3];
    public ArmorSlot[] armorSlot = new ArmorSlot[6];
    public GameObject handle;

    private void Awake()
    {
        base.Awake();
        weaponSlot[0].item = itemList[0];
        weaponSlot[0].weaponObj = Instantiate(weaponSlot[0].item.gameObject, handle.transform);
        playerData.CurWeapon = weaponSlot[0].weaponObj;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && weaponSlot[0].item != null)
            ChangeWeapon(0);
        else if (Input.GetKeyDown(KeyCode.Alpha2) && weaponSlot[1].item != null)
            ChangeWeapon(1);
        else if (Input.GetKeyDown(KeyCode.Alpha3) && weaponSlot[2].item != null)
            ChangeWeapon(2);
    }

    private void ChangeWeapon(int index)
    {
        for(int count = 0; count < weaponSlot.Length; count++)
        {
            if (weaponSlot[count].weaponObj != null)
                weaponSlot[count].weaponObj.SetActive(false);
        }

        playerData.CurWeapon = weaponSlot[index].weaponObj;
        PlayerInfoUI.Instance.currentAmmo.text = playerData.CurWeapon.GetComponent<WeaponItem>().CurAmmo.ToString();
    }

    public void ApplyArmor()
    {
        float shieldValue = 100.0f;

        for (int count = 0; count < armorSlot.Length; count++)
        {
            if (armorSlot[count].item != null)
                shieldValue += ((ArmorItem)(armorSlot[count].item)).shieldValue;
        }

        playerData.MaxShield = shieldValue;
    }
}
