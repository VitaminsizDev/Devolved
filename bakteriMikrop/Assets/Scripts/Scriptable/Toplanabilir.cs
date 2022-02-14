using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toplanabilir : MonoBehaviour
{
    public bool isDna = true;
    public bool sonDNA = false;
    //Private Evrim find in scene GameObject Scripts
    private Evrim evrim => FindObjectOfType<Evrim>();
    public GameObject yemekparticle;
    public GameObject DNAparticle;
    
    //OnTriggerEnter2D is called whenever an object enters a trigger collider attached to this object (2D physics only).
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (isDna)
            {
                evrim.CollectDna();
                Camera.main.GetComponent<RipplePostProcessor>().Ripple();
                SoundManager.instance.PlayDnaToplamaSesi();
                Instantiate(DNAparticle, transform.position, Quaternion.identity);
                //this.gameObject.SetActive(false);
            }
            else if (sonDNA)
            {
                SoundManager.instance.PlayDnaToplamaSesi();
                evrim.oyunbitis();
            }
            else
            {
                SoundManager.instance.PlayDnaToplamaSesi();
                evrim.Yemek();
                Instantiate(yemekparticle,transform.position,Quaternion.identity);
            }
            this.gameObject.SetActive(false);
        }
    }
}
