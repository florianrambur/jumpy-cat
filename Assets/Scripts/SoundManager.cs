using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager _instance;

    private static bool isEnable = true;
    private static AudioSource MainSong, DefeatSong, JumpSFX, TakeDamageSFX;

    private void Awake()
    {
        if (_instance == null)
        {
            MainSong = GameObject.Find("Song_Main").GetComponent<AudioSource>();
            DefeatSong = GameObject.Find("Song_Defeat").GetComponent<AudioSource>();
            JumpSFX = GameObject.Find("SFX_Jump").GetComponent<AudioSource>();
            TakeDamageSFX = GameObject.Find("SFX_TakeDamage").GetComponent<AudioSource>();

            _instance = this;

            DontDestroyOnLoad(this);
        }
        else
        {
            if (this != _instance)
            {
                Destroy(this.gameObject);
            }
        }
    }

    public static void ToggleSoundActivation()
    {
        isEnable = ! isEnable;

        AudioListener.volume = isEnable ? 1 : 0;
    }

    public static void StartMainSong()
    {
        if (! MainSong.isPlaying)
        {
            MainSong.Play();
        }
    }

    public static void StopMainSong()
    {
        MainSong.Stop();
    }

    public static void StartDefeatSong()
    {
        StopMainSong();

        if (! DefeatSong.isPlaying)
        {
            DefeatSong.Play();
        }
    }

    public static void StartJumpSFX()
    {
        if (! JumpSFX.isPlaying)
        {
            JumpSFX.Play();
        }
    }

    public static void StartTakeDamageSFX()
    {
        TakeDamageSFX.Play();
    }
}
