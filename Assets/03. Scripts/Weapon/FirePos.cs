using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePos : MonoBehaviour
{
    void FixedUpdate()
    {
        this.transform.LookAt(TargetPoint.Instance.transform);
    }
}
