using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundLoop : MonoBehaviour
{
    public AudioSource audioSource;
    public bool isBeUsed;
    public void updateSetting(SoundConfig _soundCfg)
    {
        if (GameManager.Instance && GameManager.Instance.data != null)
        {
            audioSource.volume = _soundCfg.volumeSfx * GameManager.Instance.data.setting.soundVolume;
            //music.volume = _soundCfg.volumeBgm * GameManager.Instance.data.setting.musicVolume;
        }
    }
    public void ChangeStateSoud(bool val)
    {
        isBeUsed = val;
        if(val)
            audioSource.UnPause();
        else 
            audioSource.Pause();
    }
    public void PlaySound(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        ChangeStateSoud(true);
        audioSource.Play();

    }
    public void StopSound()
    {
        ChangeStateSoud(false);
    }
}
