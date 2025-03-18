using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

    public static Player Instance;
    public float speed = 5f;
    private Vector2 movement;
    private Rigidbody2D rb;
    public Scanner scanner;
    private SpriteRenderer spriteRenderer;
    public bool isAlive;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        scanner = GetComponent<Scanner>();
    }
    void Start()
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
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        rb.linearVelocity = movement * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            spriteRenderer.color = Color.gray;
            Time.timeScale = 0f;
        }
    }
}