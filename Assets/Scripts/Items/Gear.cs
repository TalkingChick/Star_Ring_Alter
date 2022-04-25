﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : Item
{
    public override void useItem()
    {
        PlayerControl player = FindObjectOfType<PlayerControl>();
        player.ChangeState(player.stateExplore);
        player.talkToSelf("Response.gear");
    }
}
