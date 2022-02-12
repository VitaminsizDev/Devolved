using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFall : PlayerState
{
    public PlayerFall(Player player, PlayerStateMachine stateMachine, BacteriaStats playerData) : base(player, stateMachine, playerData)
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
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.CheckIfShouldFlip(xInput);

        player.SetVelocityX(Mathf.MoveTowards(player.CurrentVelocity.x, playerData.baseHareketHizi * xInput, Time.deltaTime * 5f));


        if (isGrounded)
        {
            stateMachine.ChangeState(player.IdleState);
        }
        else if (player.duvartutun && isTouchingWall && player.FacingDirection == xInput && !player.Ground)
        {
            stateMachine.ChangeState(player.duvarstate);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }



}
