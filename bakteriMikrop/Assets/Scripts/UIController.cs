using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class UIController : MonoBehaviour
{
    //SingleTon
    public static UIController instance;
    
    [Header("Script Referanslar")]
    public GameObject player;
    public Evrim evrim;
    public Volume postProcessing;
    
    //Public
    public TextMeshProUGUI evrimIcinGerekenDnaText;
    public TMP_ColorGradient evrimIcinGerekenDnaColor;
    public GameObject evrimSecmeEkrani;
    public GameObject kucukEvrim;
    public CanvasGroup arkaPlan;
    public GameObject pokemonPanel;
    
    private Tween evrimHazirSallaDnaTween;
    private Tween evrimPaneliHepAcikTween;
    
    private Vignette vignette;

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
        
        //Setup Tweens
        evrimHazirSallaDnaTween = evrimIcinGerekenDnaText.transform.DORotate(new Vector3(0,0,-6f), 0.45f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutElastic);
        evrimPaneliHepAcikTween = evrimIcinGerekenDnaText.transform.DOScale(new Vector3(1.05f, 1.05f, 1.05f), 0.45f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        evrimPaneliHepAcikTween.Restart();
        
        //Setup Vignette
        vignette = postProcessing.profile.components[0] as Vignette;
    }
    
    //Update
    private void Update()
    {
        //On ESC Button Click call pokemonAnimasyon
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PokemonAnimasyon();
        }
    }
    
    //
    public void UpdateEvrimIcinGerekenDnaText(int dna)
    {
        //If tween is playing, stop it
        if (evrimHazirSallaDnaTween.IsPlaying())
        {
            evrimHazirSallaDnaTween.Pause();
            // Reset rotation
            evrimIcinGerekenDnaText.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        
        evrimIcinGerekenDnaText.transform.DOPunchScale(new Vector3(0.2f, 0.2f, 0.2f), 0.5f);
        evrimIcinGerekenDnaText.color = new Color32(23, 190, 187, 255);
        evrimIcinGerekenDnaText.colorGradientPreset = null;
        evrimIcinGerekenDnaText.text = "Evrime "+dna.ToString()+" DNA Daha";
        if(dna <= 0)
        {
            //If evrimHazirSallaDnaTween is not playing, play it
            if(!evrimHazirSallaDnaTween.IsPlaying())
            {
                evrimIcinGerekenDnaText.transform.rotation = Quaternion.Euler(0,0,6f);
                evrimHazirSallaDnaTween.Restart();
            }
            
            //Toggle color gradient on evrimIcinGerekenDnaText
            evrimIcinGerekenDnaText.colorGradientPreset = evrimIcinGerekenDnaColor;
            evrimIcinGerekenDnaText.color = Color.white;
            evrimIcinGerekenDnaText.text = "Evrim Hazır!";
        }
    }
    
    //Evrim Secim Ekrani
    public void EvrimSecimEkraniAc()
    {
        evrimSecmeEkrani.transform.rotation = Quaternion.Euler(0,0,-1f);
        Tween panelRotate = evrimSecmeEkrani.transform.DORotate(new Vector3(0,0,0.05f), 3f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        Tween panelScale = evrimSecmeEkrani.transform.DOScale(new Vector3(1.02f, 1.02f, 1.02f), 1.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        panelRotate.Restart();
        panelScale.Restart();
        evrimIcinGerekenDnaText.text = "Evrim Zamanı!!";
        evrimSecmeEkrani.SetActive(true);
        
        Transform secmePanel = evrimSecmeEkrani.transform.GetChild(0).Find("SecmePanel");
        Transform secButon = evrimSecmeEkrani.transform.Find("EvrimSecButon");
        
        //Sequence
        Sequence sequence = DOTween.Sequence();
        sequence.AppendCallback(() => VignetteAcKapa(true));
        sequence.Append(arkaPlan.DOFade(1, 1f));
        sequence.AppendCallback(() => SoundManager.instance.PlayBuyukEvrilmeSesi());
        sequence.AppendCallback(() => secmePanel.gameObject.SetActive(true));
        sequence.Join(evrimSecmeEkrani.GetComponent<CanvasGroup>().DOFade(1, 0.2f));
        sequence.Join(secmePanel.GetComponent<CanvasGroup>().DOFade(1, 0.2f));
        sequence.Join(evrimSecmeEkrani.transform.DOShakeScale(0.2f, new Vector3(0.1f, 0.1f, 0.1f)));
        sequence.AppendCallback(() => secButon.gameObject.SetActive(true));
        sequence.Play();
    }
    
    public void EvrimSecimEkraniKapat()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.AppendCallback(() => evrim.Respawn());
        sequence.AppendCallback(() => VignettePosBul());
        sequence.AppendCallback(() => SoundManager.instance.PlayGeriKapanmaSesi());
        sequence.Join(evrimSecmeEkrani.GetComponent<CanvasGroup>().DOFade(0, 0.2f));
        sequence.AppendCallback(() => VignetteAcKapa(false));
        sequence.Join(arkaPlan.DOFade(0, 0.5f));
        sequence.AppendCallback(() => evrimSecmeEkrani.SetActive(false));
        
        sequence.Play();
    }

    public void KucukEvrimGecir()
    {
        evrimIcinGerekenDnaText.text = "Evrim Zamanı!!";
        kucukEvrim.SetActive(true);
        
        //Sequence
        Sequence sequence = DOTween.Sequence();
        sequence.AppendCallback(() => VignetteAcKapa(true));
        sequence.Append(arkaPlan.DOFade(1, 1f));
        sequence.AppendCallback(() => SoundManager.instance.PlayKucukEvrilmeSesi());
        sequence.Join(kucukEvrim.transform.DOScale(new Vector3(1,1,1), 0.75f).SetEase(Ease.OutElastic));
        sequence.AppendCallback(() => SoundManager.instance.PlayPopSesi());
        sequence.Join(kucukEvrim.transform.GetChild(0).GetComponent<CanvasGroup>().DOFade(1, 0.5f));
        sequence.AppendCallback(() => SoundManager.instance.PlayPopSesi());
        sequence.Join(kucukEvrim.transform.GetChild(1).GetComponent<CanvasGroup>().DOFade(1, 0.5f));
        sequence.AppendCallback(() => SoundManager.instance.PlayPopSesi());
        sequence.Join(kucukEvrim.transform.GetChild(2).GetComponent<CanvasGroup>().DOFade(1, 0.5f));
        
        sequence.AppendInterval(2f);
        sequence.AppendCallback(() => SoundManager.instance.PlayGeriKapanmaSesi());
        sequence.Join(kucukEvrim.transform.DOScale(new Vector3(0,0,0), 0.75f).SetEase(Ease.Linear));
        sequence.AppendCallback(() => evrim.Respawn());
        sequence.AppendCallback(() => VignettePosBul());
        sequence.AppendCallback(() => VignetteAcKapa(false));
        sequence.Join(arkaPlan.DOFade(0, 1f));
        sequence.AppendCallback(() => kucukEvrim.SetActive(false));
        sequence.Append(kucukEvrim.transform.GetChild(2).GetComponent<CanvasGroup>().DOFade(0, 0.2f));
        sequence.Append(kucukEvrim.transform.GetChild(1).GetComponent<CanvasGroup>().DOFade(0, 0.2f));
        sequence.Append(kucukEvrim.transform.GetChild(0).GetComponent<CanvasGroup>().DOFade(0, 0.2f));
        sequence.AppendCallback(() => player.GetComponent<Player>().StateMachine.ChangeState(player.GetComponent<Player>().IdleState));
        sequence.AppendCallback(() => evrim.CheckEvrim());
        sequence.Play();
    }
    
    public void ClickEffect(Transform target)
    {
        target.DOShakeScale(0.2f, new Vector3(0.15f, 0.15f, 0.15f));
    }
    
    //Vignette Pos Bul
    public void VignettePosBul()
    {
        #region VignettePos

        Vector3 screenPos = Camera.main.WorldToScreenPoint(player.transform.position);
        Vector2 screenPosScale = new Vector2(screenPos.x /Screen.width, screenPos.y/Screen.height);
        vignette.center.value = screenPosScale;

        #endregion
    }
    
    public void VignetteAcKapa(bool ac)
    {
        VignettePosBul();
        if(ac) DOTween.To(() => vignette.intensity.value, x => vignette.intensity.value = x, 1, 1.3f);
        else DOTween.To(() => vignette.intensity.value, x => vignette.intensity.value = x, 0, 0.7f);
    }

    public void PokemonAnimasyon()
    {
        /*Tween panelScale = evrimSecmeEkrani.transform.DOScale(new Vector3(1.05f, 1.05f, 1), 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        Tween panelRotate = evrimSecmeEkrani.transform.DORotate(new Vector3(0, 0, 3), 2).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        panelScale.Pause();
        panelRotate.Pause();*/
        //Find child with name "SecmePanel"
        Transform secmePanel = evrimSecmeEkrani.transform.GetChild(0).Find("SecmePanel");
        Transform pokemonAnimPanel = evrimSecmeEkrani.transform.GetChild(0).Find("PokemonAnim");
        Transform secButon = evrimSecmeEkrani.transform.Find("EvrimSecButon");
        
        
        var suankiPokemon = pokemonPanel.transform.GetChild(0).GetComponent<RectTransform>();
        var upgradePokemon = pokemonPanel.transform.GetChild(1).GetComponent<RectTransform>();
        Sequence sequence = DOTween.Sequence();
        sequence.AppendCallback(() => pokemonAnimPanel.gameObject.SetActive(true));
        sequence.AppendCallback(() => secButon.gameObject.SetActive(false));
        sequence.Append(secmePanel.GetComponent<CanvasGroup>().DOFade(0, 0.5f));
        sequence.Join(pokemonAnimPanel.GetComponent<CanvasGroup>().DOFade(1, 0.5f));
        sequence.AppendCallback(() => secmePanel.gameObject.SetActive(false));
        sequence.AppendCallback(() => SoundManager.instance.PlayPokemonBaslangicSesi());
        sequence.Join(suankiPokemon.DOLocalMove(Vector2.zero, 2f).SetEase(Ease.OutBounce));
        sequence.Join(upgradePokemon.DOLocalMove(Vector2.zero, 2f).SetEase(Ease.OutBounce));
        sequence.Join(upgradePokemon.DOScale(new Vector3(0,0,0), 2.5f).SetEase(Ease.InFlash));
        sequence.Join(suankiPokemon.DOScale(new Vector3(0,0,0), 2.5f).SetEase(Ease.InFlash));
        sequence.AppendCallback(() => suankiPokemon.gameObject.SetActive(false));
        sequence.AppendCallback(() => SoundManager.instance.PlayPokemonBitmeSesi());
        sequence.Join(upgradePokemon.DOScale(new Vector3(1,1,0), 0.3f).SetEase(Ease.Linear));
        /*sequence.Append(evrimSecmeEkrani.transform.DORotate(new Vector3(0, 0, -3), 1).SetEase(Ease.Linear));
        sequence.AppendCallback(() => panelScale.Play());
        sequence.AppendCallback(() => panelRotate.Play());*/
        sequence.AppendInterval(1.5f);
        sequence.AppendCallback(() => EvrimSecimEkraniKapat());
        sequence.Join(pokemonAnimPanel.GetComponent<CanvasGroup>().DOFade(1, 0.2f));
        sequence.AppendCallback(() => pokemonAnimPanel.gameObject.SetActive(false));
        sequence.Play();

    }
}
