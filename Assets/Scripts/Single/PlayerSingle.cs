using System.Collections;
using Photon.Pun;
using UnityEngine;

public class PlayerSingle : MonoBehaviourPun
{
    public float moveSpeed = 1.3f; // 이동 속도
    public float jumpForce = 2.5f; // 점프 힘

    // 마지막으로 보고 있는 방향을 기억하는 변수
    private bool isFacingRight = true;
    private bool isGround = true;

    private Rigidbody2D rb;
    public AudioSource mySfx;
    public AudioClip jumpSound, gruntSound;
    public Animator playerAnimator;
    public SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {        
        if (!GameManagerSingle.instance.isGameover && !GameManagerSingle.instance.isPause) {
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
        // 점프를 하고 착지할 때 y 값이 이상하게 되는 문제, 제자리에서 좌우로 움직이기만 해도 y 값이 바뀌는 문제를 해결하기 위한 범위 설정
        if (rb.velocity.y < 0.00000001 && rb.velocity.y >= 0) {
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
            GameManagerSingle.instance.OnPlayerFinish();
            print("GameOver!!");
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Obstacle")) {
            Debug.Log("장애물 충돌");
            StartCoroutine(Blink());
            mySfx.PlayOneShot(gruntSound);
            int dirX = transform.position.x - collision.transform.position.x > 0 ? 1 : -1;
            rb.AddForce(new Vector2(dirX, 1) * (jumpForce/1.5f), ForceMode2D.Impulse);
        }
    }

    IEnumerator Blink() {
        spriteRenderer.color = new Color32(255, 100, 100, 255);
        yield return new WaitForSeconds(0.3f);
        spriteRenderer.color = Color.white;
    }
}