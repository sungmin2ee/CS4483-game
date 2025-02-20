using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    float timer;
    int level;
    float spawnInterval = 0.5f;
    private List<GameObject> spawnedEnemies = new List<GameObject>();

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager.Instance is null! Make sure GameManager exists in the scene.");
            return;
        }

        timer += Time.deltaTime;
        level = Mathf.FloorToInt(GameManager.Instance.gameTime / 10f);

        // 시간 경과에 따라 spawnInterval 감소 (지수적으로 감소하도록 변경)
        spawnInterval = Mathf.Max(0.1f, 0.5f * Mathf.Pow(0.95f, GameManager.Instance.gameTime));

        if (timer > spawnInterval)
        {
            timer = 0;
            Spawn();
        }
    }

    private void Spawn()
    {
        if (GameManager.Instance.pool == null)
        {
            Debug.LogError("PoolManager is null! Make sure it is assigned in GameManager.");
            return;
        }

        GameObject enemy = GameManager.Instance.pool.Get(Random.Range(0, 2));
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
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
