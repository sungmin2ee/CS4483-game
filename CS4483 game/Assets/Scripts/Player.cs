using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

    public static Player Instance;
    public float speed = 3f;
    private Vector2 movement;
    private Rigidbody2D rb;
    public Scanner scanner;
    private SpriteRenderer spriteRenderer;
    public bool isAlive;
    public float attackRange = 2;
    public int attackDamage = 3;
    private float attackCooldown = 1f;
    private float lastAttackTime = 0f; 
    private Vector2 lastMoveDirection = Vector2.right;
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

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
            Debug.Log("attack");
        }
        if (movement != Vector2.zero) 
        {
            lastMoveDirection = movement.normalized;
        }
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
            //Time.timeScale = 0f;
        }
    }
    void Attack()
    {
        if (Time.time - lastAttackTime < attackCooldown) 
        {
            Debug.Log("cooldown");
            return;
        }

        lastAttackTime = Time.time; 

        Vector2 attackPosition = (Vector2)transform.position + lastMoveDirection * attackRange;

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPosition, attackRange);
        foreach (Collider2D enemy in hitEnemies)
        {
            Enemy enemyController = enemy.GetComponent<Enemy>();
            if (enemyController != null)
            {
                enemyController.changeHealth(-attackDamage); 
                
            }
        }
    }
}