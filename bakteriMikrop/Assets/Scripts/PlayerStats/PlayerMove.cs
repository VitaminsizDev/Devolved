using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PlayerMove : PlayerState
{
    Tween moveTweener;
    public PlayerMove(Player player, PlayerStateMachine stateMachine, BacteriaStats playerData) : base(player, stateMachine, playerData)
    {
        moveTweener = player.visual.transform.DOScale(new Vector3(1f -xInput*0.9f,1f +yInput*0.9f,1f),1f).SetLoops(-1,LoopType.Yoyo).SetEase(Ease.Linear);
        //Dont play moveTweener on start
        moveTweener.Pause();
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void DoChecks()
    {
        base.DoChecks(); 
    }

    public override void Enter()
    {
        base.Enter();
        player.duvarstate.ResetTutun();
        player.buyukziplama.ResetCanDash();
        moveTweener.Restart();

    }

    public override void Exit()
    {
        moveTweener.Pause();
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        xInput = player.InputHandler.NormInputX;
        yInput = player.InputHandler.NormInputY;

        player.CheckIfShouldFlip(xInput);

        player.SetVelocityX((playerData. baseHareketHizi+playerData.UpgradedHareketHizi) * xInput);

        if (!isExitingState)
        {
            if (!isGrounded)
            {
                stateMachine.ChangeState(player.fallState);
            }
          
            if (xInput == 0)
            {
                stateMachine.ChangeState(player.IdleState);
            }
            else if (yInput == 1)
            {
                stateMachine.ChangeState(player.suzulmeState);
            }
            else if (player.InputHandler.dashInput && player.buyukzipla && player.buyukziplama.CheckIfCanDash())
            {
                stateMachine.ChangeState(player.buyukziplama);
            }
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }


}
