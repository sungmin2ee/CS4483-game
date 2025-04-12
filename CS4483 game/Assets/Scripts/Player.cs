using UnityEngine;
using System.Collections;
using Unity.VisualScripting.InputSystem;


public class Player : MonoBehaviour
{

    public static Player Instance;
    public float speed = 3.25f; // 3 feels slightly too slow?
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator animator;
    private AudioSource musicSource; 
    private AudioSource effectsSource; 
    public Scanner scanner;
    private SpriteRenderer spriteRenderer;
    public bool isAlive;
    public int maxHealth = 3;
    public int currHealth = 3;
    public float invulverable = 0;
    public float attackRange = 2;
    public int attackDamage = 3;
    private float attackCooldown = 0.5f;
    private float lastAttackTime = 0f; 
    private Vector2 lastMoveDirection = Vector2.right;
    public GameObject attackEffectPrefab;
    public AudioClip attackSound;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        scanner = GetComponent<Scanner>();
        animator = GetComponent<Animator>();
        musicSource = GetComponent<AudioSource>();
        musicSource.Play();
        effectsSource = gameObject.AddComponent<AudioSource>();
        effectsSource.playOnAwake = false;
        effectsSource.volume = 0.4f;
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
            Debug.Log("attack");
        }
        if (movement != Vector2.zero)
        {
            lastMoveDirection = movement.normalized;
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }


        // if a user gets hit, they are invulnerable for a short time
        // this is to prevent the user from getting hit multiple times in a row
        // the following will turn them back to normal after a short time
        if (invulverable > 0) {
            invulverable -= Time.deltaTime;
            spriteRenderer.color = Color.gray;
        }
        else if (invulverable <= 0)
        {
            invulverable = 0;
            spriteRenderer.color = Color.white;
        }

    }

    void FixedUpdate()
    {
        rb.linearVelocity = movement * speed;
    }
    private void LateUpdate()
    {
        if (movement.x != 0)
        {
            spriteRenderer.flipX = movement.x < 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // check if they can be hurt
        if (invulverable == 0)
        {
            // check if they are hit by an enemy and wouldn't die
            if (collision.gameObject.CompareTag("Enemy") && currHealth > 1)
            {
                invulverable = 3;
                currHealth--;
                // set them invulnerable for 3 seconds
                // after 3 seconds, remove a health point -- they can be hurt again

            } else if (collision.gameObject.CompareTag("Enemy") && currHealth <= 1) {
                // game over
                currHealth = 0;
                isAlive = false;
                // pause background music and play die animation
                musicSource.Pause();
                animator.SetTrigger("Die");
                //gameover animation

                //StartCoroutine(GameOverAfterAnimation());
                // freeze the player
                speed = 0;
                //note: this still doesn't stop the items from damaging enemies

                // give the user some time to see the game over message
                GameManager.Instance.timeBetweenRounds = 3;
                // (GameManager handles the game over state)
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

        if (attackSound != null)
        {
            effectsSource.PlayOneShot(attackSound);
        }

        Vector2 attackPosition = (Vector2)transform.position + lastMoveDirection.normalized;
        float angle = Mathf.Atan2(lastMoveDirection.y, lastMoveDirection.x) * Mathf.Rad2Deg;

        float rotationOffset = -130f; 

        if (attackEffectPrefab != null)
        {
            GameObject effect = Instantiate(
                attackEffectPrefab,
                attackPosition,
                Quaternion.Euler(0, 0, angle + rotationOffset)
            );
            Destroy(effect, 0.2f);
        }

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPosition, attackRange);
        foreach (Collider2D enemy in hitEnemies)
        {
            Enemy enemyController = enemy.GetComponent<Enemy>();
            if (enemyController != null)
            {
                enemyController.changeHealth(-attackDamage);
                enemyController.anim.SetTrigger("Hit");
                enemyController.StartCoroutine(enemyController.KnockBack());
            }
        }
    }

    public void ResetPlayer()
    {
        isAlive = true;
        speed = 3.25f;
        maxHealth = 3; // from what we have decided?
        currHealth = maxHealth;
    }

     private IEnumerator GameOverAfterAnimation()
    {
        // Waiting until animation is finished
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        // Game over
        Time.timeScale = 0f;
    }
}