﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStateNeuro : UIStateBase
{
    public override void EnterState(UIControl UI) {
        UI.openGUI += UI.openNeuro;
        UI.closeGUI += UI.closeNeuro;
        UI.openGUI();
        UI.showNeuroApps();

        if (UI.IsNeuroOnly){
            UI.disableTab -= UI.disableNeuroImplantTab;
            UI.disableTab();
            UI.disableTab += UI.disableNeuroImplantTab;
        }
    }
    public override void UpdateState(UIControl UI) {
        //press Neuro Implant or Mr.Rabbit one more time to close it, then change UI state to idle and player state to explore
        if (UI.Player.CanUI) {
            if (Input.GetKeyUp(UI.Key.openNeuro) || Input.GetKeyUp(UI.Key.openRabbit) || Input.GetKeyUp(KeyCode.Escape))
            {
                UI.closeNeuro();
                UI.Player.ChangeState(UI.Player.stateExplore);
                UI.ChangeState(UI.stateIdle);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            UI.Player.ChangeState(UI.Player.stateExplore);
            UI.ChangeState(UI.stateIdle);
            UI.useNeuroImplant();
        }

        if (!UI.IsNeuroOnly) {
            if (Input.GetKeyDown(UI.Key.next)) {
                UI.ChangeState(UI.stateMap);
            }
            if (Input.GetKeyDown(UI.Key.previous))
            {
                UI.ChangeState(UI.stateInventory);
            }
        }
    }
    public override void LeaveState(UIControl UI) {
        UI.closeGUI();
        UI.openGUI -= UI.openNeuro;
        UI.closeGUI -= UI.closeNeuro;

        UI.activeTab();
        UI.IsNeuroOnly = false;
    }
}
