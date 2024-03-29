﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eCardState
{
    drawpile,
    tableau,
    target,
    discard
}



public class CardProspector : Card
{ // Make sure CardProspector extends Card

    [Header("Set Dynamically: CardProspector")]
    public eCardState state = eCardState.drawpile;

    public List<CardProspector> hiddenBy = new List<CardProspector>();

    // The layoutID matches this card to the tableau XML if it's a tableau card
    public int layoutID;

    // The SlotDef class stores information pulled in from the LayoutXML <slot>
    public SlotDef slotDef;

    override public void OnMouseUpAsButton()
    {
        Prospector.S.CardClicked(this);
        base.OnMouseUpAsButton();           
    }

}