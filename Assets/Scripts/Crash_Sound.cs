using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crash_Sound : MonoBehaviour
{

    private bool sound;

    public Sound[] Crash;

    public void Awake()
    {
       
    }

    public void PlayCrashSound()
    {
        sound = DataHolder._Sound;
        if (sound == true)
        {
            // Audio crash + Fail
            Sound s = Crash[0];
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.Play();

            Sound s1 = Crash[1];
            s1.source = gameObject.AddComponent<AudioSource>();
            s1.source.clip = s1.clip;
            s1.source.Play();

        }
    }
}
