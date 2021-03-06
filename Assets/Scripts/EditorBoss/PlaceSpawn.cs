﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlaceSpawn : MonoBehaviour
{
    public bool spawn1;
    public bool spawn2;

    public SpriteRenderer sprite1;
    public SpriteRenderer sprite2;

    public RawImage square;


    public void Start()
    {  
        spawn1 = false;
        spawn2 = false;
    }

	public void Spawn(int nb)
    {
        if (nb == 1)
        {
            spawn1 = true;
            spawn2 = false;
        }
        else if (nb == 2)
        {
            spawn1 = false;
            spawn2 = true;
        }

    }

    public void Click()
    {
        sprite1.transform.parent = null;
        Vector2 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        if (spawn1)
        {
            sprite1.transform.position = mousePos;
            sprite1.transform.parent = null;
        }
        else if (spawn2)
        {
            sprite2.transform.position = mousePos;
            sprite2.transform.parent = null;
        }
    }
}
