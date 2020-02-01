using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotPart : MonoBehaviour
{
    public bool isBroken = false;

    [SerializeField]
    private string[] repairOrder = new string[4];
    private Stack<string> repairOrderStack = new Stack<string>();

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

    public void Start()
    {
        for (int i = repairOrder.Length - 1; i >= 0; i--)
        {
            repairOrderStack.Push(repairOrder[i]);
        }
    }

    public string getCurrentRepair()
    {
        return repairOrderStack.Peek();
    }

    public void currentRepairDone()
    {
        if (repairOrderStack.Count > 0)
        {
            if (repairOrderStack.Count == 1)
            {
                isBroken = false;
            }
            repairOrderStack.Pop();
        }
    }
}
