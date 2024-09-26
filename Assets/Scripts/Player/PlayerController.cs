using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] SpriteRenderer render;
    [SerializeField] Animator animator;
    [SerializeField] float movePower;
    [SerializeField] float maxMoveSpeed;
    [SerializeField] float jumpPower;
    [SerializeField] float maxFallSpeed;



    [SerializeField] public int hp = 10;
    [SerializeField] public int maxHp = 10;

    [SerializeField] bool isGrounded;
    int curAniHash;
    float x;
    private int idleHesh = Animator.StringToHash("Idle");
    private int runHesh = Animator.StringToHash("Run");
    private int jumpHesh = Animator.StringToHash("Jump");
    private int fallHesh = Animator.StringToHash("Fall");

    public bool canMove;

    private void Start()
    {
        curAniHash = idleHesh;
    }

    private void Update()
    {
        if (!canMove) return;

        x = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        GroundCheck();
        AnimatorPlay();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        rigid.AddForce(x * movePower * Vector2.right, ForceMode2D.Force);
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

    public  void Jump()
    {
        if (!isGrounded) return;
        isGrounded = false;
        rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
    }

    private void GroundCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0, -0.5f, 0), Vector2.down, 0.5f);
        isGrounded = hit.collider != null;
    }

    private void AnimatorPlay()
    {
        int checkAniHash;
        if (rigid.velocity.y > 0.01f && !isGrounded)
        {
            checkAniHash = jumpHesh;
        }
        else if (rigid.velocity.y < -0.01f && !isGrounded)
        {
            checkAniHash = fallHesh;
        }
        else if (rigid.velocity.sqrMagnitude < 0.01f)
        {
            checkAniHash = idleHesh;
        }
        else
        {
            checkAniHash = runHesh;
        }

        if (curAniHash != checkAniHash)
        {
            curAniHash = checkAniHash;
            animator.Play(curAniHash);
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

