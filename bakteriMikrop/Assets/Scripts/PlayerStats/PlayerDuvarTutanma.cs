using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDuvarTutanma : PlayerState
{
    Vector3 holdPosition;
    int holdfacing;
    float sure;
    public bool tutun;
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
        player.transform.SetParent(player.setparentts2());
        player.buyukziplama.ResetCanDash();
        holdPosition = player.transform.position;
    }

    public override void Exit()
    {
        base.Exit();
        player.transform.SetParent(null);

        player.RB.gravityScale = 1f;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        HoldPosition();
        player.CheckIfShouldFlip(xInput);
        sure += Time.deltaTime;

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
            player.buyukziplama.parenayarla(player.transform.parent);
            stateMachine.ChangeState(player.buyukziplama);
        }
        else if (sure > playerData.Duvartutunmasure + playerData.UpgradedDuvartutunmasure)
        {
            tutun = false;
            stateMachine.ChangeState(player.fallState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    private void HoldPosition()
    {
        player.RB.gravityScale = 0f;
       // player.transform.position = holdPosition;
        holdfacing = player.FacingDirection;
        player.SetVelocityX(0f);
        player.SetVelocityY(0f);
    }
    public bool CheckIfCanTutun()
    {
        return tutun;
    }
    public void ResetTutun()
    {
        tutun = true;
        sure = 0f;
    }

}
