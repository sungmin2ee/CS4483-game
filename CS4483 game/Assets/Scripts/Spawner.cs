
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

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager.Instance is null! Make sure GameManager exists in the scene.");
            return;
        }

        timer += Time.deltaTime;
        level = Mathf.Min(Mathf.FloorToInt(GameManager.Instance.gameTime / 10f),spawnData.Length -1);

        if (timer > spawnData[level].spawnTime && GameManager.Instance.timeRemaining > 0)
        {
            timer = 0;
            Spawn();
        }
    }

    private void Spawn()
    {
        GameObject enemy = GameManager.Instance.pool.Get(0);
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
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
