using UnityEngine;

public class LevelUp : MonoBehaviour
{
    RectTransform rect;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rect = GetComponent<RectTransform>();
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
}
