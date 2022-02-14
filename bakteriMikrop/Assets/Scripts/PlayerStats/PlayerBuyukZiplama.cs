using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class PlayerBuyukZiplama : PlayerState
{
    public bool CanDash { get; private set; }
    private bool isHolding;
    private bool dashInputStop;
    private float holdtime;
    private float lastDashTime;

    private Vector2 dashDirection;
    private Vector2 dashDirectionInput;
    private Vector2 lastAIPos;

    private bool isAbilityDone = false;
    public int facing = 0;
    Sequence sq2;
    bool biti = false;
    public PlayerBuyukZiplama(Player player, PlayerStateMachine stateMachine, BacteriaStats playerData) : base(player, stateMachine, playerData)
    {

        sq2.Pause();
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
        biti = true;
        player.transform.SetParent(transformvar);
        player.canvas.SetActive(true);
        player.infoslider.maxValue = playerData.maxHoldTime;
        player.infoslider.fillRect.GetComponent<Image>().color = Color.red;
        player.buyukziplamaparticle.SetActive(true);
        if (facing == 0)
        {
            player.buyukziplamaparticle.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
         
            sq.Append(player.visual.transform.DOScale(new Vector3(1.5f, 0.5f, 1f), playerData.maxHoldTime));
            sq.Join(player.visual.transform.DOLocalMoveY(-0.5f, playerData.maxHoldTime));

            // sq.OnPause(() => sq2.Play());
            //sq.OnKill(() => sq2.Play());
        }
        else if (facing == 1)
        {
           
            player.buyukziplamaparticle.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            sq.Append(player.visual.transform.DOScale(new Vector3(0.5f, 1.5f, 1f), playerData.maxHoldTime));
            sq.Join(player.visual.transform.DOLocalMoveX(0.5f, playerData.maxHoldTime));
        }
        else if (facing == -1)
        {
          
            player.buyukziplamaparticle.transform.rotation = Quaternion.Euler(0f, 0f, -90f);
            sq.Append(player.visual.transform.DOScale(new Vector3(0.5f, 1.5f, 1f), playerData.maxHoldTime));
            sq.Join(player.visual.transform.DOLocalMoveX(0.5f, playerData.maxHoldTime));
        }
        holdtime = 0f;
        finalvelo = 0f;
        player.SetVelocityZero();
        player.DashDirectionIndicator.gameObject.SetActive(true);
        isAbilityDone = false;
        CanDash = false;
        isHolding = true;
        dashDirection = Vector2.right * player.FacingDirection;

    }
    float finalvelo = 0;
    public override void Exit()
    {
        base.Exit();
        player.TarilEffect.SetActive(false);
        // player.visual.transform.localPosition = Vector3.zero;
        facing = 0;
        player.canvas.SetActive(false);
        player.buyukziplamaparticle.SetActive(false);
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
                holdtime += Time.deltaTime;
                player.SetVelocityZero();
                dashDirectionInput = player.InputHandler.DashDirectionInput;
                dashInputStop = player.InputHandler.DashInputStop;
                player.infoslider.value = holdtime;
                player.infoslider.fillRect.GetComponent<Image>().color = Color.Lerp(player.infoslider.fillRect.GetComponent<Image>().color,Color.green,Time.deltaTime);
                if (dashDirectionInput != Vector2.zero)
                {
                    dashDirection = dashDirectionInput;
                    dashDirection.Normalize();
                }

                float angle = Vector2.SignedAngle(Vector2.right, dashDirection);
                player.DashDirectionIndicator.rotation = Quaternion.Euler(0f, 0f, angle - 45f);

                if ((dashInputStop || Time.time >= startTime + playerData.maxHoldTime) && biti)
                {
                    biti = false;
                    sq.Kill();
                    sq2 = DOTween.Sequence();

                    sq2.Append(player.visual.transform.DOScale(new Vector3(1f, 1f, 1f), 0.1f));
                    sq2.Join(player.visual.transform.DOLocalMoveY(0, 0.1f));
                    sq2.Join(player.visual.transform.DOLocalMoveX(0, 0.1f));
                    sq2.OnComplete(() => Zipla());



                }
            }
            else
            {
                if (biti == false)
                {
                    player.SetVelocity(finalvelo, dashDirection);


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
    }

    private void Zipla()
    {
        
        if (facing == 0)
        {
          
            GameObject.Instantiate(player.jumpeffect, player.transform.position, Quaternion.Euler(0f, 0f, 0f));
           
            // sq.OnPause(() => sq2.Play());
            //sq.OnKill(() => sq2.Play());
        }
        else if (facing == 1)
        {
            GameObject.Instantiate(player.jumpeffect, player.transform.position, Quaternion.Euler(0f, 0f, 90f));
           
        }
        else if (facing == -1)
        {
            GameObject.Instantiate(player.jumpeffect, player.transform.position, Quaternion.Euler(0f, 0f, -90f));
          
        }
        player.TarilEffect.SetActive(true);
        player.buyukziplamaparticle.SetActive(false);
        player.canvas.SetActive(false);
        player.transform.SetParent(null);
        isHolding = false;
        startTime = Time.time;
        player.CheckIfShouldFlip(Mathf.RoundToInt(dashDirection.x));
        player.RB.drag = 0;
        finalvelo = ((playerData.buyukziplamavelo+playerData.buyukziplamaUpragevelo) * ((holdtime) / (float)playerData.maxHoldTime));
        player.SetVelocity(playerData.buyukziplamavelo * (finalvelo), dashDirection.normalized);
        player.DashDirectionIndicator.gameObject.SetActive(false);
        SoundManager.instance.PlayZiplamaSesi();
    }
    Transform transformvar;
    public void parenayarla(Transform parentt)
    {
        transformvar = parentt;
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
