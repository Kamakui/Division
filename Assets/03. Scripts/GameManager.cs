using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : Singleton<GameManager>
{
    public GameObject playerObj;
    public GameObject cameraObj;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;
    public GameObject resultObj;
    private int score;
    public bool isDead;
    public List<GameObject> spawnPoints = new List<GameObject>();
    [SerializeField] private float delayTime;
    [SerializeField] private float spawnTime;

    public int Score
    {
        get { return score; }
        set
        {
            if (!isDead)
                score = value;

            scoreText.text = score.ToString();
        }
    }

    public float DelayTime
    {
        get { return delayTime; }
        set
        {
            delayTime = value;

            timeText.text = delayTime.ToString();
            if (delayTime <= 0)
            {
                timeText.gameObject.SetActive(false);
                scoreText.gameObject.SetActive(true);
            }
        }
    }

    private void Start()
    {
        Score = 0;
        DelayTime = DelayTime;
        isDead = false;
        resultObj.SetActive(false);
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while(delayTime > 0)
        {
            yield return new WaitForSeconds(1.0f);
            DelayTime--;
        }

        while (!isDead && spawnTime > 0)
        {
            yield return new WaitForSeconds(spawnTime);

            GameObject enemy = ObjectPoolingManager.Instance.Pop("Enemy");
            enemy.transform.position = spawnPoints[Random.Range(0, spawnPoints.Count)].transform.position;
            enemy.GetComponent<EnemyControl>().targetObj = playerObj;
            enemy.SetActive(true);

            if(spawnTime > 0.5f)
            spawnTime -= 0.01f;
        }

        resultObj.SetActive(true);
    }
}
