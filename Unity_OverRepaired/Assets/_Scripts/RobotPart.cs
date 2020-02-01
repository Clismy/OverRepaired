using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotPart : MonoBehaviour
{
    public bool isBroken = false;

    [SerializeField]
    private string[] repairOrder = new string[4];
    public Stack<string> repairOrderStack = new Stack<string>();

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
