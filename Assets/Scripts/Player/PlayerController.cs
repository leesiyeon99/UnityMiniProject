using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum State { Idle, Run, Jump, Fall, Size }
    [SerializeField] State curState = State.Idle;
    private BaseState[] states = new BaseState[(int)State.Size];

    [SerializeField] Rigidbody2D rigid;
    [SerializeField] SpriteRenderer render;
    [SerializeField] Animator animator;
    [SerializeField] float movePower;
    [SerializeField] float maxMoveSpeed;
    [SerializeField] float jumpPower;
    [SerializeField] float maxFallSpeed;

    [SerializeField] bool isGrounded;
    int curAniHash;
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

    private void OnDestroy()
    {
        states[(int)curState].Exit();
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
        public IdleState(PlayerController player) : base(player)
        {
        }
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
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                player.ChangeState(State.Jump);
            }
        }

    }

    private class RunState : PlayerState
    {
        public RunState(PlayerController player) : base(player)
        {
        }

        public override void Enter()
        {
            player.animator.Play(player.runHesh);
        }

        public override void Update()
        {
            player.x = Input.GetAxisRaw("Horizontal");

            if (player.x == 0)
            {
                player.ChangeState(State.Idle);
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                player.ChangeState(State.Jump);
            }
        }

        public override void FixedUpdate()
        {
            player.rigid.AddForce(player.x * player.movePower * Vector2.right, ForceMode2D.Force);
            player.Move();
        }
    }

    private class JumpState : PlayerState
    {
        public JumpState(PlayerController player) : base(player)
        {
        }

        public override void Enter()
        {
            player.animator.Play(player.jumpHesh);
            player.isGrounded = false;
            player.rigid.velocity = new Vector2(player.rigid.velocity.x, 0);
            player.rigid.AddForce(Vector2.up * player.jumpPower, ForceMode2D.Impulse);
            AudioManager.Instance.PlaySFX(0);
        }

        public override void Update()
        {

            player.x = Input.GetAxisRaw("Horizontal");
            if (Mathf.Abs(player.x) > 0.01f)
            {
                player.rigid.AddForce(player.x * Vector2.right, ForceMode2D.Force);
                player.Move();
            }

            if (player.rigid.velocity.y < 0 && !player.isGrounded)
            {
                player.ChangeState(State.Fall);
            }
            else if (player.rigid.velocity.sqrMagnitude < 0.01f)
            {
                player.ChangeState(State.Idle);
            }
        }
    }


    private class FallState : PlayerState
    {
        public FallState(PlayerController player) : base(player)
        {
        }

        public override void Enter()
        {
            player.animator.Play(player.fallHesh);
        }

        public override void Update()
        {
            player.x = Input.GetAxisRaw("Horizontal");
            if (Mathf.Abs(player.x) > 0.01f)
            {
                player.rigid.AddForce(player.x * Vector2.right, ForceMode2D.Force);
                player.Move();
            }

            if (player.isGrounded)
            {
                player.ChangeState(State.Idle);
            }
        }
    }

    private void Move()
    {
        if (rigid.velocity.x > maxMoveSpeed)
        {
            rigid.velocity = new Vector2(maxMoveSpeed, rigid.velocity.y);
        }
        else if (rigid.velocity.x < -maxMoveSpeed)
        {
            rigid.velocity = new Vector2(-maxMoveSpeed, rigid.velocity.y);
        }

        if (rigid.velocity.y < -maxFallSpeed)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, -maxFallSpeed);
        }

        if (x < 0) render.flipX = true;
        else if (x > 0) render.flipX = false;

    }

    private void GroundCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0, -0.5f, 0), Vector2.down, 0.5f);
        if (hit.collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
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
