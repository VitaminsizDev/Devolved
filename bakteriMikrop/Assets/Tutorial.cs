using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    
    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canvasGroup.DOFade(0.9f, 1f);
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canvasGroup.DOFade(0f, 1f);
        }
    }
}
