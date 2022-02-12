using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDuvarTutanma : PlayerState
{
    Vector3 holdPosition;
    int holdfacing;
    public PlayerDuvarTutanma(Player player, PlayerStateMachine stateMachine, BacteriaStats playerData) : base(player, stateMachine, playerData)
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
        holdPosition = player.transform.position;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        HoldPosition();
        player.CheckIfShouldFlip(xInput);
        if (player.FacingDirection != holdfacing)
        {
            stateMachine.ChangeState(player.fallState);
        }
        else if (yInput == 1)
        {
            stateMachine.ChangeState(player.suzulmeState);
        }
        else if (player.InputHandler.dashInput && player.buyukzipla && player.buyukziplama.CheckIfCanDash())
        {
            player.buyukziplama.facing = holdfacing;
            stateMachine.ChangeState(player.buyukziplama);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    private void HoldPosition()
    {
        player.transform.position = holdPosition;
        holdfacing = player.FacingDirection;
        player.SetVelocityX(0f);
        player.SetVelocityY(0f);
    }

}