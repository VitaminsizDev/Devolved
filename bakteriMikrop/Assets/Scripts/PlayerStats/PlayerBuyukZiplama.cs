using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PlayerBuyukZiplama : PlayerState
{
    public bool CanDash { get; private set; }
    private bool isHolding;
    private bool dashInputStop;

    private float lastDashTime;

    private Vector2 dashDirection;
    private Vector2 dashDirectionInput;
    private Vector2 lastAIPos;

    private bool isAbilityDone = false;
    public int facing=0;
    public PlayerBuyukZiplama(Player player, PlayerStateMachine stateMachine, BacteriaStats playerData) : base(player, stateMachine, playerData)
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
        if (facing==0)
        {
            sq.Append(player.visual.transform.DOScale(new Vector3(1.5f, 0.5f, 1f), playerData.maxHoldTime));
            sq.Join(player.visual.transform.DOLocalMoveY(-0.5f, playerData.maxHoldTime));
        }
        else if (facing==1)
        {
            sq.Append(player.visual.transform.DOScale(new Vector3(0.5f, 1.5f, 1f), playerData.maxHoldTime));
            sq.Join(player.visual.transform.DOLocalMoveX(0.5f, playerData.maxHoldTime));
        }
        else if (facing == -1)
        {
            sq.Append(player.visual.transform.DOScale(new Vector3(0.5f, 1.5f, 1f), playerData.maxHoldTime));
            sq.Join(player.visual.transform.DOLocalMoveX(0.5f, playerData.maxHoldTime));
        }
       
        player.SetVelocityZero();
        player.DashDirectionIndicator.gameObject.SetActive(true);
        isAbilityDone = false;
        CanDash = false;
        isHolding = true;
        dashDirection = Vector2.right * player.FacingDirection;
       
    }

    public override void Exit()
    {
        base.Exit();
        player.visual.transform.localPosition = Vector3.zero;
        facing = 0;
        if (player.CurrentVelocity.y > 0)
        {
            //player.SetVelocityY(player.CurrentVelocity.y * playerData.dashEndYMultiplier);
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAbilityDone)
        {
            stateMachine.ChangeState(player.fallState);
        }
        if (!isExitingState)
        {

            


            if (isHolding)
            {
                player.SetVelocityZero();
                dashDirectionInput = player.InputHandler.DashDirectionInput;
                dashInputStop = player.InputHandler.DashInputStop;

                if (dashDirectionInput != Vector2.zero)
                {
                    dashDirection = dashDirectionInput;
                    dashDirection.Normalize();
                }

                float angle = Vector2.SignedAngle(Vector2.right, dashDirection);
                player.DashDirectionIndicator.rotation = Quaternion.Euler(0f, 0f, angle - 45f);

                if (dashInputStop || Time.time >= startTime + playerData.maxHoldTime)
                {
                    isHolding = false;
                  
                    startTime = Time.time;
                    player.CheckIfShouldFlip(Mathf.RoundToInt(dashDirection.x));
                    player.RB.drag = 0;
                    player.SetVelocity(playerData.buyukziplamavelo, dashDirection);
                    player.DashDirectionIndicator.gameObject.SetActive(false);
                   
                }
            }
            else
            {
                player.SetVelocity(playerData.buyukziplamavelo, dashDirection);
              

                if (Time.time >= startTime + playerData.dashTime)
                {
                    player.InputHandler.dashInput = false;
                    player.RB.drag = 0f;
                    isAbilityDone = true;
                    lastDashTime = Time.time;
                }
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    public bool CheckIfCanDash()
    {
        return CanDash && Time.time >= lastDashTime + playerData.dashCooldown;
    }
    public void ResetCanDash() => CanDash = true;

}
