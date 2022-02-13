using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class olduren : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameObject.FindObjectOfType<Evrim>().oldun();
        }
    }
}
