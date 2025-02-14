﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class InteractiveObj : MonoBehaviour
{

    //COMMON     COMMON     COMMON     COMMON     COMMON     COMMON     COMMON     COMMON     COMMON     COMMON
    private Vector2 position;
    private Color themeColor;
    [SerializeField] private GameObject main_character;
    public bool interactonly;
   [SerializeField] private bool canUse = true, canInven = true, canNeuro = true;
    [SerializeField]private InvestIcon invest_icon;
    public DialogueRunner runner;
    PlayerControl playerControl;

    [SerializeField] private bool isTimeToTalk; //player can only talk to NPC when they have time to talk    [NPC is able to talk to player]

    [SerializeField, Header("Indicator")] GameObject indicator;
    [SerializeField, Range(0,5.0f)] float indicatorYPos; //higher number means more 
    private Vector2 indicatorPos; //indicator position
    private GameObject currentIndicator; //record the newly created indicator

    //getters & setters

    public float invest_icon_offset=0;

    public bool CanUse {get {return canUse;}}
    public bool CanInven {get {return canInven;}}
    public bool CanNeuro {get {return canNeuro;}}
    public PlayerControl Player { get => playerControl; }

   void Awake() {
       invest_icon = GameObject.FindGameObjectWithTag("invest_icon").GetComponent<InvestIcon>();
   }

    void Start()
    {
        playerControl = FindObjectOfType<PlayerControl>();
        //invest_icon = GameObject.FindGameObjectWithTag("invest_icon");
        main_character = FindObjectOfType<PlayerControl>().gameObject;
        indicatorPos = position + new Vector2(0, indicatorYPos);
        playerControl = FindObjectOfType<PlayerControl>();
    }

    void Update()
    {
        if (playerControl == null) playerControl = FindObjectOfType<PlayerControl>();   
        if (main_character == null) main_character = FindObjectOfType<PlayerControl>().gameObject;
        if (playerControl.DetectingObj != null && playerControl.DetectingObj != main_character && PlayerControl.show_invest)
        {
            invest_icon_set();
        }
        else { invest_icon.gameObject.SetActive(false); }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (currentIndicator != null)
            {
                Destroy(currentIndicator);
            }
        }
    }

    public Vector2 getIndicatorPos()
    {
        return indicatorPos;
    }
    private void invest_icon_set()
    {
        invest_icon.gameObject.SetActive(true);

        invest_icon.transform.position = Camera.main.WorldToScreenPoint(new Vector3(playerControl.DetectingObj.transform.position.x, playerControl.DetectingObj.transform.position.y +2+invest_icon_offset, playerControl.DetectingObj.transform.position.z));
    }


    public virtual void interact()
    {
        Debug.Log("INTERACT");
    }

    public virtual void useItem() {
        //Debug.Log("using item");
        playerControl.InteractingObj = playerControl.gameObject;
    }

    public virtual void useNeuroImplant() {
        //Debug.Log("using neuro implant");
        playerControl.InteractingObj = playerControl.gameObject;
    }

    public IEnumerator waitToChangeToExploreState() {
        yield return new WaitForSeconds(0.5f);
        FindObjectOfType<PlayerControl>().ChangeState(FindObjectOfType<PlayerControl>().stateExplore);
    }
}
