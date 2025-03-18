using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    private Rigidbody2D rigid;
    public Rigidbody2D target;
    private bool isAlive;

    public float health;
    public float maxHealth;
    public RuntimeAnimatorController[] animCon;
    Animator anim;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (!isAlive || target == null)
            return;

        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.linearVelocity = Vector2.zero;
    }

    void Kill()
    {
        isAlive = false;
        Destroy(gameObject);
        GameManager.Instance.kill++;
        GameManager.Instance.GetExp();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("AttackRange")) // keep or not??
        {
            Kill();
        }
        else if (collision.CompareTag("Bullet"))
        {
            health -= collision.GetComponent<Bullet>().damage;
            if (health > 0)
            {
                //live ,hit action

            }
            else
            {
                Kill();
            }
        }
    }
    private void OnEnable()
    {
        isAlive = true;
        target = GameManager.Instance.player.GetComponent<Rigidbody2D>();
        health = maxHealth;
    }
    public void Init(SpawnData data)
    {
        anim.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }


}