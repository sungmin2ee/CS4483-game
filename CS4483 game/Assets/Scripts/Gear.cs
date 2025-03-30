using UnityEngine;

public class Gear : MonoBehaviour
{
    public ItemData.ItemType type;
    public float rate;

    public void Init(ItemData data)
    {
        name = "Gear " + data.itemId;
        transform.parent = GameManager.Instance.player.transform;
        transform.localScale = Vector3.zero;

        type = data.itemType;
        rate = data.damages[0];
        ApplyGear();
    }
    public void levelUp(float rate)
    {
        this.rate = rate;
        ApplyGear();

    }
    void RateUp()
    {
        Weapon[] weapons = transform.parent.GetComponentsInChildren<Weapon>();
        foreach (Weapon weapon in weapons)
        {
            switch (weapon.id)
            {
                case 0:
                    weapon.speed = 150 + (150 *rate);
                    break;
                default:
                    weapon.speed = 0.5f *(1f - rate);
                    break;
            }
        }
    }
    void speedUp()
    {
        float speed = 3.25f;
        GameManager.Instance.player.speed = speed + speed * rate;

    }
    void ApplyGear()
    {
        switch(type)
        {
            case ItemData.ItemType.Shoe:
                speedUp(); 
                break;
        }
    }
}
