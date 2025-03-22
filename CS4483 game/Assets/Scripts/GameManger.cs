using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [Header("# Player Input")]
    public Player player;
    public PoolManager pool;
    public LevelUp uiLevelUp;
    [Header("# Game Control")]
    public float gameTime = 0;
    public const float resetTime = 30; // change value later
    public float timeRemaining = resetTime; // change value later
    public float timeBetweenRounds; // hidden timer that gives the player a break after surviving a round
    public int round = 1;
    public bool isRoundActive = true; // when false (level up screen or round end) time stops
    public string[] roundScenes = { "Environment", "Round2", "Round3" }; // change this when the next scenes are committed
    //public float maxGameTime = 300f; // is this actually used?
    [Header("# Player Info")]
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 };

    private void Awake()
    {

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
        if(!player.isAlive) // if player dead
        {
            Debug.Log("Dead!!!!");
            
            // SceneManager.LoadScene("FailScene");
        }

        if (player.isAlive && isRoundActive) // player is alive (and running from monsters)
        {
            gameTime += Time.deltaTime;
            //gameTime = Mathf.Min(gameTime, maxGameTime);

            // note: if the following code gets too big, move it to a dedicated function
            // calculate the time remaining
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                // it might turn negative, so set it to 0 when that happens -- don't let the user see "time remaining: -0:01"
                timeRemaining = 0;

                // and give the user a 5 second break before the next round starts
                timeBetweenRounds = 5;
                isRoundActive = false;
            }
        }
        else if (player.isAlive && !isRoundActive && timeRemaining == 0)
        { // player survived the round
            //wait the five seconds
            if (timeBetweenRounds > 0)
            {
                timeBetweenRounds -= Time.deltaTime;
            }
            else
            {
                // increase the round, change the scene, and reset the time
                round++;
                timeRemaining = resetTime;   
                isRoundActive = true;
                SceneManager.LoadScene("Environment"); // temp code -- only used to check if it works
                //SceneManager.LoadScene(roundScenes[(round - 1)]); // round - 1 because arrays are 0-indexed
            } // else levelling up -- don't do anything
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
}