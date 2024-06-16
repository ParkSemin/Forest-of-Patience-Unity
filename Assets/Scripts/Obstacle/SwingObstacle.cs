using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class SwingObstacle : MonoBehaviourPun
{
    // public static SwingObstacle instance;
    public float amplitude = 30f; // 최대 각도
    public float frequency = 2f; // 주기
    private double startTime;

    public void Start() {
        if (PhotonNetwork.IsMasterClient) {
            Invoke(nameof(SetUp), 3f);
        }
    }

    void SetUp() {
        startTime = UnityEngine.Random.Range(-amplitude, amplitude);
        photonView.RPC("Init", RpcTarget.All, startTime);
    }

    [PunRPC]
    public void Init(double startTime)
    {
        StartCoroutine(SwingChain(startTime));
    }

    IEnumerator SwingChain(double startTime) {
        while (true) {
            double elapsedTime = PhotonNetwork.Time - startTime;
            float angle = amplitude * Mathf.Sin((float)elapsedTime * frequency);
            transform.rotation = Quaternion.Euler(0, 0, angle);
            yield return null;
        }
    }
}