using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //Singleton
    public static SoundManager instance;
    
    [Header("Ses Ayarlari")]
    public float sfxVolume = 1f;
    public float musicVolume = 1f;
    
    [Header("Obje Referans")]
    public AudioSource sfxSource;
    public AudioSource musicSource;
    
    [Header("Sesler")]
    public AudioClip musicClip;
    public AudioClip menuClip;
    public AudioClip[] DnaToplamaSesi;
    public AudioClip[] KucukEvrilmeSesi;
    public AudioClip[] BuyukEvrilmeSesi;
    public AudioClip ClickSesi;
    
    //Awake
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
            instance = this;
        }
        musicSource.clip = musicClip;
        //Volume
        musicSource.volume = musicVolume;
        musicSource.Play();
    }
    
    //Fading Music Change
    public void FadeMusicChange(bool isMenu)
    {
        musicSource.Play();
        //Fade Sequence
        Sequence musicFade = DOTween.Sequence();
        musicFade.Append(DOTween.To(() => musicSource.volume, x => musicSource.volume = x, 0, 0.5f));
        musicFade.AppendCallback(() => musicSource.clip = isMenu ? menuClip : musicClip);
        musicFade.AppendCallback(() => musicSource.Play());
        musicFade.Append(DOTween.To(() => musicSource.volume, x => musicSource.volume = x, musicVolume, 0.5f));
        musicFade.Play();
    }
    
    //Sesleri Çal
    public void PlayDnaToplamaSesi()
    {
        //Play a random sound from the array of sounds
        sfxSource.PlayOneShot(DnaToplamaSesi[Random.Range(0, DnaToplamaSesi.Length)], sfxVolume);
    }
    
    public void PlayKucukEvrilmeSesi()
    {
        //Play a random sound from the array of sounds
        sfxSource.PlayOneShot(KucukEvrilmeSesi[Random.Range(0, KucukEvrilmeSesi.Length)], sfxVolume);
    }
    
    public void PlayBuyukEvrilmeSesi()
    {
        //Play a random sound from the array of sounds
        sfxSource.PlayOneShot(BuyukEvrilmeSesi[Random.Range(0, BuyukEvrilmeSesi.Length)], sfxVolume);
    }
    
    public void PlayClickSesi()
    {
        //Play a random sound from the array of sounds
        sfxSource.PlayOneShot(ClickSesi, sfxVolume);
    }
    
}
