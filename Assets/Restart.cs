using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour, IPointerClickHandler
{
    public AudioClip clip;
    public void OnPointerClick(PointerEventData eventData)
    {
        SceneManager.LoadScene("MainScene");

        GameObject soundObj = ObjectPoolingManager.Instance.Pop("Sound");
        soundObj.GetComponent<SoundComponent>().Play(clip, transform);
    }
}
