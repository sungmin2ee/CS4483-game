using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    private Rigidbody2D rb;

    // Player Status
    public float moveSpeed = 3f;
    private float originalMoveSpeed;
    public int power;
    public int maxHp;
    public int CurHp;
    public float maxStamina;
    public float curStamina;
    private float staminaBoost = 0.1f;
    private Vector3 originalPosition;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        originalMoveSpeed = moveSpeed;
        CurHp = maxHp;
        curStamina = maxStamina;
        power = 1;
        originalPosition = transform.position;
    }

    void Update()
    {
        HandleMovement();
        if (CurHp <= 0)
        {
            rb.linearVelocity = Vector2.zero;
        }

    }

    public void HandleMovement()
    {

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        Vector2 moveDir = new Vector2(moveX, moveY).normalized;



        if (moveDir != Vector2.zero)
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                if (curStamina < 0)
                {
                    rb.linearVelocity = moveDir * moveSpeed;

                }
                else
                {
                    rb.linearVelocity = moveDir * moveSpeed * 1.5f;
                    curStamina -= 0.2f;
                }
            }
            else
            {

                rb.linearVelocity = moveDir * moveSpeed;
                if (curStamina < 100)
                {
                    curStamina += staminaBoost;
                }

            }

        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }
}
