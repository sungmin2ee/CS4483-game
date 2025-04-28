using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawnData;
    float timer;
    int level;
    float spawnInterval = 0.5f;
    private List<GameObject> spawnedEnemies = new List<GameObject>();

    private Collider2D spawnBound;    // used for making sure the enemy is spawning within the camera confiner

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    void Start()
    {
        spawnBound = GameObject.FindWithTag("SpawnConfiner").GetComponent<Collider2D>();
        if (spawnData == null || spawnData.Length == 0)
        {

        }
    }

    void Update()
    {
        if (GameManager.Instance == null)
        {

            return;
        }

        timer += Time.deltaTime;
        level = Mathf.Clamp(Mathf.FloorToInt(GameManager.Instance.gameTime / 10f), 0, spawnData.Length - 1);
        // Debug.Log("Boss Spawned: " + GameManager.Instance.pool.bossSpawned);    
        float dynamicSpawnTime = Mathf.Max(0.2f, spawnData[level].spawnTime - level * 0.04f);

        // logic of generating boss: boss will be generate at round 3
        if (!GameManager.Instance.pool.bossSpawned && timer > spawnData[level].spawnTime && GameManager.Instance.round == 3) 
        {
            
            GameManager.Instance.pool.bossSpawned = true; 
            SpawnBoss();
        }

        // generate normal enemies
        if (timer > dynamicSpawnTime && GameManager.Instance.timeRemaining > 0)
        {
            timer = 0;
            Spawn();
        }




        if (GameManager.Instance.timeRemaining <= 0)
        {
            ClearSpawnedEnemies();
        }
    }

    private void Spawn()
    {
        if (spawnPoint.Length <= 1 || spawnBound == null) return;

        int enemyCount = Mathf.Clamp(level + 1, 1, 10); 

        for (int i = 0; i < enemyCount; i++)
        {
            int randomIndex = Random.Range(1, spawnPoint.Length);
            Vector2 spawnPos = spawnPoint[randomIndex].position;

            if (!spawnBound.OverlapPoint(spawnPos))
            {
                spawnPos = spawnBound.ClosestPoint(spawnPos);
            }

            GameObject enemy = GameManager.Instance.pool.Get(0); 
            enemy.transform.position = spawnPos;
            enemy.GetComponent<Enemy>().Init(spawnData[level]);
            spawnedEnemies.Add(enemy);
        }
    }



    private void SpawnBoss()
    {
        if (spawnPoint.Length <= 1 || spawnBound == null) return;

        int randomIndex = Random.Range(1, spawnPoint.Length);
        Vector2 spawnPos = spawnPoint[randomIndex].position;

        // check if spawn point is in the bounds
        if (!spawnBound.OverlapPoint(spawnPos))
        {
            // Debug.Log("Spawner tried to spawn outside CameraConfiner, adjusting...");
            spawnPos = spawnBound.ClosestPoint(spawnPos);
        }

        GameObject boss = GameManager.Instance.pool.Get(3);
        boss.transform.position = spawnPos;
       /* boss.GetComponent<EnemyBoss>().Init(new SpawnData
        {
            spawnTime = 0, 
            spriteType = 2, 
            health = 100, 
            speed = 2f
        });*/
        spawnedEnemies.Add(boss);
    }

    public void ClearSpawnedEnemies()
    {
        foreach (GameObject enemy in spawnedEnemies)
        {
            if (enemy != null)
            {
                GameManager.Instance.pool.ReturnToPool(enemy);
            }
        }
        spawnedEnemies.Clear();
    }
}

[System.Serializable]
public class SpawnData
{
    public float spawnTime;
    public int spriteType;
    public int health;
    public float speed;
}