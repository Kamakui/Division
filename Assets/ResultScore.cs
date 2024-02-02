using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI score;

    private void OnEnable()
    {
        score.text = GameManager.Instance.Score.ToString();
    }
}
