using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BekleState : PlayerState
{
    public BekleState(Player player, PlayerStateMachine stateMachine, BacteriaStats playerData) : base(player, stateMachine, playerData)
    {
    }
    public override void Enter()
    {
        base.Enter();
        player.SetVelocityZero();
        player.RB.gravityScale = 0f;
    }
    public override void Exit()
    {
        base.Exit();
        player.RB.gravityScale = 1f;
    }

}
