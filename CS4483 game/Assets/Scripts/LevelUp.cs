using UnityEngine;

public class LevelUp : MonoBehaviour
{
    RectTransform rect;
    Item[] items;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rect = GetComponent<RectTransform>();
        items = GetComponentsInChildren<Item>(true);
    }

    // Update is called once per frame
    public void Show()
    {
        rect.localScale = Vector3.one;
        Time.timeScale = 0;
    }
    public void Hide()
    {
        rect.localScale = Vector3.zero;
        Time.timeScale = 1;

    }
    public void Select(int i)
    {
        items[i].Onclick();
    }
}
