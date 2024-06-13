using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    private string selectedSingleMapName;
    public GameObject settingUI, mapSelectUI;
    public Button singleStartButton;
    public GameObject multiplayLobbyUI;
    public GameObject okUI, inputNicknameUI;

    public void OnClickSinglePlay() {
        mapSelectUI.SetActive(true);
    }

    // 맵 선택 메서드
    public void OnClickMap(GameObject button) {
        singleStartButton.interactable = true;
        selectedSingleMapName = button.name;
    }

    public void OnClickStartSingle() {
        SceneManager.LoadScene(selectedSingleMapName);
    }

    public void OnClickMultiPlay() {
        multiplayLobbyUI.SetActive(true);
    }

    public void OnClickSetting() {
        settingUI.SetActive(true);
    }

    public void OnClickReset() {
        okUI.SetActive(true);
        PlayerPrefs.DeleteAll();
    }

    public void OnClickOk() {
        okUI.SetActive(false);
    }

    public void OnClickDone() {
        inputNicknameUI.SetActive(false);
    }

    public void OnClickReturn() {
        if (settingUI.activeSelf)
            settingUI.SetActive(false);
        else if (multiplayLobbyUI.activeSelf)
            multiplayLobbyUI.SetActive(false);
    }

    public void OnClickQuit() {
        Application.Quit();
    }
}
