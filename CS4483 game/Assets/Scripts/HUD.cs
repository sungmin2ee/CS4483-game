using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType {Level, Exp, Kill, Health, Time, Round, RoundEnd}
    public InfoType type;

    // character hud
    Text levelText;
    Slider expSlider;

    // round hud
    Text timeText;
    Text roundText;
    Text roundEndText;

    private void Awake()
    {
        // character hud
        levelText = GetComponent<Text>();
        expSlider = GetComponent<Slider>();

        // round hud
        timeText = GetComponent<Text>();
        roundText = GetComponent<Text>();
        roundEndText = GetComponent<Text>();
    }

    private void LateUpdate()
    {
        switch (type)
        {
            case InfoType.Level:
                levelText.text = string.Format("Level: {0:F0}", GameManager.Instance.level);
                break;

            case InfoType.Exp:
                float curExp = GameManager.Instance.exp;
                float maxExp = GameManager.Instance.nextExp[GameManager.Instance.level];
                expSlider.value = curExp/maxExp;
                break;

            case InfoType.Kill:
                break;

            case InfoType.Health:
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

            case InfoType.Round:
                roundText.text = string.Format("Round: {0:F0}", GameManager.Instance.round);
                break;

            case InfoType.RoundEnd:
                if (GameManager.Instance.timeRemaining == 0) {
                    // display round end celebration
                    roundEndText.text = "Round " + GameManager.Instance.round + " Complete!";
                } else roundEndText.text = ""; // don't display anything if we're not between rounds
                break;

        }
    }
}
