using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reward : MonoBehaviour
{
    public Image background;
    public Color deafutColor;
    public Color currentReward;

    public Text T_Day;
    public Text Value;

    public void RewardData(int day, int currentStreak, DailyRewards.RewardData reward)
    {
        T_Day.text = $"Day {day + 1}";

        Value.text = reward.Value.ToString();
        background.color = day == currentStreak ? currentReward : deafutColor;
    }
}
