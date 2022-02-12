using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using DG.Tweening;
using UnityEngine.Rendering;

public class Evrim : MonoBehaviour
{
    public int totalToplananDnaSayisi = 0;
    public int buElToplananDnaSayisi = 0;
    public float hareketHiziArtisMiktari = 0.1f;
    public float yukariHareketArtisMiktari = 0.1f;

    public Transform respawnpos;
    public BacteriaStats bStats;
    public GameObject player;

    private void Awake()
    {
        ResetEvrim();
    }

    //Start
    private void Start()
    {
        UIController.instance.UpdateEvrimIcinGerekenDnaText(3-buElToplananDnaSayisi);
    }
    //Call EvrimGecir on space key
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EvrimGecir();
        }
    }
    
    //Evrim Gecir
    public void EvrimGecir()
    {
        if(buElToplananDnaSayisi == 0) return;
        bStats.UpgradedHareketHizi += buElToplananDnaSayisi * hareketHiziArtisMiktari;
        bStats.UpgradedZiplamaLimiti += buElToplananDnaSayisi * yukariHareketArtisMiktari;
        totalToplananDnaSayisi += buElToplananDnaSayisi;
        buElToplananDnaSayisi = 0;
        //Muzik Degistir
        SoundManager.instance.FadeMusicChange(true);
        
        //UI Update
        if(totalToplananDnaSayisi >= 3) UIController.instance.EvrimSecimEkraniAc();
        else UIController.instance.KucukEvrimGecir();
    }

    public void CollectDna()
    {
            buElToplananDnaSayisi++;
            UIController.instance.UpdateEvrimIcinGerekenDnaText(3-buElToplananDnaSayisi);
    }
    
    public void ResetEvrim()
    {
        bStats.UpgradedHareketHizi = 0;
        bStats.UpgradedZiplamaLimiti = 0;
        totalToplananDnaSayisi = 0;
        buElToplananDnaSayisi = 0;
    }
    
    //Respawn player
    public void Respawn()
    {
        player.transform.position = respawnpos.position;
        UIController.instance.UpdateEvrimIcinGerekenDnaText(3-buElToplananDnaSayisi);
    }
}
