using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using DG.Tweening;
using UnityEngine.Rendering;

public class Evrim : MonoBehaviour
{
    public int totalToplananDnaSayisi = 0;
    public int buElToplananDnaSayisi = 0;
    public float hareketHiziArtisMiktari = 0.1f;
    public float yukariHareketArtisMiktari = 0.1f;
    public float yukariTutunmasMiktariArtisMiktari = 0.1f;
    public float buyukziplamaVeloArtisMiktari = 0.1f;

    public float birikenpuanHiziArtisMiktari=0f;
    public float birikenpuanHareketArtisMiktari = 0f;
    public float birikenpuanduvarTutunmasMiktari = 0f;
    public float birikenBuyukZiplamaMiktariArtisMiktari = 0f;

    public Transform respawnpos;
    public BacteriaStats bStats;
    public GameObject player;
    public CanvasGroup arkaPlan;
    private void Awake()
    {
        
        ResetEvrim();
    }

    //Start
    private void Start()
    {
        bStats.UpgradedHareketHizi = 0f;
        bStats.UpgradedZiplamaLimiti = 0f;
        bStats.UpgradedDuvartutunmasure=0f;
        bStats.buyukziplamaUpragevelo = 0f;
        UIController.instance.UpdateEvrimIcinGerekenDnaText(3-buElToplananDnaSayisi);

    }
    //Call EvrimGecir on space key
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    EvrimGecir();
        //}
    }
    
    //Evrim Gecir
    public void EvrimGecir()
    {
        if(buElToplananDnaSayisi == 0) return;
        bStats.UpgradedHareketHizi += buElToplananDnaSayisi * hareketHiziArtisMiktari;
        bStats.UpgradedZiplamaLimiti += buElToplananDnaSayisi * yukariHareketArtisMiktari;
        totalToplananDnaSayisi += buElToplananDnaSayisi;
        buElToplananDnaSayisi = 0;
        
        
        //UI Update
        if (totalToplananDnaSayisi >= 3)
        {
            //Muzik Degistir
            SoundManager.instance.FadeMusicChange(true);
            UIController.instance.EvrimSecimEkraniAc();
        }
        else
        {
            
            UIController.instance.KucukEvrimGecir();
        }
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
    public void oldun()
    {
        player.GetComponent<Player>().StateMachine.ChangeState(player.GetComponent<Player>().beklestate);
        arkaPlan.DOFade(1f, 0.4f).OnComplete(() =>
        {
            player.transform.position = respawnpos.position;
            arkaPlan.DOFade(0, 0.4f).OnComplete(() =>
            {
                bStats.UpgradedHareketHizi += birikenpuanHareketArtisMiktari;
                bStats.UpgradedZiplamaLimiti += birikenpuanHareketArtisMiktari;
                bStats.UpgradedDuvartutunmasure += birikenpuanduvarTutunmasMiktari;
                bStats.buyukziplamaUpragevelo += birikenBuyukZiplamaMiktariArtisMiktari;

                birikenpuanHareketArtisMiktari = 0f;
                yukariHareketArtisMiktari = 0f;
                birikenpuanduvarTutunmasMiktari = 0f;
                birikenBuyukZiplamaMiktariArtisMiktari = 0f;
               
                player.GetComponent<Player>().StateMachine.ChangeState(player.GetComponent<Player>().IdleState);
            });
        });
     
    }
    public void Yemek()
    {
        birikenpuanHareketArtisMiktari += hareketHiziArtisMiktari;
        birikenpuanHareketArtisMiktari += yukariHareketArtisMiktari;
      
        if (player.GetComponent<Player>().buyukzipla)
        {
            birikenBuyukZiplamaMiktariArtisMiktari += buyukziplamaVeloArtisMiktari;
        }   
        if (player.GetComponent<Player>().duvartutun)
        {
            birikenpuanduvarTutunmasMiktari += yukariTutunmasMiktariArtisMiktari;
        }
    }
}
