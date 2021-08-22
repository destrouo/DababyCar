using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class StartGame : MonoBehaviour
{
    public List<GameObject> Objects;
    // float timeStart = 3f;

    int TimerStart = 3;
    public Text Timer;
    public Image Load_Game;

    private bool Music;
    private bool Sound;

    public AudioSource LetsGo;

    public static bool GamePause = false;
    public GameObject PanePause;
    public GameObject B_Pause;

    public AudioSource GameMusic;

    private void Awake()
    {
        Time.timeScale = 1f;

       // Music = DataHolder._Music;

       // Sound = DataHolder._Sound;

        countDownTimer();
        foreach (GameObject obj in Objects)
        {
            obj.SetActive(false);
        }
        Load_Game.enabled = true;
        B_Pause.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (DataHolder._Music == true)
            GameMusic.GetComponent<AudioSource>().Play();
    }

    void countDownTimer()
    {
        if (TimerStart > 1)
        {
            TimerStart--;
            Timer.text = TimerStart.ToString();
            Invoke("countDownTimer", 1.0f);
        }
        else
        {
            Timer.enabled = false;
            Load_Game.enabled = false;

            foreach (GameObject obj in Objects)
            {
                obj.SetActive(true);
            }
            B_Pause.SetActive(true);
            Sound = DataHolder._Sound;
            if (Sound == true)
            {
                LetsGo.GetComponent<AudioSource>().Play();
            }
        }
    }

    public void Pause()
    {
        PanePause.SetActive(true);
        Time.timeScale = 0f;
        GamePause = true;
        B_Pause.SetActive(false);

        ShowInterAd();
    }

    public void Resume()
    {
        PanePause.SetActive(false);
        Time.timeScale = 1f;
        GamePause = false;
        B_Pause.SetActive(true);
    }

    public void ShowInterAd()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show();
        }
        else
            Debug.Log("No Ads");
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }
}
