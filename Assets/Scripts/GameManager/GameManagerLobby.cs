using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerLobby : MonoBehaviourPunCallbacks
{
    public static GameManagerLobby instance;

    public bool isPause = false;
    private string selectedMapName;
    public GameObject pauseUI, playerPrefab;
    public AudioSource music; // 배경음악

    // 게임 시작과 동시에 싱글톤을 구성
    void Awake() {
        // 싱글톤 변수 instance가 비어있는가?
        if (instance == null)
        {
            // instance가 비어있다면(null) 그곳에 자기 자신을 할당
            instance = this;
            music.Play();
        }
        else
        {
            // instance에 이미 다른 GameManager 오브젝트가 할당되어 있는 경우
            // 씬에 두개 이상의 GameManager 오브젝트가 존재한다는 의미.
            // 싱글톤 오브젝트는 하나만 존재해야 하므로 자신의 게임 오브젝트를 파괴
            Debug.LogWarning("씬에 두개 이상의 게임 매니저가 존재합니다!");
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // 생성할 위치 지정
        Vector3 spawnPos = new Vector3(0, -1);
        // 네트워크상의 모든 클라이언트에서 생성 메서드 실행
        // 해당 게임 오브젝트의 주도권은 생성 메서드를 직접 실행한 클라이언트에 있음
        PhotonNetwork.Instantiate(playerPrefab.name, spawnPos, Quaternion.identity);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            OnPause();
        }
    }

    // ESC 키를 눌러 계속하기, 메인화면 팝업창 보이는 메서드
    public void OnPause() {
        isPause = !isPause;
        pauseUI.SetActive(isPause);
    }

    // 계속하기 메서드
    public void OnClickContinue() {
        OnPause();
    }

    // 메인 화면 이동 메서드
    public void OnClickMain() {
        pauseUI.SetActive(false);
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.Disconnect();
    }

    // 룸을 나가면 로비 씬으로 돌아감
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("Title");
    }
}
