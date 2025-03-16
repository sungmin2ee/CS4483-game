using UnityEngine;
using System.Collections;

public class EnemyBoss : Enemy
{
    public float dashSpeedMultiplier = 3f;
    public float dashDuration = 0.5f;
    public float dashCooldown = 2f;
    private bool isDashing = false;

    void Start()
    {
        StartCoroutine(DashRoutine());
    }

    void FixedUpdate()
    {
        if (target == null)
            return;

        Vector2 dirVec = target.position - GetComponent<Rigidbody2D>().position;
        Vector2 nextVec = dirVec.normalized * (isDashing ? speed * dashSpeedMultiplier : speed) * Time.fixedDeltaTime;
        GetComponent<Rigidbody2D>().MovePosition(GetComponent<Rigidbody2D>().position + nextVec);
    }

    private IEnumerator DashRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(dashCooldown);
            if (target != null)
            {
                isDashing = true;
                yield return new WaitForSeconds(dashDuration);
                isDashing = false;
            }
        }
    }

    public void Kill()
    {
        StopCoroutine(DashRoutine());
        base.GetType().GetMethod("Kill", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).Invoke(this, null);
    }
}