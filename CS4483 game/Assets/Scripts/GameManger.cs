using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static MapChanger Map;
    [Header("# Player Input")]
    public Player player;
    public PoolManager pool;
    public LevelUp uiLevelUp;
    [Header("# Game Control")]
    public float gameTime = 0;
    public const float resetTime = 30; // how many seconds in a round
    public float timeRemaining = resetTime; // change value later
    public float timeBetweenRounds; // hidden timer that gives the player a break after surviving a round
    public int round = 1;
    public bool isRoundActive = true; // when false (level up screen or round end) time stops
    public string[] roundScenes = { "Environment", "Round2", "Round3" }; // change this when the next scenes are committed
    [Header("# Player Info")]
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 };


     void Start()
    {
        resetAll();
    }

    private void Awake()
    {
        if (Map == null)
        {
            Map = FindFirstObjectByType<MapChanger>();
        }

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
        // check if the player character exists
        if (player == null)
        {
            Debug.LogError("GameManager: Player is null! Make sure Player is assigned in the Inspector.");
            return;
        }

        // check if the round is active
        if (isRoundActive == true)
        {
            // check if the player is alive
            if (player.isAlive == true) {
                // player is alive and the round is active; continue

                gameTime += Time.deltaTime; // globlal game time
                if (timeRemaining > 0) { // round time
                    timeRemaining -= Time.deltaTime;
                } else { // if timeRemaining is 0, the round is over

                    // it might turn negative, so set it to 0 when that happens -- don't let the user see "time remaining: -0:01"
                    timeRemaining = 0;
                    timeBetweenRounds = 3; // 3 sec break between rounds
                    isRoundActive = false; 
                }
            } else { // player is dead; stop the game
                timeBetweenRounds = 3; // 3 sec break to show the game over message
                isRoundActive = false;
            }
        } else { // round is not active
            // check if they are in the break between rounds
            if (timeBetweenRounds > 0) {
                timeBetweenRounds -= Time.deltaTime;
            } else { // break is over

                //check if they died, completed the game, or are ready for the next round
                if (player.isAlive == false) // game over
                {
                    Debug.Log("Dead!!!! Loading FailScene");
                    ClearPersistentObjects();
                    SceneManager.LoadScene("FailScene");    // switch to fail scene if player dead
                    return;
                }

                if (round == 3 && timeRemaining == 0) { // victory condition
                    // we should probably have a "roundMax" var for this
                    Debug.Log("All rounds cleared! Loading SuccessScene...");
                    ClearPersistentObjects();
                    SceneManager.LoadScene("SuccessScene");
                    return;
                }

                if (timeRemaining == 0) { // completed a round

                    round++;
                    timeRemaining = resetTime;
                    isRoundActive = true;
                    //SceneManager.LoadScene("Environment"); // would be changed when we get more levels/maps/scenes to play in
                    //SceneManager.LoadScene(roundScenes[(round - 1)]); // round - 1 because arrays are 0-indexed
                    Map.LoadRoundMap(); // change the map
                }

                // else the player is in a level up screen
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
            uiLevelUp.Show();
        }
    }

    // wipe out all the dontDestroy elements in the scene, making sure it wont be bring to next scene
    public void ClearPersistentObjects()
    {
        Debug.Log("Clearing persistent objects before scene change...");

        if (player != null)
            Destroy(player.gameObject);

        if (pool != null)
            Destroy(pool.gameObject);

        Destroy(gameObject);
    }
    
    // reset game if wins
    private void resetAll() 
    {
        Debug.Log("Resetting all game state...");

        // Reset core gameplay state
        gameTime = 0;
        round = 1;
        timeRemaining = resetTime;
        timeBetweenRounds = 0f;
        isRoundActive = true;

        // Reset player progression
        kill = 0;
        exp = 0;
        level = 0;
        

        // Reset player status if still in scene
        if (player != null)
        {
            player.ResetPlayer(); 
        }
        }

}