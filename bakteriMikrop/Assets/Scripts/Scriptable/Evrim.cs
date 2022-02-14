using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using DG.Tweening;
using UnityEngine.Rendering;
using TMPro;
[System.Serializable]
public class evrim
{
    public int yemekSayisi;
    public Sprite sprite;
    public Vector2 size;
}
public class Evrim : MonoBehaviour
{
    public int totalToplananDnaSayisi = 0;
    public int buElToplananDnaSayisi = 0;
    public float hareketHiziArtisMiktari = 0.1f;
    public float yukariHareketArtisMiktari = 0.1f;
    public float yukariTutunmasMiktariArtisMiktari = 0.1f;
    public float buyukziplamaVeloArtisMiktari = 0.1f;

    public float birikenpuanYukariHiziArtisMiktari = 0f;
    public float birikenpuanHareketArtisMiktari = 0f;
    public float birikenpuanduvarTutunmasMiktari = 0f;
    public float birikenBuyukZiplamaMiktariArtisMiktari = 0f;

    public Transform respawnpos;
    public BacteriaStats bStats;
    public GameObject player;
    public CanvasGroup arkaPlan;

    public List<evrim> birinciEvrim;
    public List<evrim> ikinciEvrim;
    public List<evrim> sonEvrim;

    public evrim simdikievrim;
    public int toplamyemek;
    public int evrimno = 1;

