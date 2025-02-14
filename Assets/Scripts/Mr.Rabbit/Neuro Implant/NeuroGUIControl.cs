﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using NaughtyAttributes;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class NeuroGUIControl : MonoBehaviour
{
    [SerializeField, BoxGroup("Description Section")] 
    private TextMeshProUGUI neuroTypeText, neuroNameText, neuroDesText;
    [SerializeField, BoxGroup("Description Section")]
    private Image neuroImage;

    [SerializeField, BoxGroup("Neuro Units")]
    private List<NeuroUnit> neuroUnits;

    [SerializeField, BoxGroup("Neuo Units")] private GridLayoutGroup grid;

    //index of currently selected backpack unit from List neuroUnits
    private int currentIndex;

    public static NeuroUnit currentUnit;

    [SerializeField] private PlayerControl player;
    [SerializeField] private NeuroImplantDevice playerNeuroDevice;

    private List<NeuroImplantApp> apps = new List<NeuroImplantApp>();

    //getters & setters
    public int CurrentIndex {get => currentIndex; set => currentIndex = value;}
    void Start()
    {
        // player = FindObjectOfType<PlayerControl>();
        // playerBackpack = FindObjectOfType<PlayerBackpack>();
        //check if everything is set up
        if (neuroTypeText == null) Debug.LogWarning("neuro type text is null");
        if (neuroNameText == null) Debug.LogWarning("neuro name text is null");
        if (neuroDesText == null) Debug.LogWarning("neuro description text is null");
        if (neuroUnits == null) Debug.LogWarning("neuro units is null");
        if (grid == null) Debug.LogWarning("grid layout group is null");

        for (int i = 0; i < neuroUnits.Count; i++) {
            neuroUnits[i].Index = i;
        }

        setCurrentUnit(0);
    }

    
    void Update()
    {
        CurrentIndex = currentIndex;
        changeCurrentUnit();
    }

    /* set imgae, description, name, and type of the item to NeuroUnit
       if there's no item sign to NeuroUnit, then no icon will be shown*/
    public void showApps() {
        //link app list to player neuro implant
        apps.Clear();
        if (playerNeuroDevice == null) Debug.Log("player neuro device is null");
        if (player == null) Debug.Log("player is null");
        for (int i = 0; i < playerNeuroDevice.downloadedApps.Count; i++) {
            if (playerNeuroDevice.downloadedApps[i].GetComponent<NeuroImplantApp>() != null)
                apps.Add(playerNeuroDevice.downloadedApps[i].GetComponent<NeuroImplantApp>());
            else
                Debug.LogWarning(playerNeuroDevice.downloadedApps[i].name + " doesn't have a neuro app component");
        }
        
        //set infomation of item to NeuroUnit and show them
        for (int i = 0; i < neuroUnits.Count; i++)
        {
            neuroUnits[i].NeuroApp = null;
            if (apps.Count > i)
                neuroUnits[i].NeuroApp = apps[i];

            if (neuroUnits[i].NeuroApp != null) {
                neuroUnits[i].Icon = apps[i].Icon;
                neuroUnits[i].NeuroName = apps[i].appName;
                neuroUnits[i].NeuroDes = apps[i].Description;
                neuroUnits[i].NeuroDes = apps[i].DesctiptionAfterUse;
            } else {
                neuroUnits[i].resetDisplayingApp();
            }
            neuroUnits[i].showUnit();
        }

        //set current Unit back to the first one
        setCurrentUnit(0);
    }

    /* show name, type, and description of item in GUI*/
    public void showInfo(NeuroImplantApp app) {
        if (app != null) {
            //make spaces between lower and upper case letters 
            neuroTypeText.text = Regex.Replace(app.Type.ToString(), @"([a-z])([A-Z])", "$1 $2");
            neuroDesText.text = app.Description;
            neuroNameText.text = app.appName;
            neuroImage.color = new Color(255,255,255,255);
            neuroImage.sprite = app.Icon;
        } else {
            neuroTypeText.text = "";
            neuroDesText.text = "";
            neuroNameText.text = "";
            neuroImage.color = new Color(255,255,255,0);
            neuroImage.sprite = null;
            
        }
    }

    public void changeCurrentUnit() {
        int targetIndex = currentIndex;
        if (Input.GetKeyDown(KeyCode.A))
            targetIndex = Mathf.Max(0,currentIndex-1);
        if (Input.GetKeyDown(KeyCode.D))
            targetIndex = Mathf.Min(currentIndex+1, neuroUnits.Count-1);
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            targetIndex = Mathf.Max(0, currentIndex - grid.constraintCount);
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            targetIndex = Mathf.Min(currentIndex + grid.constraintCount, neuroUnits.Count-1);

        if (targetIndex != currentIndex) { 
            if (checkValid(targetIndex)) {
                setCurrentUnit(targetIndex);
            } else {
                Debug.LogWarning(targetIndex + " is out of bound of NeuroUnits list");
            }
        }
    }

    /* return true if targetIndex is a valid index of the NeuroUnits list, false otherwise
       @param targetIndex the index number that player is trying to reach in the backpakcUnits list*/
    public bool checkValid(int targetIndex) {
        if (targetIndex < neuroUnits.Count && targetIndex >= 0)
            return true;
        return false;
    }

    /* set current unit to neuroUnits[index] by invoke the button of that NeuroUnit
       @param index index of the NeuroUnits that should be set to current unit*/
    public void setCurrentUnit(int index) {
        neuroUnits[index].GetComponent<Button>().Select();
        neuroUnits[index].GetComponent<Button>().onClick.Invoke();
        currentIndex = index;

        showInfo(currentUnit.NeuroApp);
    }

    public void useNeuroImplant() {
        currentUnit.useNeuroImplant();
    }
}
