using UnityEngine;

public class BasicWeapon : MonoBehaviour
{
    public bool isLeft;
    public SpriteRenderer spriter;

    SpriteRenderer player;

    Quaternion leftRot = Quaternion.Euler(0f, 0f, -35f);
    Quaternion leftRotReverse = Quaternion.Euler(0f, 0f, -135f);
    private void Awake()
    {
        player = GetComponentsInParent<SpriteRenderer>()[1];
    }
    private void LateUpdate()
    {
        bool isReverse = player.flipX;
        if (isLeft)
        {
            transform.localRotation = isReverse ? leftRotReverse : leftRot;
            spriter.flipY = isReverse;
            spriter.sortingOrder = isReverse ? 2 : 1; 
        }
    }
}
