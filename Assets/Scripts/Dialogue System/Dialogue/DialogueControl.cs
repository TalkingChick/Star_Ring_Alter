﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using TMPro;
using UnityEngine.UI;

public class DialogueControl : MonoBehaviour
{
    private KeyManager key;
    private DialogueRunner runner;
    [SerializeField] private DialogueUI UI;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI dialogueText;
    public GameObject[] portraits;

    //player control
    private PlayerControl player;

    //options
    public int currentOption = 0;
    private bool isOptionStarted = false;

    //those are text and name of current displaying text
    private string characterName;
    private string text;

    void Start()
    {
        player = FindObjectOfType<PlayerControl>();
        runner = GetComponentInChildren<DialogueRunner>();
        key = FindObjectOfType<KeyManager>();

        //check whether all things are set up
        if (portraits.Length < 1)
            Debug.LogWarning("haven't set dialogue portraits images yet");
    }

    
    void Update()
    {
        /*
        switch (stateManager.getCurrentState())
        {
            case State.Explore:
                break;

            case State.Dialogue:
                displayName();
                continueDialogue();
                switchOption();
                chooseOption(currentOption);
                break;
        }*/
        displayName();
        continueDialogue();
        switchOption();
        chooseOption(currentOption);

    }

    /**
     * mark the line is completed, so dialogue will go to the next line
     * checking player pressed [key.continueDialogue]
     */
    public void continueDialogue()
    {
        if (Input.GetKeyDown(key.continueDialogue) && !isOptionStarted)
            UI.MarkLineComplete();

        //FOR TESTING   
        /*
        if (Input.GetKey(KeyCode.LeftControl))
            UI.MarkLineComplete();*/
    }

    /**
     * making the name text shows name, the part before ':' in yarn spinner text
     */
    public void displayName()
    {
        if (UI != null) {
            if (UI.getLineText() != null)
            {
                if (UI.getLineText().Contains(":"))
                    characterName = UI.getLineText().Substring(0, UI.getLineText().IndexOf(":"));
            } else
            {
                characterName = "";
            }
            this.nameText.text = characterName;
        } else {
            Debug.Log("cannot find dialogue UI");
        }
    }

    /**
     * press [Previous] or [Next] to select which option is currently choosing
     * player exit animation for the one that just deselect, and play enter animation for one that just select
     * when a option contains nothing (""), it will not be able to selected
     */
    public void switchOption()
    {
        if (Input.GetKeyDown(key.previous) && currentOption > 0)
            currentOption--;
        else if (Input.GetKeyDown(key.next) && currentOption < UI.optionButtons.Count - 1)
        {
            if(!UI.optionButtons[currentOption + 1].GetComponentInChildren<TextMeshProUGUI>().text.Equals(""))
                currentOption++;
        }
    }

    /**
     * press [interact] to choose the current options, and play the animation
     */
    public void chooseOption(int currentOption)
    {
        if (Input.GetKeyDown(key.interact) && isOptionStarted)
        {
            if (UI.optionButtons[currentOption] != null)
                UI.SelectOption(currentOption);
            else
                Debug.LogWarning("options selection is out of index");
        }
    }

    /**
     * set whether is displaying options for player
     * {its being set in Yarn Dialogue UI (onOptionStart & onOptionEnd)} {in inspector area}
     */
    public void setIsOptionStarted(bool isStarted)
    {
        isOptionStarted = isStarted;
        Debug.Log("set option as " + isStarted);
    }

    /**
     * reset all options to empty string ""
     * and, reset the currentOption number to 0
     * {its being set in Yarn Dialogue UI (onOptionEnd)} {in inspector area}
     */
    public void resetOption()
    {
        for (int i = 0; i < UI.optionButtons.Count; i++)
        {
            UI.optionButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = "";
        }
        currentOption = 0;
    }
    
}
