﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class ManagerConsole : InteractiveObj
{
    public bool isElevatorActivated;


    [SerializeField, BoxGroup("elevator")]
    private GameObject elevatorDoor;

    private bool triggered = false;

    public override void interact()
    {
        isElevatorActivated = true;
        Player.ChangeState(Player.stateExplore);
        if (!triggered)
        {
            triggered = true;
            Player.UIControl.ChangeState(Player.UIControl.stateIdle);
            Player.ChangeState(Player.stateExplore);
            if (elevatorDoor.activeSelf)
            {
                Player.NPCToTalk = gameObject;
                Player.talkToNPC("MrRabbit.Manager_Office_Elevator_Access_Fixed");
            }
            else
            {
                Player.NPCToTalk = gameObject;
                Player.talkToNPC("MrRabbit.Manager_Office_Elevator_Access_UnFixed");
            }
        }
        else {
            Player.NPCToTalk = gameObject;
            Player.talkToNPC("Manager.DoubleCheck");
        }
    }
}
