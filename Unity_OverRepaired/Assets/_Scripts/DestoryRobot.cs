using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryRobot : MonoBehaviour
{

    private void OnTriggerEnter(Collider collision)
    {
        var robot = collision.gameObject.GetComponent<BrokenRobot>();
        if (robot != null)
        {
            int numberOfBrokenParts = 0;
            for (int i = 0; i < robot.parts.Length; i++)
            {
                if (robot.transform.GetChild(i).childCount > 0)
                {
                    if (!robot.transform.GetChild(i).GetChild(0).GetComponent<RobotPart>().isBroken)
                    {
                        continue;
                    }
                }
                numberOfBrokenParts++;
            }

            Destroy(robot.gameObject,4);
        }
    }
}