    public Transform bitisnoktasi;
    public Sprite bitissprite;
    public Color birisrengi;
    public GameObject evrimbtn;
    public TextMeshProUGUI textMesh;
    private void Awake()
    {

        ResetEvrim();
        simdikievrim = birinciEvrim[0];
        textMesh.gameObject.SetActive(false);
    }
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    EvrimGecir();
        //}
    }

    //Start
    private void Start()
    {
        toplamyemek = 0;
        bStats.UpgradedHareketHizi = 0f;
        bStats.UpgradedZiplamaLimiti = 0f;
        bStats.UpgradedDuvartutunmasure = 0f;
        bStats.buyukziplamaUpragevelo = 0f;
        UIController.instance.UpdateEvrimIcinGerekenDnaText(3 - buElToplananDnaSayisi);

    }
    bool evrim2;
    bool evrim3;
    //Evrim Gecir
    public void EvrimGecir()
    {
        evrim secilenevrim = new evrim();
        if (!acildurum)
        {
            if (birikenpuanHareketArtisMiktari == 0)
            {
               
                oldun();

                return;
            }
        }

        acildurum = false;
        int secilen = 0;
        if (evrimno == 1)
        {
            for (int i = 0; i < birinciEvrim.Count; i++)
            {
                if (toplamyemek >= birinciEvrim[i].yemekSayisi)
                {
                    secilen = i;
                }
            }
            secilenevrim = birinciEvrim[secilen];
            player.transform.SetParent(null);
            player.GetComponent<Player>().size = birinciEvrim[secilen].size;
            player.GetComponent<Player>().visual.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = birinciEvrim[secilen].sprite;
            //  player.GetComponent<BoxCollider2D>().size= birinciEvrim[secilen].size;
            player.transform.localScale = birinciEvrim[secilen].size;
        }
        else if (evrimno == 2)
        {
            if (!evrim2)
            {
                textMesh.gameObject.SetActive(true);
                textMesh.text = "Ziplama Acildý Space Tusu";
                evrim2 = true;
            }
            else
            {
                textMesh.gameObject.SetActive(false);

            }
            for (int i = 0; i < ikinciEvrim.Count; i++)
            {
                if (toplamyemek >= ikinciEvrim[i].yemekSayisi)
                {
                    secilen = i;
                }
            }
            secilenevrim = ikinciEvrim[secilen];
            player.transform.SetParent(null);
            player.GetComponent<Player>().size = ikinciEvrim[secilen].size;
            player.GetComponent<Player>().buyukzipla = true;
            player.GetComponent<Player>().visual.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = ikinciEvrim[secilen].sprite;
            //  player.GetComponent<BoxCollider2D>().size= birinciEvrim[secilen].size;
            player.transform.localScale = ikinciEvrim[secilen].size;
        }
        else if (evrimno == 3)
        {
            if (!evrim3)
            {
                textMesh.gameObject.SetActive(true);
                textMesh.text = "Duvara Tutunma Acildi";
                evrim3 = true;
            }
            else
            {
                textMesh.gameObject.SetActive(false);
            }
            UIController.instance.evrimIcinGerekenDnaText.gameObject.SetActive(false);
            for (int i = 0; i < sonEvrim.Count; i++)
            {
                if (toplamyemek >= ikinciEvrim[i].yemekSayisi)
                {
                    secilen = i;
                }
            }
            secilenevrim = sonEvrim[secilen];
            player.transform.SetParent(null);
            player.GetComponent<Player>().size = sonEvrim[secilen].size;
            player.GetComponent<Player>().buyukzipla = true;
            player.GetComponent<Player>().duvartutun = true;
            player.GetComponent<Player>().visual.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sonEvrim[secilen].sprite;
            //  player.GetComponent<BoxCollider2D>().size= birinciEvrim[secilen].size;
            player.transform.localScale = sonEvrim[secilen].size;
        }
        if (simdikievrim != secilenevrim)
        {
            olumsayisi++;
            UIController.instance.UpdateJenarasyon(olumsayisi);
            UIController.instance.PokemonAnimasyon(simdikievrim.sprite, secilenevrim.sprite);
            simdikievrim = secilenevrim;
        }
        else
        {
            // UIController.instance.KucukEvrimGecir();
            oldun();
            //olumsayisi++;
            //UIController.instance.UpdateJenarasyon(olumsayisi);
        }
        //olumsayisi++;
        //UIController.instance.UpdateJenarasyon(olumsayisi);
        player.GetComponent<Player>().StateMachine.ChangeState(player.GetComponent<Player>().beklestate);

        bStats.UpgradedHareketHizi += birikenpuanHareketArtisMiktari;
        bStats.UpgradedZiplamaLimiti += birikenpuanYukariHiziArtisMiktari;
        bStats.UpgradedDuvartutunmasure += birikenpuanduvarTutunmasMiktari;
        bStats.buyukziplamaUpragevelo += birikenBuyukZiplamaMiktariArtisMiktari;

        birikenpuanHareketArtisMiktari = 0f;
        birikenpuanYukariHiziArtisMiktari = 0f;
        birikenpuanduvarTutunmasMiktari = 0f;
        birikenBuyukZiplamaMiktariArtisMiktari = 0f;
        UIController.instance.UpdateEvrimIcinGerekenDnaText(3 - buElToplananDnaSayisi);
        player.GetComponent<Player>().StateMachine.ChangeState(player.GetComponent<Player>().IdleState);
        // UIController.instance.KucukEvrimGecir();
    }

    //Check Evrim
    public void CheckEvrim()
    {
        //if (buElToplananDnaSayisi >= 3)
        //{
        //    evrimno++;
        //    //Muzik Degistir
        //    SoundManager.instance.FadeMusicChange(true);
        //    UIController.instance.EvrimSecimEkraniAc();
        //    buElToplananDnaSayisi = 0;

        //}
        //else
        //{
        //    UIController.instance.UpdateEvrimIcinGerekenDnaText(3 - buElToplananDnaSayisi);
        //}
    }
    bool acildurum = false;
    public void CollectDna()
    {
        buElToplananDnaSayisi++;
        if (buElToplananDnaSayisi >= 3)
        {
            evrimno++;
            acildurum = true;
        }

        UIController.instance.UpdateEvrimIcinGerekenDnaText(3 - buElToplananDnaSayisi);
        buElToplananDnaSayisi %= 3;
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
        UIController.instance.UpdateEvrimIcinGerekenDnaText(3 - buElToplananDnaSayisi);
    }
    public int olumsayisi = 1;
    public void oldun()
    {
        olumsayisi++;
        UIController.instance.UpdateJenarasyon(olumsayisi);

        player.GetComponent<Player>().StateMachine.ChangeState(player.GetComponent<Player>().beklestate);
        arkaPlan.DOFade(1f, 0.4f).OnComplete(() =>
        {
            player.transform.position = respawnpos.position;
            arkaPlan.DOFade(0, 0.4f).OnComplete(() =>
            {
                player.GetComponent<Player>().StateMachine.ChangeState(player.GetComponent<Player>().IdleState);
            });
        });
    }
    public void oyunbitis()
    {
        evrimbtn.SetActive(false);
        UIController.instance.evrimIcinGerekenDnaText.gameObject.SetActive(false);
        player.GetComponent<Player>().StateMachine.ChangeState(player.GetComponent<Player>().beklestate);
        player.transform.DOMove(bitisnoktasi.position, 10f);
        arkaPlan.DOFade(1, 10f);
        arkaPlan.GetComponent<Image>().color = Color.white;
        arkaPlan.GetComponent<Image>().DOColor(birisrengi, 10f);
        StartCoroutine(senmasege());
    }
    IEnumerator senmasege()
    {
        yield return new WaitForSeconds(10f);
        UIController.instance.PokemonAnimasyon(simdikievrim.sprite, bitissprite);
        yield return new WaitForSeconds(6f);
        //bitis Ekrani buraya  yaz
        SceneManager.LoadScene(1);
    }
    public void Yemek()
    {
        toplamyemek++;
        birikenpuanHareketArtisMiktari += hareketHiziArtisMiktari;
        birikenpuanYukariHiziArtisMiktari += yukariHareketArtisMiktari;

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
