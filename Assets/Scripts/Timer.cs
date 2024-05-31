using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private float sec;
    private int min;

    [SerializeField]
    private TextMeshPro TimerText;

    // Update is called once per frame
    void Update()
    {
        sec += Time.deltaTime;
        if ((int)sec > 59) {
            sec = 0;
            min++;
        }

        TimerText.text = string.Format("{0:D2}:{1:D2}", min, (int)sec);
    }
}
