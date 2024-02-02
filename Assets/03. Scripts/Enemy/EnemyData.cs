using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyData : CharacterData
{
    [SerializeField] private RectTransform hpBar;

    public override float CurrentHp
    {
        get { return currentHp; }
        set
        {
            currentHp = value;

            if (currentHp > maxHp)
                currentHp = maxHp;
            else if (currentHp < 0)
                currentHp = 0;

            hpBar.localScale = new Vector3(currentHp / maxHp, 1, 1);
        }
    }

    public float Atk
    {
        get { return atk; }
    }

    private void OnEnable()
    {
        Init();
    }

    private void Update()
    {
        if(currentHp <= 0)
        {
            ItemManager.Instance.DropItem(gameObject);
            GameManager.Instance.Score++;
            ObjectPoolingManager.Instance.ReturnObj(this.gameObject);
        }
    }

    void Init()
    {
        CurrentHp = maxHp;
        CurrentShield = maxShield;
    }
}
