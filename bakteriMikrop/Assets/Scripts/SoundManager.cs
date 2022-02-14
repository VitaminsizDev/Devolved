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
    public AudioClip KucukEvrilmeSesi;
    public AudioClip BuyukEvrilmeSesi;
    public AudioClip ClickSesi;
    public AudioClip[] popSesi;
    public AudioClip[] geriKapanmaSesi;
    public AudioClip[] pokemonBaslangicSesi;
    public AudioClip[] pokemonBitmeSesi;
    public AudioClip[] ZiplamaSesi;
    
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
        sfxSource.PlayOneShot(KucukEvrilmeSesi, sfxVolume);
    }
    
    public void PlayBuyukEvrilmeSesi()
    {
        sfxSource.PlayOneShot(BuyukEvrilmeSesi, sfxVolume);
    }
    
    public void PlayClickSesi()
    {
        //Play a random sound from the array of sounds
        sfxSource.PlayOneShot(ClickSesi, sfxVolume);
    }
    
    public void PlayPopSesi()
    {
        //Play a random sound from the array of sounds
        sfxSource.PlayOneShot(popSesi[Random.Range(0, popSesi.Length)], sfxVolume);
    }
    
    public void PlayGeriKapanmaSesi()
    {
        //Play a random sound from the array of sounds
        sfxSource.PlayOneShot(geriKapanmaSesi[Random.Range(0, geriKapanmaSesi.Length)], sfxVolume);
    }
    
    public void PlayPokemonBaslangicSesi()
    {
        //Play a random sound from the array of sounds
        sfxSource.PlayOneShot(pokemonBaslangicSesi[Random.Range(0, pokemonBaslangicSesi.Length)], sfxVolume);
    }
    
    public void PlayPokemonBitmeSesi()
    {
        //Play a random sound from the array of sounds
        sfxSource.PlayOneShot(pokemonBitmeSesi[Random.Range(0, pokemonBitmeSesi.Length)], sfxVolume);
    }
    
    public void PlayZiplamaSesi()
    {
        //Play a random sound from the array of sounds
        sfxSource.PlayOneShot(ZiplamaSesi[Random.Range(0, ZiplamaSesi.Length)], sfxVolume);
    }
}
