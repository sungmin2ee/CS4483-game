using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemData data;
    public int level;
    public Weapon weapon;

    Image icon;
    Text textLevel;
    Text textName;
    Text textDesc;
    public Gear gear;

    private void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1];
        icon.sprite = data.itemIcon;

        Text[] texts = GetComponentsInChildren<Text>();
        textLevel = texts[0];
        textName = texts[1];
        textDesc = texts[2];
        textName.text = data.itemName;
    }
    private void OnEnable()
    {
        textLevel.text = "Lv." + level;
        switch (data.itemType)
        {
            case ItemData.ItemType.Melee:
            case ItemData.ItemType.Range:
                textDesc.text = string.Format(data.itemDesc, data.damages[level]*100, data.counts[level]);
                break;
            case ItemData.ItemType.Glove:
            case ItemData.ItemType.Heal:
                textDesc.text = string.Format(data.itemDesc, data.damages[level]*100);
                break;
            case ItemData.ItemType.Shoe:
                textDesc.text = string.Format(data.itemDesc, data.damages[level] * 100);
                break;
            default:
                textDesc.text = string.Format(data.itemDesc);
                break;
        }
                
    }
    public void Onclick()
    {
        switch (data.itemType)
        {
            case ItemData.ItemType.Melee:
            case ItemData.ItemType.Range:
                if(level == 0)
                {
                    GameObject newWeapon = new GameObject();
                    weapon = newWeapon.AddComponent<Weapon>();
                    weapon.Init(data);
                }
                else
                {
                    float nextDamage = data.baseDamage;
                    //int nextCount = 0;
                    nextDamage += data.baseDamage * data.damages[level];
                    //nextCount += data.counts[level];
                    int nextCount = weapon.count + data.counts[level];
                    weapon.LevelUp(nextDamage, nextCount);
                    Debug.Log(nextCount);
                }
                break;
            case ItemData.ItemType.Glove:
                break;
            case ItemData.ItemType.Heal:
                break;
            case ItemData.ItemType.Shoe:
                if(level == 0)
                {
                    GameObject newGear = new GameObject();
                    gear = newGear.AddComponent<Gear>();
                    gear.Init(data);
                }
                else
                {
                    float nextRate = data.damages[level];
                    gear.levelUp(nextRate);
                }
                break;
        }
        Debug.Log("Item Clicked");
        level++;
        if (level == data.damages.Length)
        {
            GetComponent<Button>().interactable = false;
        }
    }

}
