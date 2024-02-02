using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    void Update()
    {
        Debug.DrawLine(transform.position, transform.forward * 100, Color.red);
    }
}
