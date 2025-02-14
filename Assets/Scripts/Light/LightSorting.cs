﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSorting : MonoBehaviour
{
    private PlayerControl player;
    
    void Start()
    {
        player = FindObjectOfType<PlayerControl>();
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerControl>() != null)
            collision.GetComponent<SpriteRenderer>().sortingLayerName = "2D Light";
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerControl>() != null)
            collision.GetComponent<SpriteRenderer>().sortingLayerName = "Default";
    }
}
