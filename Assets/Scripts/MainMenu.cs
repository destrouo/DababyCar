using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Sprite music_on;
    public Sprite music_off;

    public Sprite sound_on;
    public Sprite sound_off;

    private bool Music;
    private bool Sound;

    public AudioSource Click_Sound;

    private int highScore;
    public Text T_HighScore;
    public Text T_Coins;

    public Animator transition;


    public void Awake()
    {

    }
    public void Start()
    {
        Time.timeScale = 1f;

        Music = true;
        DataHolder._Music = Music;
        Camera.main.GetComponent<AudioSource>().enabled = true;

        Sound = true;
        DataHolder._Sound = Sound;

        //If FIRST Start Up
        int hasPlayed = PlayerPrefs.GetInt("HasPlayed");

        if (hasPlayed == 0)
        {
            // First Time

            int[] StockWheels = {0};
            PlayerPrefsX.SetIntArray("StockWheels", StockWheels);

            PlayerPrefs.SetInt("HasPlayed", 1);
        }


    }

    IEnumerator LoadScene(int Index)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(Index);
    }

    public void ChangeScene_Game()
    {
        Time.timeScale = 1f;
        StartCoroutine(LoadScene(1));
    }

    public void ChangeScene_Home()
    {
        Time.timeScale = 1f;
        StartCoroutine(LoadScene(0));
    }

    public void ChangeScene_SHOP()
    {
        Time.timeScale = 1f;
        StartCoroutine(LoadScene(2));
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void OnClickSettings()
    {
        for (int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).gameObject.SetActive(!transform.GetChild(i).gameObject.activeSelf);

    }

    public void OnClickRecord()
    {
        if (PlayerPrefs.HasKey("HighScore"))
            highScore= PlayerPrefs.GetInt("HighScore");

        T_HighScore.text = highScore.ToString();
        T_Coins.text = PlayerPrefs.GetInt("Coins").ToString();
        //for (int i = 0; i < transform.childCount; i++)
        transform.GetChild(0).gameObject.SetActive(!transform.GetChild(0).gameObject.activeSelf);


    }

    /// <summary>
    /// MUSIC
    /// </summary>
    public void OnClickMusic()
    {
        if (Music == false)
        {
            GetComponent<Image>().sprite = music_on;
            Music = true;
            DataHolder._Music = true;

            Camera.main.GetComponent<AudioSource>().enabled = true;


        }
        else
        {
            GetComponent<Image>().sprite = music_off;
            Music = false;
            DataHolder._Music = false;

            Camera.main.GetComponent<AudioSource>().enabled = false;
        }


    }

    /// <summary>
    ///  SOUND
    /// </summary>
    public void OnClickSound()
    {
        if (Sound == false)
        {
            GetComponent<Image>().sprite = sound_on;

            Sound = true;
            DataHolder._Sound = true;

        }
        else
        {
            GetComponent<Image>().sprite = sound_off;

            Sound = false;
            DataHolder._Sound = false;
        }
    }

    public void ClickSound()
    {
        Sound = DataHolder._Sound;
        if (Sound == true)
            Click_Sound.GetComponent<AudioSource>().Play();
    }

    private IEnumerator RewardStateUpdater()
    {
        while (true)
        {
            UpdateCoinsStats();
            yield return new WaitForSeconds(5);
        }
    }

    public void UpdateCoinsStats()
    {
        T_Coins.text = PlayerPrefs.GetInt("Coins").ToString();
    }
    public void OpenInst()
    {
        Application.OpenURL("https://www.instagram.com/choco.nuts_games/");
    }
    public void OpenYT()
    {
        Application.OpenURL("https://www.youtube.com/channel/UCxpNkCbRlRxd_-OH8NTrYTg?sub_confirmation=1");
    }
    public void OpenTikTok()
    {
        Application.OpenURL("http://www.tiktok.com/@choconuts_games/");
    }

}
