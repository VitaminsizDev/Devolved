using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerSuzulme : PlayerState
{
    private float toplamgezme;

    private Vector3 sonPosition;
    Sequence suzulsq;
    public PlayerSuzulme(Player player, PlayerStateMachine stateMachine, BacteriaStats playerData) : base(player, stateMachine, playerData)
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
        suzulsq = DOTween.Sequence();
        suzulsq.Append(player.visual.transform.DOScale(new Vector3(0.95f, 0.95f, 0.95f), 0.1f)).Append(player.visual.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.1f));
        suzulsq.SetLoops(-1, LoopType.Yoyo);
        suzulsq.Play();
        sonPosition = player.transform.position;
        toplamgezme = 0f;
        player.suzulme.Play();

    }

    public override void Exit()
    {
        base.Exit();
        suzulsq.Pause();
        player.suzulme.Stop();
        SoundManager.instance.suzulmesesikapa();

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        SoundManager.instance.suzulmesesi();
        toplamgezme += (playerData.UpgradedHareketHizi + playerData.baseHareketHizi) * Time.deltaTime;





        sonPosition = player.transform.position;


        xInput = player.InputHandler.NormInputX;
        yInput = player.InputHandler.NormInputY;

        player.CheckIfShouldFlip(xInput);



        if (!isExitingState)
        {
            if (toplamgezme >= (playerData.UpgradedZiplamaLimiti + playerData.baseZiplamaLimiti))
            {
                stateMachine.ChangeState(player.fallState);
            }
            else if (player.duvartutun && isTouchingWall && player.FacingDirection == xInput && !player.Ground && player.duvarstate.tutun)
            {
                stateMachine.ChangeState(player.duvarstate);
            }
            else if (player.InputHandler.RawMovementInput.magnitude > 0.1f)
            {
                player.SetVelocity(playerData.UpgradedHareketHizi + playerData.baseHareketHizi, new Vector2(xInput, yInput));

            }
            else if (player.InputHandler.RawMovementInput.magnitude < 0.1)
            {
                stateMachine.ChangeState(player.fallState);
            }
        }


    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
