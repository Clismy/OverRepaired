using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public GameObject minutes;
    public GameObject endTime;
    public int timeMinutesStart;
    public int endTimeStart;

    private float timeSeconds;
    private float timeMinutes;

    private const float minutesToDegress = 360f / 60f;

    private void Start()
    {
        timeMinutes = timeMinutesStart;
        endTime.transform.localRotation =  Quaternion.Euler(0f, endTimeStart * minutesToDegress, 0f);
    }

    void ChangeSecondsToMinutes()
    {
        timeMinutes += 1.0f;
        timeSeconds -= 10.0f;
        if(timeMinutes == endTimeStart)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        Debug.Log("This is the end");
    }

    // Update is called once per frame
    void Update()
    {
        timeSeconds += Time.deltaTime *2;
        if(timeSeconds > 10.0f)
        {
            ChangeSecondsToMinutes();
        }

        minutes.transform.localRotation = Quaternion.Euler(0f, timeMinutes * minutesToDegress, 0f);
    }
}
