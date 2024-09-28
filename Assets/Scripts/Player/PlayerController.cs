using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum State { Idle, Run, Jump, Fall, Size }
    [SerializeField] State curState = State.Idle;
    private BaseState[] states = new BaseState[(int)State.Size];

    [SerializeField] Rigidbody2D rigid;
    [SerializeField] SpriteRenderer render;
    [SerializeField] Animator animator;
    [SerializeField] float moveSpeed;
    [SerializeField] float maxMoveSpeed;
    [SerializeField] float jumpPower;
    [SerializeField] float maxFallSpeed;

    [SerializeField] bool isGrounded;
    float x;
    public bool canMove;

    private int idleHesh = Animator.StringToHash("Idle");
    private int runHesh = Animator.StringToHash("Run");
    private int jumpHesh = Animator.StringToHash("Jump");
    private int fallHesh = Animator.StringToHash("Fall");

    private void Awake()
    {
        states[(int)State.Idle] = new IdleState(this);
        states[(int)State.Run] = new RunState(this);
        states[(int)State.Jump] = new JumpState(this);
        states[(int)State.Fall] = new FallState(this);
    }

    private void Start()
    {
        states[(int)curState].Enter();
        animator.Play(idleHesh);
    }

    private void Update()
    {
        if (!canMove) return;
        states[(int)curState].Update();
    }

    private void FixedUpdate()
    {
        GroundCheck();
        states[(int)curState].FixedUpdate();
    }

    public void ChangeState(State state)
    {
        states[(int)curState].Exit();
        curState = state;
        states[(int)curState].Enter();
    }

    private class PlayerState : BaseState
    {
        public PlayerController player;
        public PlayerState(PlayerController player)
        {
            this.player = player;
        }
    }

    private class IdleState : PlayerState
    {
        public IdleState(PlayerController player) : base(player) { }
        public override void Enter()
        {
            player.animator.Play(player.idleHesh);
        }
        public override void Update()
        {
            player.x = Input.GetAxisRaw("Horizontal");

            if (Mathf.Abs(player.x) > 0.01f)
            {
                player.ChangeState(State.Run);
            }
            else if (Input.GetKeyDown(KeyCode.Space) && player.isGrounded)
            {
                player.ChangeState(State.Jump);
            }
        }
    }

    private class RunState : PlayerState
    {
        public RunState(PlayerController player) : base(player) { }
        public override void Enter() 
        {
            player.animator.Play(player.runHesh); 
        }
        public override void Update()
        {
            player.x = Input.GetAxisRaw("Horizontal");

            if (Mathf.Abs(player.x) < 0.01f)
            {
                player.ChangeState(State.Idle);
            }
            else if (Input.GetKeyDown(KeyCode.Space) && player.isGrounded)
            {
                player.ChangeState(State.Jump);
            }
        }

        public override void FixedUpdate()
        {
            float targetSpeed = player.x * player.moveSpeed;
            targetSpeed = Mathf.Clamp(targetSpeed, -player.maxMoveSpeed, player.maxMoveSpeed);

            player.rigid.velocity = new Vector2(targetSpeed, player.rigid.velocity.y);

            player.render.flipX = player.x < 0;
        }
    }

    private class JumpState : PlayerState
    {
        public JumpState(PlayerController player) : base(player) { }

        public override void Enter()
        {
            player.animator.Play(player.jumpHesh);
            player.isGrounded = false;
            player.rigid.velocity = new Vector2(player.rigid.velocity.x, player.jumpPower);
            AudioManager.Instance.PlaySFX(0);
        }

        public override void Update()
        {
            player.x = Input.GetAxisRaw("Horizontal");

            float targetSpeed = player.x * player.moveSpeed;
            targetSpeed = Mathf.Clamp(targetSpeed, -player.maxMoveSpeed, player.maxMoveSpeed);

            player.rigid.AddForce(new Vector2(targetSpeed - player.rigid.velocity.x, 0), ForceMode2D.Force);

            player.render.flipX = player.x < 0;

            if (player.rigid.velocity.y < 0)
            {
                player.ChangeState(State.Fall);
            }
        }
    }


    private class FallState : PlayerState
    {
        public FallState(PlayerController player) : base(player) { }
        public override void Enter() 
        { 
            player.animator.Play(player.fallHesh); 
        }
        public override void Update()
        {
            if (player.isGrounded)
            {
                player.ChangeState(State.Idle);
            }
        }
    }

    private void GroundCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0, -0.5f, 0), Vector2.down, 0.1f);
        isGrounded = hit.collider != null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("RedWater"))
        {
            Debug.Log("빨간물에 닿았습니다.");
        }
        if (collision.gameObject.CompareTag("BlueWater"))
        {
            Debug.Log("파란물에 닿았습니다.");
        }
        if (collision.gameObject.CompareTag("GreenWater"))
        {
            Debug.Log("초록물에 닿았습니다.");
        }
    }
}
