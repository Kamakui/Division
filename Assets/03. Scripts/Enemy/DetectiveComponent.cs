using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectiveComponent : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private float distance;
    [SerializeField] private LayerMask target;
    public Transform targetObj;
    public bool isFind;
    public bool isAttack;

    void Update()
    {
        Collider[] coll = Physics.OverlapSphere(transform.position, radius, target);

        Debug.DrawLine(transform.position + Vector3.up, transform.forward * 10 + Vector3.up, Color.green);

        if (isFind = (coll.Length > 0))
        {
            targetObj = coll[0].transform;

            RaycastHit hit;

            if (Physics.Raycast(transform.position + Vector3.up, targetObj.position - transform.position, out hit, distance))
            {
                isAttack = 1 << hit.transform.gameObject.layer == target;
            }  
        }
    }
}
