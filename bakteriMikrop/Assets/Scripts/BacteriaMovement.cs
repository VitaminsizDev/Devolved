using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BacteriaMovement : MonoBehaviour
{
    //Fields
    public BacteriaStats stats;
    public float tekrarZiplamaSuresi = 1f;
    public bool canMoveUpward = true;
    public bool canMove = true;
    private float sonYPosition;
    public float suankiYukariGitmeMesafesi = 0;
    public GameObject player;
    private Transform playerTransform => player.transform;
    private Rigidbody2D rb => player.GetComponent<Rigidbody2D>();
    private Vector2 movementDirection;
    
    
    //Get User Input
    void Update()
    {
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        HareketKontrol();
    }
    
    //Movement with rigidbody2D and movementDirection
    void FixedUpdate()
    {
        //If canMoveUpward is true, move on both x and y axis with rigidbody2D.velocity
        if (canMoveUpward && canMove)
        {
            if(movementDirection.y > 0)
            {
                rb.velocity = movementDirection * (stats.baseHareketHizi + stats.UpgradedHareketHizi);
            }
            else
            {
                rb.velocity = new Vector2(movementDirection.x * (stats.baseHareketHizi + stats.UpgradedHareketHizi), rb.velocity.y) ;
            }
        }
        //If canMoveUpward is false, move on y axis with rigidbody2D.velocity
        else
        {
            rb.velocity = new Vector2(movementDirection.x * (stats.baseHareketHizi + stats.UpgradedHareketHizi), rb.velocity.y) ;
        }
    }

    bool IsGrounded()
    {
        if (Physics2D.Raycast(transform.position, Vector2.down, (playerTransform.localScale.y *0.5f + 0.05f), 1 << LayerMask.NameToLayer("Ground")))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    void HareketKontrol()
    {
        //If raycast hits ground layer, set canMoveUpward to true
        if (IsGrounded())
        {
            canMoveUpward = true;
            suankiYukariGitmeMesafesi = 0;
        }
        else
        {
            if(movementDirection.y > 0)  suankiYukariGitmeMesafesi += Mathf.Abs(transform.position.y - sonYPosition);
            if(suankiYukariGitmeMesafesi > (stats.baseZiplamaLimiti + stats.UpgradedZiplamaLimiti))
            {
                canMoveUpward = false;
                suankiYukariGitmeMesafesi = 0;
            }
        }
        
        sonYPosition = transform.position.y;
    }
    
    //Coroutine to wait for tekrarZiplamaSuresi seconds and set canMove to true
    IEnumerator WaitForTekrarZiplama()
    {
        canMove = false;
        yield return new WaitForSeconds(tekrarZiplamaSuresi);
        canMove = true;
    }
    
    


}
