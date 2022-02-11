using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class UIController : MonoBehaviour
{
    //SingleTon
    public static UIController instance;
    
    //Public
    public TextMeshProUGUI evrimIcinGerekenDnaText;
    public TMP_ColorGradient evrimIcinGerekenDnaColor;
    
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
    }
    
    //
    public void UpdateEvrimIcinGerekenDnaText(int dna)
    {
        evrimIcinGerekenDnaText.transform.DOPunchScale(new Vector3(0.2f, 0.2f, 0.2f), 0.5f);
        evrimIcinGerekenDnaText.color = new Color32(23, 190, 187, 255);
        evrimIcinGerekenDnaText.colorGradientPreset = null;
        evrimIcinGerekenDnaText.text = "Evrime "+dna.ToString()+" DNA Daha";
        if(dna <= 0)
        {
            //Toggle color gradient on evrimIcinGerekenDnaText
            evrimIcinGerekenDnaText.colorGradientPreset = evrimIcinGerekenDnaColor;
            evrimIcinGerekenDnaText.color = Color.white;
            evrimIcinGerekenDnaText.text = "Evrim Hazır!";
        }
    }
}
