using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    private string selectedMapName;
    public GameObject menuUI, mapSelectUI, infoUI;

    void Update()
    {
        if (PhotonNetwork.IsMasterClient) {
            menuUI.SetActive(true);
        }
       
    }

    // 게임 시작 메서드
    public void OnClickGameStart() {
        if (selectedMapName != null) {
            photonView.RPC("GameStart", RpcTarget.All, selectedMapName);   
        } else {
            infoUI.SetActive(true);
        }
    }

    [PunRPC]
    public void GameStart(string mapName) {
        PhotonNetwork.LoadLevel(mapName);
    }

    // 맵 선택 UI 표시 메서드
    public void OnClickMapSelect() {
        mapSelectUI.SetActive(true);
    }

    // 맵 선택 메서드
    public void OnClickMap(GameObject button) {
        print(button.name);
        selectedMapName = button.name;
    }

    // 맵 선택 완료 메서드
    public void OnClickDone() {
        mapSelectUI.SetActive(false);
    }

    // 안내 창에서 '확인' 메서드
    public void OnClickOk() {
        infoUI.SetActive(false);
    }
}
