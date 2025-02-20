using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;  

    public Player player;
    public PoolManager pool;
    public float gameTime;
    public float maxGameTime = 20f;

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

        if (player.isAlive)
        {
            gameTime += Time.deltaTime;
            gameTime = Mathf.Min(gameTime, maxGameTime);
        }
    }
}
