using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{
    [SerializeField] private List<WeaponItem> weaponItemList = new List<WeaponItem>();
    [SerializeField] private List<ArmorItem> armorItemList = new List<ArmorItem>();
    [SerializeField] private ConsumItem consumItem;
    [SerializeField] private int equipDropRate;
    [SerializeField] private int weaponDropRate;
    [SerializeField] private int ammoDropRate;
    private const int MAX_DROP_RATE = 100;

    private void Start()
    {
        if (equipDropRate > MAX_DROP_RATE)
            equipDropRate = MAX_DROP_RATE;
    }

    public void DropItem(GameObject target)
    {
        GameObject obj;

        if (Random.Range(0, MAX_DROP_RATE) < equipDropRate)
        {
            ItemObj itemObj = ObjectPoolingManager.Instance.Pop("ItemObj").GetComponent<ItemObj>();

            if (Random.Range(0, MAX_DROP_RATE) < weaponDropRate)
                itemObj.item = weaponItemList[Random.Range(0, weaponItemList.Count)];
            else
                itemObj.item = armorItemList[Random.Range(0, armorItemList.Count)];

            obj = itemObj.gameObject;
        }
        else
        {
            ConsumItem itemObj = ObjectPoolingManager.Instance.Pop("ConsumItem").GetComponent<ConsumItem>();

            if (Random.Range(0, MAX_DROP_RATE) < ammoDropRate)
                itemObj.type = ConsumType.Ammo;
            else
                itemObj.type = (ConsumType)Random.Range(0, 2);

            obj = itemObj.gameObject;
        }

        
        RaycastHit hit;
        if (Physics.Raycast(target.transform.position, Vector3.down, out hit))
        {
            obj.transform.position = hit.point + Vector3.up;
            obj.gameObject.SetActive(true);
        }
    }
}
