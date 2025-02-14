﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStateIdle : UIStateBase
{
    public override void EnterState(UIControl UI) {
        //close selection menu first, to make sure it resets
        UI.closeSelectionMenu();
    }
    public override void UpdateState(UIControl UI) {
        if (!UI.isInMain_1)
        {
            if (UI.Player.CanUI)
            {
                if (Input.GetKeyUp(KeyCode.Tab))
                    UI.ChangeToIntelState();
                if (Input.GetKeyUp(UI.Key.openBackpack))
                    UI.openInventory();
                if (Input.GetKeyUp(UI.Key.openNeuro))
                    UI.openNeuro();
            }
        }
    }
    public override void LeaveState(UIControl UI) {
        //change player state when change UI state into state that have real funtionality
        UI.Player.ChangeState(UI.Player.stateUI);
    }
}
