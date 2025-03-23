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
    public int health = 3;
    public float invulverable = 0;
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

        // if a user gets hit, they are invulnerable for a short time
        // this is to prevent the user from getting hit multiple times in a row
        // the following will turn them back to normal after a short time
        if (invulverable > 0) {
            invulverable -= Time.deltaTime;
            spriteRenderer.color = Color.gray;
        } else if (invulverable <= 0) {
            invulverable = 0;
            spriteRenderer.color = Color.white;
        }

    }

    void FixedUpdate()
    {
        rb.linearVelocity = movement * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // check if they can be hurt
        if (invulverable == 0)
        {
            // check if they are hit by an enemy and wouldn't die
            if (collision.gameObject.CompareTag("Enemy") && health > 1)
            {
                health--;
                bool heartflash = true; // dummy code
                invulverable = 3; // set them invulnerable for 3 seconds
            }
            // check if they are hit by an enemy and would die
            else if (collision.gameObject.CompareTag("Enemy") && health <= 1)
            {
                spriteRenderer.color = Color.gray;
                isAlive = false;
                // game over
                Time.timeScale = 0f;
            }
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

    public void ResetPlayer()
    {
        isAlive = true;
        health = 3; // from what we have decided? 
    }
}