using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PlayerState
{

    protected bool isGrounded;
    protected bool isTouchingWall;
    protected bool isTouchingWallBack;
    public int xInput;
    public int yInput;
    protected Player player;
    protected PlayerStateMachine stateMachine;
    protected BacteriaStats playerData;

    protected bool isAnimationFinished;
    protected bool isExitingState;

    protected float startTime;
    protected Sequence sq;
    private string animBoolName;

    public PlayerState(Player player, PlayerStateMachine stateMachine, BacteriaStats playerData)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.playerData = playerData;


    }

    public virtual void Enter()

    {
       
        DoChecks();

       
        startTime = Time.time;

        isAnimationFinished = false;
        isExitingState = false;

        sq = DOTween.Sequence();
        //player.visual.transform.DOScale(player.size, 0.1f);
    }

    public virtual void Exit()
    {

        isExitingState = true;
      
        sq.Kill();
    }

    public virtual void LogicUpdate()
    {
        xInput = player.InputHandler.NormInputX;
        yInput = player.InputHandler.NormInputY;

    }

    public virtual void PhysicsUpdate()
    {
        DoChecks();
   
    }

    public virtual void DoChecks()
    {
        isGrounded = player.Ground;
        isTouchingWall = player.WallFront;
        isTouchingWallBack = player.WallBack;
    }

    public virtual void AnimationTrigger() { }

    public virtual void AnimationFinishTrigger() => isAnimationFinished = true;


}
