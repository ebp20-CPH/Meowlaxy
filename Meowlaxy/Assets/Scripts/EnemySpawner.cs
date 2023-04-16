using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject EnemyPrefabA;
    [SerializeField] GameObject EnemyPrefabB;
    [SerializeField] GameObject BossPrefab;

    bool isTicking = false;

    enum SpawnMode { Single, Burst, Constant, None, Boss };
    SpawnMode mode = SpawnMode.None;

    float spawnDelta = 20f;

    float totalTimer = 0f;

    float spawnTimer = 0f;
    float spawnCooldown = 1f;
    float enemyValue = 0f;

    float diffScale = 0f;

    int timingIndex = 0;
    float timer = 0f;
    float[] timings = { 3f, 15f, 15f, 15f };

    private void Start()
    {
        //SpawnEnemy();

        //SpawnBoss();

        mode = SpawnMode.None;

        timer = timings[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (isTicking) 
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
                UpdateTimer();

            spawnTimer -= Time.deltaTime;
            if (spawnTimer <= 0)
            {
                SpawnEnemy();
                spawnTimer = spawnCooldown;
            }
        }

        totalTimer += Time.deltaTime;
    }

    private void UpdateTimer() 
    {
        timingIndex++;
        if (timingIndex < timings.Length)
        {
            timer = timings[timingIndex];

            if (timingIndex == 1)
            {
                mode = SpawnMode.Single;
            }
            else if (timingIndex == 2)
            {
                spawnCooldown = 2f;
                mode = SpawnMode.Burst;
            }
            else if (timingIndex == 3)
            {
                mode = SpawnMode.Constant;
                spawnCooldown = .5f;
            }
            else if (timingIndex == 4) 
            {
                SpawnBoss();
                mode = SpawnMode.Boss;
            }
        }
        else 
        {
            isTicking = false;
        }
    }

    private void SpawnEnemy() 
    {
        Vector2 rv = GetRandomPosition();
        GameObject en;

        if (mode == SpawnMode.Single)
        {
            rv = GetRandomPosition();
            en = RollEnemy();
            en.transform.position = new Vector2(rv.x * spawnDelta, rv.y * spawnDelta);
        }
        else if (mode == SpawnMode.Burst)
        {
            for (int i = 0; i < 3; i++) 
            {
                rv = GetRandomPosition();
                en = RollEnemy();
                en.transform.position = new Vector2(rv.x * spawnDelta, rv.y * spawnDelta);
            }
        }
        else if (mode == SpawnMode.Constant)
        {
            rv = GetRandomPosition();
            en = RollEnemy();
            en.transform.position = new Vector2(rv.x * spawnDelta, rv.y * spawnDelta);
        }
        else if (mode == SpawnMode.Boss)
        {

        }
    }

    private GameObject RollEnemy() 
    {
        GameObject g;

        float rv = Random.Range(0f, 1f);
        if (rv > .65f && timingIndex > 1)
        {
            g = Instantiate(EnemyPrefabB);
        }
        else 
        {
            g = Instantiate(EnemyPrefabA);
        }

        return g;
    }

    private void SpawnBoss() 
    {
        GameObject g = Instantiate(BossPrefab);
        g.transform.position = new Vector2(0, 15);
    }

    private Vector2 GetRandomPosition() 
    {
        Vector2 v = new Vector2();
        float r = Random.Range(0, 2f);

        v.x = Mathf.Sin(r * Mathf.PI);
        v.y = Mathf.Cos(r * Mathf.PI);

        return v;
    }

    public void SetIsTicking(bool isVal) 
    {
        isTicking = isVal;
    }
}
