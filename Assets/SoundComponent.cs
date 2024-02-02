using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundComponent : MonoBehaviour
{
    public AudioSource audioSource;

    private void Update()
    {
        if(!audioSource.isPlaying)
            ObjectPoolingManager.Instance.ReturnObj(this.gameObject);
    }

    public void Play(AudioClip clip, Transform target)
    {
        audioSource.clip = clip;
        this.transform.position = target.position;
        gameObject.SetActive(true);
        audioSource.Play();
    }
}
