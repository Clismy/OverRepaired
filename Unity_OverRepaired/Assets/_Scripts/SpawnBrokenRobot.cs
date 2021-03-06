﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBrokenRobot : MonoBehaviour
{
    public BrokenRobot prefab;
    public float SpawnTime;
    public Transform spawnPostion;
    private float timer = 0;
    public void Start()
    {
        SpawnRobot();
        timer = 0;
    }

    public void Update()
    {
        if (timer < SpawnTime)
        {
            timer += Time.deltaTime;
        }
        else
        {
            SpawnRobot();
            timer = 0;
        }
    }

    public void SpawnRobot()
    {
        Instantiate(prefab, spawnPostion);
    }
}
