﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardType
{
    PlayableCard, ValueCard, QueenCard
}

public class QueensCard : MonoBehaviour
{
    public CardType cardType;

    public int value = 0;

    public string title;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
