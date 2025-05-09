using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType { Level, Exp, Kill, Health, Time, Round, RoundEnd_GameEnd}
    public InfoType type;

    // character hud
    Text levelText;
    Slider expSlider;
    Slider hpBar;

    // round hud
    Text timeText;
    Text roundText;
    Text roundEndText;

    private void Awake()
    {
        // character hud
        levelText = GetComponent<Text>();
        expSlider = GetComponent<Slider>();
        hpBar = GetComponent<Slider>();

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
                hpBar.value = (float) Player.Instance.currHealth / (float) Player.Instance.maxHealth;
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

            case InfoType.RoundEnd_GameEnd:
                int round = GameManager.Instance.round;
                // check if alive
                if (Player.Instance.isAlive == false) {
                    roundEndText.text = "You fell to the monsters...";
                } // else, check if they won
                else if ((GameManager.Instance.timeRemaining == 0) && round == 3) {
                    roundEndText.text = "You fought back all the monsters!";
                } // else, check if they finished a round
                else if (GameManager.Instance.timeRemaining == 0) {                    
                    roundEndText.text = "Round " + round + " complete!";
                } // else, don't display anything
                else roundEndText.text = ""; // don't display anything if we're not between rounds
                break;

        }
    }
}
