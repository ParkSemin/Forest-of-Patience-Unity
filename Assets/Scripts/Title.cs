using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public GameObject settingUI;
    public GameObject multiplayLobbyUI;
    public GameObject okUI;

    public void OnClickSinglePlay() {
        SceneManager.LoadScene("Map_1_single");
    }

    public void OnClickMultiPlay() {
        multiplayLobbyUI.SetActive(true);
    }

    public void OnClickSetting() {
        settingUI.SetActive(true);
    }

    public void OnClickReset() {
        okUI.SetActive(true);
        PlayerPrefs.SetInt("BestMin", 0);
        PlayerPrefs.SetInt("BestSec", 0);
    }

    public void OnClickOk() {
        okUI.SetActive(false);
    }

    public void OnClickReturn() {
        if (settingUI.activeSelf)
            settingUI.SetActive(false);
        else if (multiplayLobbyUI.activeSelf)
            multiplayLobbyUI.SetActive(false);
    }
}
