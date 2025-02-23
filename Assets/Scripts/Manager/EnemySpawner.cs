using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(EnemySpawner_UIs))]
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Wave[] waves;
    [SerializeField] Transform[] spawnPoints;
    EnemySpawner_UIs UIs;
    int enemyCount = 0;
    public int enemyLeft { get { return enemyCount; } }
    public Wave currentWave
    {
        get { return waves[index]; }
    }
    public int currentWaveNum
    {
        get { return index; }
    }
    public bool lastWave
    {
        get { return index == waves.Length - 1; }
    }
    int index = 0;
    bool spawnEnded = false;
    public bool resting = false;
    public float restTimer;

    float counter = 0.0f;
    private void Awake()
    {
        UIs = GetComponent<EnemySpawner_UIs>();
        GameManager.Instance.onGameOver.AddListener(delegate { gameObject.SetActive(false); });
    }
    private void Update()
    {
        if (resting)
        {
            UIs.OnUpdate();
            return;
        }
        if (!spawnEnded)
        {
            if(counter < currentWave.spawnRate)
            {
                counter += Time.deltaTime;
            }
            else
            {
                counter = 0.0f;
                if(currentWave.enemies.Count > 0)
                {
                    int tmp = Random.Range(0, currentWave.enemies.Count);
                    currentWave.enemies[tmp].remaining--;
                    Spawn(currentWave.enemies[tmp].enemy);
                    if (currentWave.enemies[tmp].remaining == 0) currentWave.enemies.RemoveAt(tmp);
                }
                else
                {
                    if (currentWave.boss != null) Spawn(currentWave.boss.gameObject);
                    spawnEnded = true;
                }
            }
        }
        else
        {
            if(enemyCount == 0)
            {
                resting = true;
                NextWave();
            }
        }
        UIs.OnUpdate();
    }
    public UnityEvent onNextWave = new UnityEvent();
    void NextWave()
    {
        GameManager.Instance.EarnMoney(currentWave.waveReward, "Wave Reward");
        if(index==waves.Length - 1)
        {
            UIs.End();
            GameManager.Instance.GameWin();
            gameObject.SetActive(false);
            return;
        }
        onNextWave.Invoke();
        StartCoroutine(WaveWait());
    }
    IEnumerator WaveWait()
    {
        restTimer = Mathf.Max(currentWave.restTime, 3.0f);
        while(restTimer > 0)
        {
            restTimer -= Time.deltaTime;
            if (restTimer < 0) restTimer = 0;
            yield return null;
        }
        spawnEnded = false;
        resting = false;
        index++;
    }
    void Spawn(GameObject enemy)
    {
        Instantiate(enemy, spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);
    }
    public void EnemyAdd()
    {
        enemyCount++;
    }
    public void EnemyRemove()
    {
        enemyCount--;
    }
}
[System.Serializable] public class Wave
{
    public List<Enemy_Left> enemies;
    public Enemy boss;
    public float spawnRate;
    public float restTime;
    public int waveReward;
}
[System.Serializable] public class Enemy_Left
{
    public GameObject enemy;
    public int remaining;
}
