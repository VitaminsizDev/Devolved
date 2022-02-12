using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PlayerIdle : PlayerState
{
    Tween idleTween;
    public PlayerIdle(Player player, PlayerStateMachine stateMachine, BacteriaStats playerData) : base(player, stateMachine, playerData)
    {
        idleTween = player.visual.transform.DOScale(new Vector3(1.1f,1.1f,1f),1f).SetLoops(-1,LoopType.Yoyo).SetEase(Ease.Linear);
        idleTween.Pause();
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
        player.SetVelocityX(0f);
        
        idleTween.Restart();
    }

    public override void Exit()
    {
        idleTween.Pause();
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        xInput = player.InputHandler.NormInputX;
        yInput = player.InputHandler.NormInputY;
        if (!isExitingState)
        {
            if (!isGrounded)
            {
                stateMachine.ChangeState(player.fallState);
            }
            if (xInput != 0)
            {
                stateMachine.ChangeState(player.MoveState);
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
