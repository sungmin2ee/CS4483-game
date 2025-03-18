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

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    void Start()
    {
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
        
        // logic of generating boss: boss will be generate at round 3
        if (!GameManager.Instance.pool.bossSpawned && timer > spawnData[level].spawnTime && GameManager.Instance.round == 3) 
        {
            
            GameManager.Instance.pool.bossSpawned = true; 
            SpawnBoss();
        }   

        // generate normal enemies
        if (timer > spawnData[level].spawnTime && GameManager.Instance.timeRemaining > 0)
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
        if (spawnPoint.Length <= 1)
        {
            return;
        }

        GameObject enemy = GameManager.Instance.pool.Get(0);
        int randomIndex = Random.Range(1, spawnPoint.Length);
        enemy.transform.position = spawnPoint[randomIndex].position;
        enemy.GetComponent<Enemy>().Init(spawnData[level]);
        spawnedEnemies.Add(enemy);
    }

    private void SpawnBoss()
    {
        if (spawnPoint.Length <= 1) return;

        GameObject boss = GameManager.Instance.pool.Get(3); 
        int randomIndex = Random.Range(1, spawnPoint.Length);
        boss.transform.position = spawnPoint[randomIndex].position;
        
        boss.GetComponent<EnemyBoss>().Init(new SpawnData
        {
            spawnTime = 0, 
            spriteType = 2, 
            health = 100, 
            speed = 2f
        });

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