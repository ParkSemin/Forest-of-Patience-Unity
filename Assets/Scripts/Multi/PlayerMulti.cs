using Photon.Pun;
using Photon.Realtime;
using TMPro;
using System.Collections;
using UnityEngine;

public class PlayerMulti : MonoBehaviourPun
{
    public float moveSpeed = 1.3f; // 이동 속도
    public float jumpForce = 2.5f; // 점프 힘

    // 마지막으로 보고 있는 방향을 기억하는 변수
    private bool isFacingRight = true;
    private bool isGround = true;
    private string myName;

    private Rigidbody2D rb;
    public AudioSource mySfx;
    public AudioClip jumpSound, gruntSound;
    public Animator playerAnimator;
    public TextMeshProUGUI mNicknameLabel;
    public SpriteRenderer spriteRenderer;

    void Start()
    {
        foreach(PlayerMulti player in GameObject.FindObjectsOfType<PlayerMulti>()) {
            player.ApplyPlayerName(player.photonView.Owner.NickName);
        }
        
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }

    void Update()
    {
        if (!photonView.IsMine)
        {
            mNicknameLabel.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f);
            return;
        }
        
        if (!GameManagerMulti.instance.isGameover && !GameManagerMulti.instance.isPause) {
            rb.gravityScale = 1;
            Move(); 
            CheckGround();
            if (Input.GetKeyDown(KeyCode.Space)) {
                Jump();
            }
            mNicknameLabel.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f);
        }
    }

    private void ApplyPlayerName(string name) {
        mNicknameLabel.text = name;

        if (photonView.IsMine) {
            myName = name;
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
            transform.rotation = Quaternion.Euler(0, 180, 0); // 180도 회전하여 왼쪽을 보게 함
            isFacingRight = false;
            playerAnimator.SetBool("isMove", true);
        }
        else if (moveInput > 0) // 오른쪽으로 이동
        {
            transform.rotation = Quaternion.Euler(0, 0, 0); // 원래 방향 (오른쪽)
            isFacingRight = true;
            playerAnimator.SetBool("isMove", true);
        }
        else
        {
            // 이동이 멈추면 마지막 방향을 유지
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
        if (other.CompareTag("Finish") && myName == mNicknameLabel.text) {
            photonView.RPC("EndGame", RpcTarget.All, myName);
        }
    }

    [PunRPC]
    void EndGame(string name) {
        GameManagerMulti.instance.OnPlayerFinish(name);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Obstacle")) {
            
            if (photonView.IsMine) {
                // 충돌 사실을 마스터 클라이언트에게 알림
                photonView.RPC("NotifyMasterClient", RpcTarget.MasterClient, photonView.ViewID);
                StartCoroutine(ChangeColorCoroutine());
                mySfx.PlayOneShot(gruntSound);
                int dirX = transform.position.x - collision.transform.position.x > 0 ? 1 : -1;
                rb.AddForce(new Vector2(dirX, 1) * (jumpForce/1.5f), ForceMode2D.Impulse);
            }
        }
    }

    [PunRPC]
    void NotifyMasterClient(int viewID)
    {
        // 모든 클라이언트에게 이 캐릭터가 장애물에 맞았음을 알림
        photonView.RPC("OnHitByObstacle", RpcTarget.All, viewID);
    }

    [PunRPC]
    void OnHitByObstacle(int viewID)
    {
        if (photonView.ViewID == viewID)
        {
            // 캐릭터 색상을 빨간색으로 변경하고 일정 시간 후 다시 흰색으로 변경
            StartCoroutine(ChangeColorCoroutine());
        }
    }

    IEnumerator ChangeColorCoroutine()
    {
        spriteRenderer.color = new Color32(255, 100, 100, 255);
        yield return new WaitForSeconds(0.3f);
        spriteRenderer.color = Color.white;
    }
}