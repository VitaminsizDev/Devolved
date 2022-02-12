using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BakteriStat", menuName = "Bakteri")]
public class BacteriaStats : ScriptableObject
{   
    public float baseHareketHizi;
    public float baseZiplamaLimiti;
    
    public float UpgradedHareketHizi;
    public float UpgradedZiplamaLimiti;
    [Header("BuyukZiplama")]
    public float buyukziplamavelo;
    public float dashTime;
    public float maxHoldTime;
    public float dashCooldown;
    public float dashEndYMultiplier;
    public float drag;
    [Header("CollisionSenses")]
    public LayerMask whatIsGround;
    public float groundCheckRadius;
    public float wallCheckDistance;


}
