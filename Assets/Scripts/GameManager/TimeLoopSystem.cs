﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * time loop system is a class controlling time loop.
 * it shows current time and the time that triggers the loop [note that only minute will count down]
 * it also manage what data are carried to the next loop
 */
public class TimeLoopSystem : MonoBehaviour
{
    [SerializeField] private int startTime;
    public static float currentTime, loopTime, rate;
    public static bool isPaused;
    private void Awake()
    {
        currentTime = startTime;
        isPaused = false;
        rate = 10;

        loopTime = 1300;
    }

    void Start()
    {
        //load information from last loop
        FindObjectOfType<Codex>().loadDialogueNodesCount();
    }

    void Update()
    {
        if (isPaused)
        {
            
        } else
        {
            loopCountdown(rate);

            if (currentTime >= loopTime)
                reset();
        }
    }

    /**
     * make the time flow
     * @param rate of the time that flow, bigger number means faster
     */
    public static void loopCountdown(float rate)
    {
        currentTime += rate * Time.deltaTime;
    }

    /**
     * make the time flow
     * it will have the default time flow rate, 1
     */
    public static void loopCountDown()
    {
        currentTime += 1.0f * Time.deltaTime;
    }

    /**
     * set the rate of time flow
     */
    public static void setRate(float rate)
    {
        TimeLoopSystem.rate = rate;
    }

    /**
     * set isPaued to true
     */
    public static void pause()
    {
        isPaused = true;
    }

    /**
     * set isPaued to false
     */
    public static void countDown()
    {
        isPaused = false;
    }
    
    /**
     * get whether time flow is paused
     */
    public static bool getPaused()
    {
        return isPaused;
    }

    /**
     * reset the game but save and load from last time loop
     */
    public static void reset()
    {
        FindObjectOfType<Codex>().saveDialgoueNodesCount();

        FindObjectOfType<StateManager>().transitionState(State.Explore);

        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
