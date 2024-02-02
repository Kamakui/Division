using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PoolingData
{
    public GameObject prefab;
    public int size;
    public GameObject parentObj;
}

public class ObjectPoolingManager : Singleton<ObjectPoolingManager>
{
    public List<PoolingData> poolingList = new List<PoolingData>();
    public GameObject newParentObj;

    public Dictionary<string, Queue<GameObject>> poolingDic = new Dictionary<string, Queue<GameObject>>();
    public Dictionary<string, PoolingData> dataDic = new Dictionary<string, PoolingData>();

    private void Start()
    {
        foreach (PoolingData data in poolingList)
        {
            dataDic.Add(data.prefab.name, data);

            if (data.parentObj == null)
            {
                data.parentObj = Instantiate(newParentObj, this.transform);
                data.parentObj.name = data.prefab.name + " Pool";
            }

            poolingDic.Add(data.prefab.name, new Queue<GameObject>());
            Init(data, data.size);
        }
    }

    public void Init(PoolingData data, int size)
    {
        for (int i = 0; i < size; i++)
        {
            GameObject obj = Instantiate(data.prefab, data.parentObj.transform);
            obj.name = data.prefab.name;
            obj.SetActive(false);

            if (obj.GetComponent<WeaponSlot>() != null)
                WeaponListUI.Instance.weaponList.Add(obj.GetComponent<WeaponSlot>());
            if (obj.GetComponent<ArmorSlot>() != null)
                ArmorListUI.Instance.armorList.Add(obj.GetComponent<ArmorSlot>());
            else
                poolingDic[data.prefab.name].Enqueue(obj);
        }
    }

    public GameObject Pop(string objName)
    {
        if(poolingDic[objName].Count == 0)
        {
            Init(dataDic[objName], dataDic[objName].size / 3);
        }

        return poolingDic[objName].Dequeue();
    }

    public void ReturnObj(GameObject obj)
    {
        obj.SetActive(false);
        poolingDic[obj.name].Enqueue(obj);
    }
}
