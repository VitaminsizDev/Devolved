using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class olduren : MonoBehaviour
{
    Evrim evrim;
    private void Awake()
    {
        evrim = GameObject.FindObjectOfType<Evrim>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("öldün");
            evrim.EvrimGecir();
        }
    }
}
