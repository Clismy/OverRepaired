using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotPart : MonoBehaviour
{
    public bool isBroken = false;
    [Serializable]
    public enum parts
    {
        Head,
        Chest,
        RightLeg,
        RightArm,
        LeftArm,
        LeftLeg
    }
    public parts whatPart;


}
