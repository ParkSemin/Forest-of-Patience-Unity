using System.Collections;
using System.Collections.Generic;
// using Photon.Pun;
using UnityEngine;

public class SawObstacleSingle : MonoBehaviour
{
    float lowerY = 0f; // 장애물이 내려갈 y값
    float upperY = 0.24f; // 장애물이 올라갈 y값
    float duration = 0.25f; // 움직이는 데 걸리는 시간
    float elapsedTime;

    private bool movingUp = true;
    public new CircleCollider2D collider2D;

    void Start() {
        StartCoroutine(MoveSaw());
    }

    IEnumerator MoveSaw()
    {
        while (true)
        {
            float startY = transform.localPosition.y;
            float endY = movingUp ? upperY : lowerY;
            collider2D.enabled = false;
            elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                transform.localPosition = new Vector3(
                    transform.localPosition.x,
                    Mathf.Lerp(startY, endY, elapsedTime / duration),
                    transform.localPosition.z
                );
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            collider2D.enabled = endY == upperY;
            transform.localPosition = new Vector3(transform.localPosition.x, endY, transform.localPosition.z);
            yield return new WaitForSeconds(2.0f);
            movingUp = !movingUp;
        }
    }
}
