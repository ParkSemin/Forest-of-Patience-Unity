using System.Collections;
using UnityEngine;

public class SwingObstacleSingle : MonoBehaviour
{
    private float amplitude = 90f; // 최대 각도
    private float frequency = 2f; // 주기
    float elapsedTime;

    public void Start() {
        elapsedTime = Random.Range(0f, 3f);
        StartCoroutine(SwingChain());
    }

    IEnumerator SwingChain() {
        while (true) {
            elapsedTime += Time.deltaTime;
            float angle = amplitude * Mathf.Sin(elapsedTime * frequency);
            transform.rotation = Quaternion.Euler(0, 0, angle);
            yield return null;
        }
    }
}