using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class DynamicSorting : MonoBehaviour {
    private SpriteRenderer spriteRenderer;

    public int sortingOrderBase = 2; 
    public int offset = 0; 

    void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void LateUpdate() {
        spriteRenderer.sortingOrder = (int)(sortingOrderBase - transform.position.y) + offset;
    }
}
