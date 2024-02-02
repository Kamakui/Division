using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHpbar : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(GameManager.Instance.cameraObj.transform.position);
    }
}
