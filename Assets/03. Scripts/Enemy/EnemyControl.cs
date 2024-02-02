using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControl : MonoBehaviour
{
    [SerializeField] private Transform firePos;
    [SerializeField] private Animation anime;
    private DetectiveComponent dec;
    private EnemyData enemyData;
    public NavMeshAgent nav;
    public GameObject targetObj;
    [SerializeField] private AudioClip shootClip;

    private void Awake()
    {
        dec = GetComponent<DetectiveComponent>();
        nav = GetComponent<NavMeshAgent>();
        enemyData = GetComponent<EnemyData>();
    }

    private void OnEnable()
    {
        if (targetObj != null)
        {
            StartCoroutine(ShootCountine());
            nav.enabled = true;
        }
    }

    IEnumerator ShootCountine()
    {
        while (!GameManager.Instance.isDead)
        {
            if(nav.enabled)
                nav.SetDestination(targetObj.transform.position);

            if (dec.isFind)
            {
                transform.forward = targetObj.transform.position - transform.position;
                anime.Play("Left Aim");

                if (dec.isAttack)
                {
                    nav.enabled = false;
                    Shoot();
                    anime.Play("Left Blast Attack");

                    yield return new WaitForSeconds(0.5f);
                }
                else
                    nav.enabled = true;
            }
            else
            {
                nav.enabled = true;
                anime.Play("Move");
            }
            yield return null;
        }

        nav.enabled = false;
    }

    void Shoot()
    {
        firePos.forward = dec.targetObj.position - firePos.transform.position;
        Bullet bullet = ObjectPoolingManager.Instance.Pop("Bullet").GetComponent<Bullet>();
        bullet.atkValue = enemyData.Atk;
        bullet.transform.position = firePos.position;
        bullet.transform.forward = firePos.forward;
        bullet.gameObject.SetActive(true);
        GameObject soundObj = ObjectPoolingManager.Instance.Pop("Sound");
        soundObj.GetComponent<SoundComponent>().Play(shootClip, transform);
    }

}
