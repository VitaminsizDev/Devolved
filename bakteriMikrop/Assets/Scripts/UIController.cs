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
        Sequence sequence = DOTween.Sequence();
        sequence.AppendCallback(() => VignetteAcKapa(true));
        sequence.Append(arkaPlan.DOFade(1, 1f));
        sequence.Append(evrimSecmeEkrani.GetComponent<CanvasGroup>().DOFade(1, 0.2f));
        sequence.Join(evrimSecmeEkrani.transform.DOShakeScale(0.2f, new Vector3(0.1f, 0.1f, 0.1f)));
        sequence.Play();
    }
    
    public void EvrimSecimEkraniKapat()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.AppendCallback(() => evrim.Respawn());
        sequence.AppendCallback(() => VignettePosBul());
        sequence.Append(evrimSecmeEkrani.GetComponent<CanvasGroup>().DOFade(0, 0.2f));
        sequence.Append(arkaPlan.DOFade(0, 0.5f));
        sequence.AppendCallback(() => evrimSecmeEkrani.SetActive(false));
        sequence.AppendCallback(() => VignetteAcKapa(false));
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
        sequence.Append(kucukEvrim.transform.DOScale(new Vector3(1,1,1), 0.75f).SetEase(Ease.OutElastic));
        sequence.Append(kucukEvrim.transform.GetChild(0).GetComponent<CanvasGroup>().DOFade(1, 0.5f));
        sequence.Append(kucukEvrim.transform.GetChild(1).GetComponent<CanvasGroup>().DOFade(1, 0.5f));
        sequence.Append(kucukEvrim.transform.GetChild(2).GetComponent<CanvasGroup>().DOFade(1, 0.5f));
        sequence.AppendInterval(2f);
        sequence.Append(kucukEvrim.transform.DOScale(new Vector3(0,0,0), 0.75f).SetEase(Ease.Linear));
        sequence.AppendCallback(() => evrim.Respawn());
        sequence.AppendCallback(() => VignettePosBul());
        sequence.Append(arkaPlan.DOFade(0, 1f));
        sequence.AppendCallback(() => kucukEvrim.SetActive(false));
        sequence.AppendCallback(() => VignetteAcKapa(false));
        sequence.Append(kucukEvrim.transform.GetChild(2).GetComponent<CanvasGroup>().DOFade(0, 0.2f));
        sequence.Append(kucukEvrim.transform.GetChild(1).GetComponent<CanvasGroup>().DOFade(0, 0.2f));
        sequence.Append(kucukEvrim.transform.GetChild(0).GetComponent<CanvasGroup>().DOFade(0, 0.2f));
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
    
}
