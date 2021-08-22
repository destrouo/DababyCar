using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyRewards : MonoBehaviour
{

    public GameObject Rec, Soc, B_Play, Daily_panel, B_Claim , Claim_panel, shop;
    public Text Coins, status , T_GetCoins;


    public Reward rewardPref;
    public Transform rewarsGrid;

    public List<RewardData> rewards;

    private List<Reward> rewardPrefabs;
    private int currentStreak
    {
        get => PlayerPrefs.GetInt("currentStreak", 0);
        set => PlayerPrefs.SetInt("currentStreak", value);
    }

    [System.Serializable]
    public class RewardData
    {
        public int Value;
    }

    
    private DateTime? lastClaimTime
    {
        get
        {
            string data = PlayerPrefs.GetString("lastClaimTime", null);

            if (!string.IsNullOrEmpty(data))
                return DateTime.Parse(data);

            return null;
        }
        set
        {
            if (value != null)
                PlayerPrefs.SetString("lastClaimTime", value.ToString());
            else
                PlayerPrefs.DeleteKey("lastClaimTime");
        }
    }

    private bool canClaim;
    private int maxStreak = 7;
    //  / 24 /60 /6 / 2 
    private float claimCoolDown = 24f ;
    private float claimDeadLine = 48f;

    // Start is called before the first frame update
    void Start()
    {
        if (DataHolder.HasPlayed == 0)
        {
            Daily_panel.SetActive(true);

            Claim_panel.SetActive(false);
            Rec.SetActive(false);
            Soc.SetActive(false);
            B_Play.SetActive(false);
            shop.SetActive(false);

            Coins.text = PlayerPrefs.GetInt("Coins").ToString();
            DataHolder.HasPlayed = 1;

            InitPrefabs();
            StartCoroutine(RewardStateUpdater());
        }
        else
        {
            Daily_panel.SetActive(false);

            Claim_panel.SetActive(false);
            Rec.SetActive(true);
            Soc.SetActive(true);
            B_Play.SetActive(true);
            shop.SetActive(true);
        }
        
    }

    private void InitPrefabs()
    {
        rewardPrefabs = new List<Reward>();

        for (int i = 0; i < maxStreak; i++)
            rewardPrefabs.Add(Instantiate(rewardPref, rewarsGrid, false));
    }

    private IEnumerator RewardStateUpdater()
    {
        while (true)
        {
            UpdateRewarrdsState();
            yield return new WaitForSeconds(1);
        }
    }
    private void UpdateRewarrdsState()
    {
        canClaim = true;

        if(lastClaimTime.HasValue)
        {
            var timeSpan = DateTime.UtcNow - lastClaimTime.Value;

            if (timeSpan.TotalHours > claimCoolDown)
            {
                lastClaimTime = null;
                currentStreak = 0;
            }
            else if (timeSpan.TotalHours < claimCoolDown)
                canClaim = false;
        }
        UpdateRewardsUI();
    }

    private void UpdateRewardsUI()
    {
        B_Claim.SetActive(canClaim);

        if (canClaim)
            status.text = "Claim your reward !";
        else
        {
            var nextClaim = lastClaimTime.Value.AddHours(claimCoolDown);
            var currentClaimCoolDown = nextClaim - DateTime.UtcNow;

            string cd = $"{currentClaimCoolDown.Hours:D2}:{currentClaimCoolDown.Minutes:D2}:{currentClaimCoolDown.Seconds:D2}";

            status.text = $"Come back in {cd} for your next reward";
        }

        for (int i = 0; i < rewardPrefabs.Count; i++)
            rewardPrefabs[i].RewardData(i, currentStreak, rewards[i]);
    }

    public void ClaimReward()
    {
        if (!canClaim)
            return;

        // give coins
        var reward = rewards[currentStreak];

        int numCoins = PlayerPrefs.GetInt("Coins");
        PlayerPrefs.SetInt("Coins", numCoins + reward.Value);

        Coins.text = PlayerPrefs.GetInt("Coins").ToString();

        lastClaimTime = DateTime.UtcNow;
        currentStreak = (currentStreak + 1) % maxStreak;

        UpdateRewarrdsState();

        Claim_panel.SetActive(true);
        T_GetCoins.text = $"Got {reward.Value} Coins";


    }

    public void CloseRewards()
    {
        Claim_panel.SetActive(false);
        Daily_panel.SetActive(false);

        Rec.SetActive(true);
        Soc.SetActive(true);
        B_Play.SetActive(true);
        shop.SetActive(true);
    }

    public void OpenDaily()
    {
        Daily_panel.SetActive(true);

        Claim_panel.SetActive(false);
        Rec.SetActive(false);
        Soc.SetActive(false);
        B_Play.SetActive(false);
        shop.SetActive(false);

        Coins.text = PlayerPrefs.GetInt("Coins").ToString();

        InitPrefabs();
        StartCoroutine(RewardStateUpdater());
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
