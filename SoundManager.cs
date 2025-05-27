using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource sfxSource;  
    public AudioSource bgmSource;  
    public AudioClip backgroundMusic1;  
    public AudioClip backgroundMusic2;  

    public AudioClip F_Do, F_Re, F_Mi, F_Fa, S_Sol, S_La, S_Ti, S_Do;

    private bool isSwitched = false; 

    void Start()
    {
        PlayBackgroundMusic(backgroundMusic1); 
    }

    public void PlaySound(string note)
    {
        switch (note)
        {
            case "F_Do":
                sfxSource.PlayOneShot(F_Do);
                break;
            case "F_Re":
                sfxSource.PlayOneShot(F_Re);
                break;
            case "F_Mi":
                sfxSource.PlayOneShot(F_Mi);
                break;
            case "F_Fa":
                sfxSource.PlayOneShot(F_Fa);
                break;
            case "S_Sol":
                sfxSource.PlayOneShot(S_Sol);
                break;
            case "S_La":
                sfxSource.PlayOneShot(S_La);
                break;
            case "S_Ti":
                sfxSource.PlayOneShot(S_Ti);
                break;
            case "S_Do":
                sfxSource.PlayOneShot(S_Do);
                break;
        }
    }

    public void PlayBackgroundMusic(AudioClip music)
    {
        if (bgmSource.clip != music)
        {
            bgmSource.Stop();
            bgmSource.clip = music;
            bgmSource.loop = true;
            bgmSource.Play();
        }
    }

    public void CheckScoreAndSwitchMusic(int score)
    {
        if (score >= 25 && !isSwitched)
        {
            PlayBackgroundMusic(backgroundMusic2);
            isSwitched = true; 
        }
    }
}

