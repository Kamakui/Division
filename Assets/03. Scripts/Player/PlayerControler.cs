using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    private PlayerData playerData;
    private Rigidbody rb;
    [SerializeField] private float moveSpeed;
    public Animator animator;
    [SerializeField] private GameObject inventoryUI;
    public bool isOpenInventory;
    [SerializeField] private GameObject Lhand;
    [SerializeField] private GameObject Rhand;
    [SerializeField] private GameObject playerModel;
    [SerializeField] private GameObject minimapUI;
    [SerializeField] private GameObject playerStateUI;
    [SerializeField] private AudioClip reloadClip;
    [SerializeField] private bool isReload;
    [SerializeField] private float reloadSpeed;

    public bool IsOpenInventory
    {
        get { return isOpenInventory; }
        set
        {
            isOpenInventory = value;
            inventoryUI.SetActive(isOpenInventory);
            minimapUI.SetActive(!isOpenInventory);
            playerStateUI.SetActive(!isOpenInventory);

            if (isOpenInventory)
                Cursor.lockState = CursorLockMode.None;
            else
                Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void Start()
    {
        playerData = GetComponent<PlayerData>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        IsOpenInventory = false;
        isReload = true;
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.isDead)
        {
            ResetAnimatorState();
            animator.SetTrigger("IsDead");
            return;
        }

        playerData.CurWeapon.transform.forward = Lhand.transform.position - Rhand.transform.position;

        if (!IsOpenInventory)
        {
            Move();

            animator.SetBool("IsAiming", Input.GetMouseButton(1));
            if (animator.GetBool("IsAiming") && isReload)
                Attack();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            IsOpenInventory = !IsOpenInventory;
        }
    }

    private void Move()
    {
        Vector3 move = Vector3.zero;
        Vector3 animatorMove = Vector3.zero;
        float velocityY = rb.velocity.y;

        if (Input.GetKey(KeyCode.W))
        {
            move += this.transform.forward;
            animatorMove += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            move -= this.transform.forward;
            animatorMove += Vector3.back;
        }
        if (Input.GetKey(KeyCode.D))
        {
            move += this.transform.right;
            animatorMove += Vector3.right;
        }
        if (Input.GetKey(KeyCode.A))
        {
            move -= this.transform.right;
            animatorMove += Vector3.left;
        }

        if (Input.GetKeyDown(KeyCode.R) && isReload)
        {
            if (playerData.CurWeapon.TryGetComponent<WeaponItem>(out WeaponItem weaponItem))
            {
                if (weaponItem.CurAmmo < weaponItem.maxAmmo)
                    StartCoroutine(Reload(weaponItem));
            }
        }

        animator.SetFloat("PosX", animatorMove.x);
        animator.SetFloat("PosY", animatorMove.z);

        animator.SetBool("IsAiming", Input.GetMouseButton(1));
        animator.SetBool("IsRunning", Input.GetKey(KeyCode.LeftShift));
        animator.SetBool("IsWalking", move != Vector3.zero);
            
        //move = move * moveSpeed * Time.deltaTime;
        //move.y = velocityY;
        //rb.velocity = move;
    }

    private void Attack()
    {
        if (playerData.CurWeapon.TryGetComponent<WeaponItem>(out WeaponItem weaponItem))
        {
            if (!weaponItem.Attack())
            {
                StartCoroutine(Reload(weaponItem));
            }      
        }
    }

    private IEnumerator Reload(WeaponItem weaponItem)
    {
        isReload = false;
        GameObject soundObj = ObjectPoolingManager.Instance.Pop("Sound");
        soundObj.GetComponent<SoundComponent>().Play(reloadClip, transform);
        PlayerInfoUI.Instance.reloadImage.fillAmount = 0.0f;
        PlayerInfoUI.Instance.reloadImage.gameObject.SetActive(true);

        while (PlayerInfoUI.Instance.reloadImage.fillAmount < 1)
        {
            PlayerInfoUI.Instance.reloadImage.fillAmount += reloadSpeed / 100.0f;
            yield return new WaitForSeconds(0.01f); 
        }

        PlayerInfoUI.Instance.reloadImage.gameObject.SetActive(false);

        if (playerData.HaveAmmo / weaponItem.maxAmmo > 0)
        {
            int needAmmo = weaponItem.maxAmmo - weaponItem.CurAmmo;
            playerData.HaveAmmo -= needAmmo;
            weaponItem.CurAmmo += needAmmo;
        }
        else
        {
            weaponItem.CurAmmo += playerData.HaveAmmo % weaponItem.maxAmmo;
            playerData.HaveAmmo = 0;
        }

        isReload = true;
    }

    public void ResetAnimatorState()
    {
        animator.SetBool("IsAiming", false);
        animator.SetBool("IsRunning", false);
        animator.SetBool("IsWalking", false);
        animator.SetFloat("PosX", 0);
        animator.SetFloat("PosY", 0);
    }
}
