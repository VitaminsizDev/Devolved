using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //states
    public PlayerStateMachine StateMachine { get; private set; }

    public PlayerIdle IdleState { get; private set; }
    public PlayerMove MoveState { get; private set; }
    public PlayerSuzulme suzulmeState { get; private set; }
    public PlayerFall fallState { get; private set; }
    public PlayerDuvarTutanma duvarstate { get; private set; }
    public PlayerBuyukZiplama buyukziplama { get; private set; }
    public BekleState beklestate { get; private set; }


    //components
    public Vector2 size=Vector2.one;
    public ParticleSystem move; 
    public ParticleSystem land; 
    public ParticleSystem suzulme;
    public GameObject buyukziplamaparticle;
    public GameObject jumpeffect;
    public GameObject canvas;
    public GameObject TarilEffect;
    public Slider infoslider;
    public GameObject visual;
    [SerializeField]
    private Transform WallCheck;
    [SerializeField]
    private Transform GroundCheck;
    [SerializeField]
    private Transform CeilingCheck;
    [SerializeField]
    private BacteriaStats playerData;
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public Transform DashDirectionIndicator;
    private BoxCollider2D colider;
    //
    [Header("Evrimler")]
    public bool duvartutun;
    public bool buyukzipla;



    //variable
    public bool CanSetVelocity { get; set; }
    public int FacingDirection;

    public Vector2 CurrentVelocity { get; private set; }

    private Vector2 workspace;
    // Start is called before the first frame update

    //gozlem
    public string currentstate;
    protected void Awake()
    {
        colider = GetComponent<BoxCollider2D>();
        StateMachine = new PlayerStateMachine();
        suzulme.Stop();
        IdleState = new PlayerIdle(this, StateMachine, playerData);
        MoveState = new PlayerMove(this, StateMachine, playerData);
        suzulmeState = new PlayerSuzulme(this, StateMachine, playerData);
        fallState = new PlayerFall(this, StateMachine, playerData);
        duvarstate = new PlayerDuvarTutanma(this, StateMachine, playerData);
        buyukziplama = new PlayerBuyukZiplama(this, StateMachine, playerData);
        beklestate = new BekleState(this, StateMachine, playerData);
        


    }
    private void Start()
    {
        RB = GetComponentInParent<Rigidbody2D>();

        FacingDirection = 1;
        CanSetVelocity = true;
        InputHandler = GetComponent<PlayerInputHandler>();
        StateMachine.Initialize(IdleState);
    }

    // Update is called once per frame
    void Update()
    {
        currentstate = StateMachine.CurrentState.ToString();
        CurrentVelocity = RB.velocity;
        StateMachine.CurrentState.LogicUpdate();
    }


    private void FixedUpdate()
    {

        StateMachine.CurrentState.PhysicsUpdate();
    }
    #region digerfonk
    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();

    private void AnimtionFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
    #endregion

    #region colision
    public bool Ceiling
    {
        get => Physics2D.OverlapCircle(CeilingCheck.position, playerData.groundCheckRadius, playerData.whatIsGround);
    }

    public RaycastHit2D info;
    public bool Ground
    {
        get => Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y - (size.y / 2)), playerData.groundCheckRadius, playerData.whatIsGround);
    }

    public bool WallFront
    {
        get => Physics2D.Raycast(transform.position, Vector2.right * FacingDirection, (size.y / 2)+0.2f, playerData.whatIsGround);
    }

    public bool WallBack
    {
        get => Physics2D.Raycast(WallCheck.position, Vector2.right * -FacingDirection, (size.y / 2) + 0.2f, playerData.whatIsGround);
    }
    //public Transform groundtransform
    //{
    //get=>    Physics2D.Raycast(WallCheck.position, Vector2.down, playerData.wallCheckDistance, playerData.whatIsGround);
    //}
    #endregion
    public Transform setparentts()
    {
        info = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y- (colider.size.y/2)+0.2f), Vector2.down, 0.4f, playerData.whatIsGround);
        if (info)
        {
            return info.collider.transform;
        }
        return null;
    }  
    public Transform setparentts2()
    {
        info = Physics2D.Raycast(new Vector2(transform.position.x+FacingDirection*(colider.size.x / 2) +( -FacingDirection* 0.2f), transform.position.y),FacingDirection* Vector2.right, 0.4f, playerData.whatIsGround);
        if (info)
        {
            return info.collider.transform;
        }
        return null;
    }
    #region move
    public void SetVelocity(float velocity, Vector2 direction)
    {
        workspace = direction * velocity;
        SetFinalVelocity();
    }
    public void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        workspace.Set(angle.x * velocity * direction, angle.y * velocity);
        SetFinalVelocity();
    }
    public void SetVelocityX(float velocity)
    {
        workspace.Set(velocity, CurrentVelocity.y);
        SetFinalVelocity();
    }
    public void SetVelocityY(float velocity)
    {
        workspace.Set(CurrentVelocity.x, velocity);
        SetFinalVelocity();
    }
    public void SetVelocityZero()
    {
        workspace = Vector2.zero;
        SetFinalVelocity();
    }
    private void SetFinalVelocity()
    {
        if (CanSetVelocity)
        {
            RB.velocity = workspace;
            CurrentVelocity = workspace;
        }
    }
    public void CheckIfShouldFlip(int xInput)
    {
        if (xInput != 0 && xInput != FacingDirection)
        {
            Flip();
        }
    }

    public void Flip()
    {
        FacingDirection *= -1;
        RB.transform.Rotate(0.0f, 180.0f, 0.0f);
    }
    #endregion

    private void OnDrawGizmos()
    {
        if (colider!=null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(new Vector2(transform.position.x, transform.position.y - (size.y / 2)), playerData.groundCheckRadius);
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(WallCheck.position, new Vector2(transform.position.x + (size.x / 2) + 0.1f, transform.position.y));
        }
       
       
    }
}
