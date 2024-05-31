using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public GameObject settingUI;
    public GameObject okUI;

    public void OnClickSinglePlay() {
        SceneManager.LoadScene("MainScene");
    }

    public void OnClickMultiPlay() {

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
        settingUI.SetActive(false);
    }
}
