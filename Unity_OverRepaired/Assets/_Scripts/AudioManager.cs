using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class AudioManager : MonoBehaviour
{
    [EventRef]
    public string robotMove;
    FMOD.Studio.EventInstance RobotMove;
    FMOD.Studio.ParameterInstance Stop;
    
    public void RobotMoveStart()
    {
        RobotMove = FMODUnity.RuntimeManager.CreateInstance(robotMove);
        RobotMove.getParameter("Stop", out Stop);
        RobotMove.start();
    }

    public void RobotMoveStop()
    {
        Stop.setValue(1.0f);
    }

}
