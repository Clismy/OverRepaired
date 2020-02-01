using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public GameObject minutes;
    public GameObject endTime;
    public EndScore endScore;
    public int timeMinutesStart;
    public int endTimeStart;

    public GameObject Test;

    private float timeSeconds;
    private float timeMinutes;

    private const float minutesToDegress = 360f / 60f;

    private void Start()
    {
        timeMinutes = timeMinutesStart;
        endTime.transform.localRotation = Quaternion.Euler(0f, 0f, endTimeStart * -minutesToDegress);
    }

    void ChangeSecondsToMinutes()
    {
        timeMinutes += 1.0f;
        timeSeconds -= 10.0f;
        if (timeMinutes == endTimeStart)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        endScore.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        timeSeconds += Time.deltaTime * 2;
        if (timeSeconds > 10.0f)
        {
            ChangeSecondsToMinutes();
        }

        minutes.transform.localRotation = Quaternion.Euler(0f, 0f, timeMinutes * -minutesToDegress);
    }
}
