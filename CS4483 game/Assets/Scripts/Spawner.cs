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

        if (timer > spawnData[level].spawnTime && GameManager.Instance.timeRemaining > 0)
        {
            timer = 0;
            Spawn();
        }
        else if (GameManager.Instance.timeRemaining <= 0)
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
