using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class SawObstacleMulti : MonoBehaviourPun
{
    float lowerY = 0f; // 장애물이 내려갈 y값
    float upperY = 0.24f; // 장애물이 올라갈 y값
    float duration = 0.25f; // 움직이는 데 걸리는 시간

    private bool movingUp = true;
    public new CircleCollider2D collider2D;

    void Start() {
        if (PhotonNetwork.IsMasterClient) {
            Invoke(nameof(SetUp), 3f);
        }
    }

    void SetUp() {
        photonView.RPC("Init", RpcTarget.All);
    }

    [PunRPC]
    public void Init()
    {
        // 코루틴 시작
        StartCoroutine(MoveSaw());
    }

    IEnumerator MoveSaw()
    {
        while (true)
        {
            float startY = transform.localPosition.y;
            float endY = movingUp ? upperY : lowerY;
            float startTime = (float)PhotonNetwork.Time;

            collider2D.enabled = false;
            while ((float)PhotonNetwork.Time - startTime < duration)
            {
                float elapsedTime = (float)PhotonNetwork.Time - startTime;
                transform.localPosition = new Vector3(
                    transform.localPosition.x,
                    Mathf.Lerp(startY, endY, elapsedTime / duration),
                    transform.localPosition.z
                );
                yield return null;
            }
            collider2D.enabled = endY == upperY;
            transform.localPosition = new Vector3(transform.localPosition.x, endY, transform.localPosition.z);
            yield return new WaitForSeconds(2.0f);
            movingUp = !movingUp;
        }
    }
}
