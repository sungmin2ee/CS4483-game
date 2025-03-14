using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [Header("# Player Input")]
    public Player player;
    public PoolManager pool;
    [Header("# Game Control")]
    public float gameTime;
    public float timeRemaining = 30; // change value later
    public int round = 1;
    public bool isRoundActive = true; // when false (in shop or level up screen) time stops
    public float maxGameTime = 200f;
    [Header("# Player Info")]
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 3,5, 10, 100, 150, 210, 280, 360, 450, 600 };

    private void Awake()
    {
        // ½Ì±ÛÅæ ÆÐÅÏ Àû¿ë
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  
        }
        else
        {
            Destroy(gameObject);  
        }
    }

    void Update()
    {
        if (player == null)
        {
            Debug.LogError("GameManager: Player is null! Make sure Player is assigned in the Inspector.");
            return;
        }

        if (player.isAlive && isRoundActive)
        {
            gameTime += Time.deltaTime;
            gameTime = Mathf.Min(gameTime, maxGameTime);

            // note: if the following code gets too big, move it to a dedicated function
            // calculate the time remaining
            if (timeRemaining > 0) {
                timeRemaining -= Time.deltaTime;
            } else {
                // it might turn negative, so set it to 0 when that happens
                timeRemaining = 0;

                // round ended, clear all enemies

                // Spawner.ClearSpawnedEnemies();
                /* does not work -- compiler hates that Spawner is not static
                 * either Spawner needs to call ClearSpawnedEnemies()
                 * or GameManager needs to somehow inherit from Spawner
                 * so for now, just throw up a "Round Cleared!" and set the timeRemaining to back to 30
                */

                round++;
                bool inShop = true;
                // dummy code -- replace with an actual method call for a shop or something (and pause the game)
                // for now, just set inShop to false and reset the timeRemaining
                inShop = false;

                timeRemaining = 30;
            }
        }
        
    }
    public void GetExp()
    {
        exp++;
        if (exp >= nextExp[level])
        {
            level++;
            exp = 0;

        }
    }
}
