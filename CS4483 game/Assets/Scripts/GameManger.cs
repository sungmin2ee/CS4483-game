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
    public float maxGameTime = 200f;
    [Header("# Player Info")]
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 3,5, 10, 100, 150, 210, 280, 360, 450, 600 };

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

        if (player.isAlive)
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
