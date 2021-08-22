using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using UnityEditor;


public class Jump : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;
    // private PolygonCollider2D polCol;

    public bool ReadyJump;
    public bool ReadyGround;

    public Animator anim;

    public int score;
    private int HighScore;
    public Text scoreT;

    //public Sound[] sounds;
    private bool sound;
    public List<AudioSource> L_Sounds;

    private System.Random rand = new System.Random();
    public AudioSource CrashSound;
    public AudioSource FailSound;


    // obj Lose Panel
    public List<GameObject> LoseObjects;
    // public List<GameObject> LosePanel;
    public GameObject LosePanel;

    public Image Load_Game;

    public Text Lose_Score;

    public Text New_Record;

    public Button B_Jump;

    public GameObject B_Pause;

#if UNITY_IOS
    private string gameId ="4088232";
#elif UNITY_ANDROID
    private string gameId = "4088233";
#endif

    bool testMode = false;
    public GameObject GooglePlayobj;

    private void Awake()
    {
        //FPS
        Application.targetFrameRate = 60;
        rigidbody2d = GetComponent<Rigidbody2D>();

        New_Record.enabled = false;

        sound = DataHolder._Sound;

        score = 0;

    }
    void Start()
    {
        Advertisement.Initialize(gameId, testMode);

        scoreT.text = score.ToString();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (ReadyJump && ReadyGround)
        {
            anim.SetBool("Jump", true);
            float jumpVelocity = 7f;
            rigidbody2d.velocity = Vector2.up * jumpVelocity;
            //  rigidbody2d.AddForce(new Vector2(0f, 5f));            
            ReadyGround = false;
            ReadyJump = false;
            B_Jump.interactable = false;
        }
    }

    public void JumpPlayer()
    {
        ReadyJump = true;
        int randNum = rand.Next(0, L_Sounds.Count);

        if (sound == true)
            L_Sounds[randNum].GetComponent<AudioSource>().Play();

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Road")
        {
            ReadyGround = true;
            B_Jump.interactable = true;
            anim.SetBool("Jump", false);
        }
        else
            ReadyGround = false;
        if (collision.gameObject.tag == "Enemy")   //PROEBAL
        {
            B_Pause.SetActive(false);

            Load_Game.enabled = true;

            foreach (GameObject obj in LoseObjects)
            {
                obj.SetActive(false);
            }

            LosePanel.SetActive(true);

            Lose_Score.text = score.ToString();

            /////////
            ////Spawn the prefab gameobject
            //GameObject gameObject = GameObject.Instantiate(Resources.Load("SoundCrash")) as GameObject;
            ////Get a reference to its component LevelChanger
            //Crash_Sound SoundCrash = gameObject.GetComponent<Crash_Sound>();
            ////Call the function
            //SoundCrash.PlayCrashSound();  ////////
            ////////////
            // IMage + text + Button View True


            sound = DataHolder._Sound;
            if (sound == true)
            {
                // Audio crash + Fail
                CrashSound.GetComponent<AudioSource>().Play();

                FailSound.GetComponent<AudioSource>().Play();
            }


            HighScore = PlayerPrefs.GetInt("HighScore");
            if (HighScore < score)
            {
                PlayerPrefs.SetInt("HighScore", score);

                // Add HighScore to Leadeboard

                
                GooglePlay GooglePlay = GooglePlayobj.GetComponent<GooglePlay>();
                //Call the function
                GooglePlay.AddScore(score);  ////////
                

                New_Record.enabled = true;
            }

            int Coins = PlayerPrefs.GetInt("Coins");

            PlayerPrefs.SetInt("Coins", score + Coins);

            DataHolder.LoseCount += 1;
            if (DataHolder.LoseCount > 2)
            {
                ShowInterAd();
            }


        }

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Score")
        {
            score = AddScore();
            scoreT.text = score.ToString();
            DataHolder._Score = score;
        }
    }

    public int AddScore()
    {
        score++;
        return score;
    }

    // ZANOVO (Restart)

    public void RestartGame()
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }

    public void Go_Home()
    {
        SceneManager.LoadScene(0);
    }


    public void ShowInterAd()
    {
        if (Advertisement.IsReady())
        {
            // Time.timeScale = 0;
            Advertisement.Show();
        }
        else
            Debug.Log("No Ads");
    }

    public void ShowRewardedAD(Action<ShowResult> callback)
    {
        if (UnityAdsReward.IsReady())
        {
            UnityAdsReward.Show(callback);

        }
    }
    public static class UnityAdsReward
    {
        public static bool IsReady() => Advertisement.IsReady(placementId: "rewardedVideo");

        public static void Show(Action<ShowResult> callback)
        {
            ShowOptions options = new ShowOptions
            {
                resultCallback = callback
            };
            Advertisement.Show(placementId: "rewardedVideo", options);
        }
    }

    public void REWARED_Add()
    {

        ShowRewardedAD(result =>
        {
            switch (result)
            {
                case ShowResult.Failed:
                    Debug.Log("No Ads");
                    break;

                case ShowResult.Finished:
                    int Coins = PlayerPrefs.GetInt("Coins");
                    PlayerPrefs.SetInt("Coins", score + Coins);
                    break;

                case ShowResult.Skipped:
                    Debug.Log("No Ads");
                    break;
            }
            SceneManager.LoadScene("Game", LoadSceneMode.Single);
        });

        //score = score * 2;
        //DataHolder._Score = score;
    
        //else
        //    Debug.Log("No Ads");
    }
}


