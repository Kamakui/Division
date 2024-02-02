using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float atkValue;
    public float bulletSpeed;
    [SerializeField] float time;
    [SerializeField] bool isHit;
    [SerializeField] GameObject model;
    [SerializeField] GameObject bulletLine;
    private Rigidbody rb;
    private Coroutine coroutine;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        model.SetActive(true);
        bulletLine.SetActive(true);
        isHit = false;
        coroutine = StartCoroutine(BulletFire());
        rb.velocity = this.transform.forward * bulletSpeed;
    }

    private void Update()
    {
        this.GetComponent<Collider>().enabled = model.activeSelf;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isHit = other.GetComponent<Bullet>() == null && other.GetComponent<ItemObj>() == null && other.GetComponent<ConsumItem>() == null)
        {
            if(other.TryGetComponent<CharacterData>(out CharacterData characterData))
            {
                characterData.CurrentShield -= atkValue;
            }

            StopCoroutine(coroutine);
            StartCoroutine(BulletOff());
            GameObject effect = ObjectPoolingManager.Instance.Pop("HitEffect");
            effect.transform.forward = -this.transform.forward;
            effect.transform.position = this.transform.position - this.transform.forward;
            effect.SetActive(true);
        }
    }

    IEnumerator BulletFire()
    {
        yield return new WaitForSeconds(time);

        StartCoroutine(BulletOff());
    }

    IEnumerator BulletOff()
    {
        model.SetActive(false);

        yield return new WaitForSeconds(bulletLine.GetComponent<TrailRenderer>().time);

        bulletLine.GetComponent<TrailRenderer>().Clear();
        ObjectPoolingManager.Instance.ReturnObj(this.gameObject);
    }
}
