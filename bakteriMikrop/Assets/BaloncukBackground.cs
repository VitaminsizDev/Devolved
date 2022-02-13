using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BaloncukBackground : MonoBehaviour
{
    public Transform player;
    Tween currentTweenUp;
    Tween currentTweenRight;
    float randomXValue;
    float randomYValue;
    
    //Start is called before the first frame update
    void Start()
    {
        Respawn();
    }
    public void MoveUpwardTween()
    {
        //Randomize the X and Y values between 0.5 and 1.5
        randomXValue = Random.Range(0.5f, 1.5f);
        randomYValue = Random.Range(3f, 5f);
        currentTweenUp = transform.DOMoveY(transform.position.y + randomYValue, 1f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental);
        currentTweenRight = transform.DOMoveX(transform.position.x + randomXValue, 1f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        currentTweenUp.Play();
        currentTweenRight.Play();
    }
    
    //Update is called once per frame
    void Update()
    {
        //Check if transform is out of screen
        if (transform.position.y > player.position.y + 15)
        {
            Respawn();
        }
    }
    
    //Respawn the baloncuk at the bottom of the screen in a random x position
    public void Respawn()
    {
        //Kill the tween
        currentTweenUp.Kill();
        currentTweenRight.Kill();
        //Respawn the baloncuk at the bottom of the screen in a random x position
        transform.position = new Vector2(player.position.x + Random.Range(-18, 18), player.position.y - 11f);
        //Move the baloncuk upward
        MoveUpwardTween();
    }
}
