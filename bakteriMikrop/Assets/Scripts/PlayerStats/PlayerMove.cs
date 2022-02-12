using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PlayerMove : PlayerState
{

    public PlayerMove(Player player, PlayerStateMachine stateMachine, BacteriaStats playerData) : base(player, stateMachine, playerData)
    {
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
        player.buyukziplama.ResetCanDash();
        sq.Append(player.visual.transform.DOScale(new Vector3(0.85f, 1f, 1f), 0.5f)).Append(player.visual.transform.DOScale(new Vector3(1.15f, 1f, 1f), 0.5f)).SetLoops(-1, LoopType.Yoyo); 
   
    }

    public override void Exit()
    {
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
