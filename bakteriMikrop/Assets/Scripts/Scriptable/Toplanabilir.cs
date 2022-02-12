using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toplanabilir : MonoBehaviour
{
    public bool isDna = true;
    //Private Evrim find in scene GameObject Scripts
    private Evrim evrim => FindObjectOfType<Evrim>();
    
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
                this.gameObject.SetActive(false);
            }
        }
    }
}
