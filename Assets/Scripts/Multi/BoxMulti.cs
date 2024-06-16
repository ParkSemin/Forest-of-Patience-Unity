using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class BoxMulti : MonoBehaviourPun
{
    private Renderer boxRenderer;
    private Collider2D boxCollider;
    private Color originalColor;
    float fadeDuration = 0.25f; // 움직이는 데 걸리는 시간
    float visibleDuration = 2.0f; // 상자가 보이는 시간

    float initValue;

    public void Start() {
        if (PhotonNetwork.IsMasterClient) {
            Invoke(nameof(SetUp), 3f);
        }
    }
    
    void SetUp()
    {
        photonView.RPC("Init", RpcTarget.All, Random.Range(0f, 3f));
    }

    [PunRPC]
    public void Init(float waitSecond) {
        boxRenderer = GetComponent<Renderer>();
        boxCollider = GetComponent<Collider2D>();
        originalColor = boxRenderer.material.color;
        StartCoroutine(BoxVisibilityCycle(waitSecond));
    }

    IEnumerator BoxVisibilityCycle(float waitSecond)
    {
        yield return new WaitForSeconds(waitSecond);
        while (true)
        {
            // 상자가 보이는 상태 유지
            yield return new WaitForSeconds(visibleDuration);

            // 투명하게 만들기
            yield return StartCoroutine(FadeOut());

            // 투명 상태 유지
            yield return new WaitForSeconds(visibleDuration);

            // 다시 보이게 만들기
            yield return StartCoroutine(FadeIn());
        }
    }

    IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        float startTime = (float)PhotonNetwork.Time;
        Color color = boxRenderer.material.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime = (float)PhotonNetwork.Time - startTime;
            color.a = Mathf.Lerp(originalColor.a, 0f, elapsedTime / fadeDuration);
            boxRenderer.material.color = color;
            yield return null;
        }

        color.a = 0f;
        boxRenderer.material.color = color;
        boxCollider.enabled = false; // 콜라이더 비활성화
    }

    IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        float startTime = (float)PhotonNetwork.Time;
        Color color = boxRenderer.material.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime = (float)PhotonNetwork.Time - startTime;
            color.a = Mathf.Lerp(0f, originalColor.a, elapsedTime / fadeDuration);
            boxRenderer.material.color = color;
            yield return null;
        }

        color.a = originalColor.a;
        boxRenderer.material.color = color;
        boxCollider.enabled = true; // 콜라이더 활성화
    }
}
