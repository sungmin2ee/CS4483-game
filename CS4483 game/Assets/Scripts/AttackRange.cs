using UnityEngine;
using System.Collections;

public class AttackRange : MonoBehaviour
{
    private Collider2D col;
    private SpriteRenderer attackRangeRenderer;
    private float originalAlpha;
    void Awake()
    {
        col = GetComponent<Collider2D>();
        attackRangeRenderer = GetComponent<SpriteRenderer>();
        originalAlpha = attackRangeRenderer.color.a;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(DisableAttackRange());
        }
    }

    private IEnumerator DisableAttackRange()
    {
        if (col != null)
        {
            col.enabled = false;
            attackRangeRenderer.color = new Color(attackRangeRenderer.color.r, attackRangeRenderer.color.g, attackRangeRenderer.color.b, 0f); 
            yield return new WaitForSeconds(2f);
            attackRangeRenderer.color = new Color(attackRangeRenderer.color.r, attackRangeRenderer.color.g, attackRangeRenderer.color.b, originalAlpha);
            col.enabled = true; 
        }
    }
}
