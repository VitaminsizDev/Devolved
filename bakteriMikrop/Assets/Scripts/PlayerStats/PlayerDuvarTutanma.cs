using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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
        player.canvas.SetActive(true);
        player.infoslider.maxValue = playerData.Duvartutunmasure + playerData.UpgradedDuvartutunmasure;
        player.infoslider.value = player.infoslider.maxValue-sure;
        player.infoslider.fillRect.GetComponent<Image>().color = Color.green;
        player.infoslider.fillRect.GetComponent<Image>().DOColor(Color.red, playerData.Duvartutunmasure + playerData.UpgradedDuvartutunmasure);
      //  player.infoslider.fillRect.GetComponent<Image>().color = Color.Lerp(player.infoslider.fillRect.GetComponent<Image>().color, Color.green, Time.deltaTime);
        player.transform.SetParent(player.setparentts2());
        player.buyukziplama.ResetCanDash();
        holdPosition = player.transform.position;
    }

    public override void Exit()
    {
        base.Exit();
        player.transform.SetParent(null);
        player.canvas.SetActive(false);
        player.RB.gravityScale = 1f;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        HoldPosition();
        player.CheckIfShouldFlip(xInput);
       
        if (!isExitingState)
        {
            sure += Time.deltaTime;
            player.infoslider.value -= Time.deltaTime;
            if (sure > playerData.Duvartutunmasure + playerData.UpgradedDuvartutunmasure)
            {
                tutun = false;

            }
            if (player.FacingDirection != holdfacing || !tutun)
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
