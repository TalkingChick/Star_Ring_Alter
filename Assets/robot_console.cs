﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class robot_console : InteractiveObj
{
    public GameObject robot;

    void Start()
    {

    }

    void Update()
    {

    }
    public override void interact()
    {
        if (FindObjectOfType<DeliveryTunnel>().sentRobot)
            robot.SetActive(true);
    }
}