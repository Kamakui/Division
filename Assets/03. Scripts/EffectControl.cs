using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectControl : MonoBehaviour
{
    private void OnParticleSystemStopped()
    {
        ObjectPoolingManager.Instance.ReturnObj(this.gameObject);
    }
}
