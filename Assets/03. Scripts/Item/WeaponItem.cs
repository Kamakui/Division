using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Rifle,
    Shotgun,
    SniperRifle
}

public class WeaponData
{
    public Weapon weapon;

    public WeaponData(WeaponItem weaponItem)

    {
        switch(weaponItem.weaponType)
        {
            case WeaponType.Rifle:
                weapon = new Rifle();
                break;
            case WeaponType.Shotgun:
                weapon = new Shotgun();
                break;
            case WeaponType.SniperRifle:
                weapon = new SniperRifle();
                break;
        }

        weapon.weaponItem = weaponItem;
        weapon.isAttackable = true;
    }
}

public abstract class Weapon
{
    public WeaponItem weaponItem;
    public bool isAttackable;

    public abstract void Attack();

    protected void BulletInstantiate()
    {
        GameObject bulletObj = ObjectPoolingManager.Instance.Pop("Bullet");
        bulletObj.transform.position = weaponItem.firePos.transform.position;
        bulletObj.transform.rotation = weaponItem.firePos.transform.rotation;
        bulletObj.transform.Rotate(Random.Range(-weaponItem.applyAccuracyRate, weaponItem.applyAccuracyRate), Random.Range(-weaponItem.applyAccuracyRate, weaponItem.applyAccuracyRate), 0);
        bulletObj.GetComponent<Bullet>().atkValue = weaponItem.attackValue;
        bulletObj.SetActive(true);
        GameObject effect = ObjectPoolingManager.Instance.Pop("FIreEffect");
        effect.transform.position = weaponItem.firePos.transform.position;
        effect.transform.rotation = weaponItem.firePos.transform.rotation;
        effect.SetActive(true);
    }

    protected void AttackProcessing()
    {
        weaponItem.StartCoroutine(weaponItem.AttackCoolTime());
        isAttackable = false;
        weaponItem.CurAmmo--;
        weaponItem.animator.SetTrigger("AttackTrigger");
        GameObject soundObj = ObjectPoolingManager.Instance.Pop("Sound");
        soundObj.GetComponent<SoundComponent>().Play(weaponItem.clip, weaponItem.transform);
    }
}

public class Rifle : Weapon
{
    public override void Attack()
    {
        if (Input.GetMouseButton(0) && isAttackable)
        {
            AttackProcessing();
            BulletInstantiate();
        }
    }
}

public class Shotgun : Weapon
{
    private const int shotBulletCount = 8;
    public override void Attack()
    {
        if (Input.GetMouseButtonDown(0) && isAttackable)
        {
            AttackProcessing();
            for (int count = 0; count < shotBulletCount; count++)
            {
                BulletInstantiate();
            }
        }
    }
}
public class SniperRifle : Weapon
{
    public override void Attack()
    {
        if (Input.GetMouseButtonDown(0) && isAttackable)
        {
            AttackProcessing();
            BulletInstantiate();
        }
    }
}

public class WeaponItem : Item
{
    public int attackValue;
    public int maxAmmo;
    [SerializeField] private int curAmmo;
    public float rpm;
    [SerializeField] private float accuracyRate;
    public float applyAccuracyRate;
    public WeaponType weaponType;
    public WeaponData weaponData;
    public GameObject firePos;
    public Animator animator;
    public AudioClip clip;

    public int CurAmmo
    {
        get { return curAmmo; }
        set
        {
            curAmmo = value;
            PlayerInfoUI.Instance.currentAmmo.text = curAmmo.ToString();
        }
    }

    private void Awake()
    {
        weaponData = new WeaponData(this);
        applyAccuracyRate = 10 * (1.0f - accuracyRate / 100.0f);
    }

    public bool Attack()
    {
        if (curAmmo <= 0)
            return false;

        weaponData.weapon.Attack();
        return true;
    }

    public IEnumerator AttackCoolTime()
    {        
        yield return new WaitForSeconds(60.0f / rpm);
        weaponData.weapon.isAttackable = true;
    }
}
