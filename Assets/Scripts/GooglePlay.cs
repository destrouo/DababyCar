using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class GooglePlay : MonoBehaviour
{
    public GameObject PanelActive;
    public Text ChangeText;
    string leadBoard = "CgkI4-flwqYdEAIQAQ";
    private float sec = 2f;

    public static PlayGamesPlatform platform;

    private void Awake()
    {
        PanelActive.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayGamesPlatform.DebugLogEnabled = false;
        if (platform == null)
        {
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
            PlayGamesPlatform.InitializeInstance(config);
           
            platform = PlayGamesPlatform.Activate();
        }

        Social.Active.localUser.Authenticate(success =>
        {
            if (success)
                ChangeText.text = "Success Log in Google Play"; //"Success Log in Google Play"); //Show Yes
            else
                ChangeText.text = "NOT Success Log in Google Play"; //Show No
        });
        PanelActive.SetActive(true);

        StartCoroutine(LateCall());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowLeader()
    {
        if (Social.Active.localUser.authenticated)
        {
            platform.ShowLeaderboardUI();
        }
    }

    public void AddScore(int score)
    {
        if (Social.Active.localUser.authenticated)
        {
            Social.ReportScore(score, leadBoard, success => { });

            ChangeText.text = "Success Log in Google Play";
        }
        else
            ChangeText.text = "NOT Success Log in Google Play"; //Show No

            //Social.ReportScore(score, leadBoard, (bool success) => {
            //    //("Title here", "Your text", "Ok")
            //    if (success)
            //        ChangeText.text = "Success Log in Google Play"; //"Success Log in Google Play"); //Show Yes
            //    else
            //        ChangeText.text = "NOT Success Log in Google Play"; //Show No
            //});

        PanelActive.SetActive(true);

        StartCoroutine(LateCall());
    }

    public void ExitFromGPS()
    {
        PlayGamesPlatform.Instance.SignOut();
    }

    IEnumerator LateCall()
    {

        yield return new WaitForSeconds(sec);

        PanelActive.SetActive(false);
        //Do Function here...
    }

}
