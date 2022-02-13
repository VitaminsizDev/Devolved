using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerFall : PlayerState
{
    public bool startcoyete;
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
        player.TarilEffect.SetActive(true);
    }

    public override void Exit()
    {
        base.Exit();
        player.TarilEffect.SetActive(false);
        startcoyete = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!isExitingState)
        {
            player.CheckIfShouldFlip(xInput);
            if (player.CurrentVelocity.y<0f)
            {
                player.visual.transform.DOScaleY(Mathf.MoveTowards(player.visual.transform.localScale.y, 1.3f, Time.deltaTime), Time.deltaTime);
                player.visual.transform.DOScaleX(Mathf.MoveTowards(player.visual.transform.localScale.x, 0.6f, Time.deltaTime), Time.deltaTime);
            }
           
            player.SetVelocityX(Mathf.MoveTowards(player.CurrentVelocity.x, playerData.baseHareketHizi * xInput, Time.deltaTime * 5f));
            if (startcoyete && Time.time < startTime + playerData.coyoteTime && yInput == 1)
            {
                stateMachine.ChangeState(player.suzulmeState);
            }

            else if (isGrounded)
            {
                player.land.Play();
                player.visual.transform.DOPunchScale(Vector3.one * 0.5f, 0.1f, 20, 5);
                stateMachine.ChangeState(player.IdleState);
            }
            else if (player.duvartutun && isTouchingWall && player.FacingDirection == xInput && !player.Ground && player.duvarstate.tutun)
            {
                stateMachine.ChangeState(player.duvarstate);
            }
        }
    }
       

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }



}
