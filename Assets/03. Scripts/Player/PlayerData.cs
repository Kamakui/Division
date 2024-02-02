using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerData : CharacterData
{
    [SerializeField] private const int MAX_AMMO = 800;
    [SerializeField] private float criProbablity;
    [SerializeField] private float criDamage;
    [SerializeField] private float headDamage;
    [SerializeField] private int haveAmmo;
    private Animator animator;
    [SerializeField] private GameObject curWeapon = null;

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

            PlayerInfoUI.Instance.hpBar.localScale = new Vector3(currentHp / maxHp, 1, 1);

            if (currentHp <= 0)
                Die();
        }
    }

    public override float CurrentShield
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

            PlayerInfoUI.Instance.shiledBar.localScale = new Vector3(currentShield / maxShield, 1, 1);
        }
    }

    public int HaveAmmo
    {
        get { return haveAmmo; }
        set
        {
            haveAmmo = value;

            if (haveAmmo > MAX_AMMO)
                haveAmmo = MAX_AMMO;

            PlayerInfoUI.Instance.haveAmmo.text = "/ " + haveAmmo;
        }
    }

    public GameObject CurWeapon
    {
        get { return curWeapon; }

        set 
        {
            if (CurWeapon != null)
                CurWeapon.SetActive(false);

            curWeapon = value;
            curWeapon.SetActive(true);
            curWeapon.GetComponent<WeaponItem>().animator = animator;
            curWeapon.GetComponent<WeaponItem>().weaponData.weapon.isAttackable = true;
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        Application.targetFrameRate = 50;
    }

    private void Start()
    {
        PlayerInfoUI.Instance.haveAmmo.text = "/ " + haveAmmo;
        PlayerInfoUI.Instance.currentAmmo.text = CurWeapon.GetComponent<WeaponItem>().CurAmmo.ToString();
    }

    protected override void Die()
    {
        GameManager.Instance.isDead = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
