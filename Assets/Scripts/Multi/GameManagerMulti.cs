using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerMulti : MonoBehaviourPunCallbacks
{
    public static GameManagerMulti instance;

    public bool isGameover = false;
    public bool isPause = false;
    public TextMeshProUGUI timeText, bestTimeText;
    public GameObject gameoverUI, pauseUI, playerPrefab;
    public AudioSource music; // 배경음악
    private float sec;
    private int min;

    [SerializeField]
    private TextMeshProUGUI TimerText;

    private int bestMin;
    private int bestSec;


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
        
        bestMin = PlayerPrefs.GetInt("BestMin");
        bestSec = PlayerPrefs.GetInt("BestSec");

        if (bestMin == 0 && bestSec == 0) {
            bestMin = 99;
        }
    }

    void Update()
    {
        if (isGameover)
        {
            // music.Pause();
            if (Input.GetKeyDown(KeyCode.R))
            {
                // r을 누르면 기록 초기화
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                PlayerPrefs.SetInt("BestMin", 0);
                PlayerPrefs.SetInt("BestSec", 0);
            } 
        } else {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                OnPause();
            }
            Timer();
        }
    }

    // 골인지점 도착 시 게임 오버를 실행하는 메서드
    public void OnPlayerFinish() {
        isGameover = true;
        gameoverUI.SetActive(true);
        
        // 최고점수 갱신 여부 확인
        if (bestMin*60 + bestSec > min*60 + (int)sec) {
            bestMin = min;
            bestSec = (int)sec;
            PlayerPrefs.SetInt("BestMin", bestMin);
            PlayerPrefs.SetInt("BestSec", bestSec);
        }

        bestTimeText.text = string.Format("{0:D2}:{1:D2}", bestMin, bestSec);
        timeText.text = string.Format("{0:D2}:{1:D2}", min, (int)sec);
    }

    // 타이머 메서드
    void Timer()
    {
        sec += Time.deltaTime;
        if ((int)sec > 59) {
            sec = 0;
            min++;
        }

        TimerText.text = string.Format("{0:D2}:{1:D2}", min, (int)sec);
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

    // 다시 시작 메서드
    public void OnClickRestart() {
        pauseUI.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
