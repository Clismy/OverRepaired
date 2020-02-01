using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairStation : MonoBehaviour
{
    [SerializeField] string type;
    public float workTime;
    public float timer;
    public Transform placePosition;
    public WorkProgress ProgressBar;
    public bool work(RobotPart part)
    {
        ProgressBar.Working(this);
        if (part.getCurrentRepair() == type)
        {
            Debug.Log(timer);
            if (timer < workTime)
            {
                timer += Time.deltaTime;
            }
            else
            {
                part.currentRepairDone();
            }
            return true;
        }
        return false;
    }
    public void resetWork()
    {
        timer = 0;
    }
}
