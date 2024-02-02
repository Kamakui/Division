using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ConsumType
{ 
    HP,
    Shield,
    Ammo
}

public class ConsumItem : MonoBehaviour
{
    [SerializeField] private List<GameObject> modelList = new List<GameObject>();
    [SerializeField] private List<GameObject> effectList = new List<GameObject>();
    public ConsumType type;
    [SerializeField] private GameObject obj;
    [SerializeField] private GameObject effect;

    private void Start()
    {
        obj = modelList[(int)type];
        Instantiate(obj, this.transform);
        effect = effectList[(int)type];
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.TryGetComponent<PlayerData>(out PlayerData pldata))
        {
            switch(type)
            {
                case ConsumType.HP:
                    pldata.CurrentHp += 100;
                    break;
                case ConsumType.Shield:
                    pldata.CurrentShield += 100;
                    break;
                case ConsumType.Ammo:
                    pldata.HaveAmmo += 50;
                    break;
            }

            Instantiate(effect, other.transform);
            ObjectPoolingManager.Instance.ReturnObj(this.gameObject);
        }
    }
}
