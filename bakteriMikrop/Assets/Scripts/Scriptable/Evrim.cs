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
    public Volume postProcessing;
    public GameObject player;
    private Vignette vignette;

    private void Awake()
    {
        ResetEvrim();
        //Get vignte effect from post processing
        vignette = postProcessing.profile.components[0] as Vignette;
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

        #region VignettePos

        Vector3 screenPos = Camera.main.WorldToScreenPoint(player.transform.position);
        Vector2 screenPosScale = new Vector2(screenPos.x /Screen.width, screenPos.y/Screen.height);
        vignette.center.value = screenPosScale;

        #endregion
        
        
        DOTween.To(() => vignette.intensity.value, x => vignette.intensity.value = x, 1, 1.3f).OnComplete(Respawn);
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
        
        #region VignettePos

        Vector3 screenPos = Camera.main.WorldToScreenPoint(respawnpos.position);
        Vector2 screenPosScale = new Vector2(screenPos.x /Screen.width, screenPos.y/Screen.height);
        vignette.center.value = screenPosScale;

        #endregion
        player.transform.position = respawnpos.position;
        DOTween.To(() => vignette.intensity.value, x => vignette.intensity.value = x, 0, 0.7f);
        
    }
}
