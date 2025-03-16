using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType {Exp, Level, Kill, Time, Health }
    public InfoType type;

    Text levelText;
    Text timeText;
    Slider expSlider;
    private void Awake()
    {
        levelText = GetComponent<Text>();
        timeText = GetComponent<Text>();
        expSlider = GetComponent<Slider>();
    }

    private void LateUpdate()
    {
        switch (type)
        {
            case InfoType.Exp:
                float curExp = GameManager.Instance.exp;
                float maxExp = GameManager.Instance.nextExp[GameManager.Instance.level];
                expSlider.value = curExp/maxExp;
                break;

            case InfoType.Level:
                levelText.text = string.Format("Lv.{0:F0}", GameManager.Instance.level);
                break;

            case InfoType.Kill:
                break;

            case InfoType.Time:
                // don't display a negative time
                float timeRemaining = GameManager.Instance.timeRemaining;
                if (timeRemaining <= 0) {
                    timeRemaining = 0;
                } else {
                    // we need to use Floor to display our minutes and seconds
                    // but this means time = 0.9 is displayed as 0 seconds remaining
                    // so just add 1 to the time display
                    timeRemaining += 1;
                }
                // want to display time in terms of minutes and seconds
                // also only want to display whole numbers -- so we use Floor.
                float minutes = Mathf.FloorToInt(timeRemaining / 60);
                float seconds = Mathf.FloorToInt(timeRemaining % 60);
                timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
                break;

            case InfoType.Health:
                break;



        }
    }
}
