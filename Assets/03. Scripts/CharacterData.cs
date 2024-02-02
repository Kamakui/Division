using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData : MonoBehaviour
{
    [SerializeField] protected float maxHp;
    [SerializeField] protected float currentHp;
    [SerializeField] protected float maxShield;
    [SerializeField] protected float currentShield;
    [SerializeField] protected float atk;

    public float MaxHp
    {
        get { return maxHp; }
    }

    public float MaxShield
    {
        get { return maxShield; }
        set
        {
            maxShield = value;

            PlayerInfoUI.Instance.shiledBar.localScale = new Vector3(currentShield / maxShield, 1, 1);
        }
    }

    public virtual float CurrentHp
    {
        get { return currentHp; }
        set
        {
            currentHp = value;

            if (currentHp > maxHp)
                currentHp = maxHp;
            else if (currentHp < 0)
                currentHp = 0;

            if (currentHp <= 0)
                Die();
        }
    }

    public virtual float CurrentShield
    {
        get { return currentShield; }
        set
        {
            currentShield = value;

            if (currentShield > maxShield)
                currentShield = maxShield;
            else if (currentShield < 0)
            {
                CurrentHp += currentShield;
                currentShield = 0;
            }
        }
    }

    protected virtual void Die()
    {
         
    }
}
