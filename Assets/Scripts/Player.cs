using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 2f; // 이동 속도
    public float jumpForce = 3f; // 점프 힘

    // 마지막으로 보고 있는 방향을 기억하는 변수
    private bool isFacingRight = true;
    private bool isGround = true;

    private Rigidbody2D rb;
    public AudioSource mySfx;
    public AudioClip jumpSound;
    public Animator playerAnimator;

    public LayerMask playerLayer, groundLayer;

    public Transform groundCheck; // 캐릭터 발 아래에 위치한 점

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (GameManager.instance.isGameover == false) {
            Move();
            CheckGround();
            if (Input.GetKeyDown(KeyCode.Space)) {
                Jump();
            }
        }
    }
        
    void Move()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        Vector3 moveTo = new Vector3(moveInput * moveSpeed, rb.velocity.y);
        transform.position += moveTo * moveSpeed * Time.deltaTime;

        // 캐릭터가 이동 중일 때만 방향을 변경
        if (moveInput < 0) // 왼쪽으로 이동
        {
            GetComponent<SpriteRenderer>().flipX = true;
            isFacingRight = false;
            playerAnimator.SetBool("isMove", true);
        }
        else if (moveInput > 0) // 오른쪽으로 이동
        {
            GetComponent<SpriteRenderer>().flipX = false;
            isFacingRight = true;
            playerAnimator.SetBool("isMove", true);
        }
        else
        {
            // 이동이 멈추면 마지막 방향을 유지
            GetComponent<SpriteRenderer>().flipX = !isFacingRight;
            playerAnimator.SetBool("isMove", false);
        }
    }

    void CheckGround() {
        if (rb.velocity.y == 0) {
            isGround = true;
            playerAnimator.SetBool("isJump", false);
        } else {
            isGround = false;
        }
    }

    void Jump()
    {
        if (isGround)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
            JumpSound();
            playerAnimator.SetBool("isJump", true);
        }
    }

    void JumpSound()
    {
        mySfx.PlayOneShot(jumpSound);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Finish")) {
            GameManager.instance.OnPlayerDead();
            print("GameOver!!");
        }
    }
}