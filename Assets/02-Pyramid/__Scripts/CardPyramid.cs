using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CardPyramid : Card {	
    [Header("Set Dynamically: CardPyramid")]	
    public eCardState state = eCardState.drawpile;	
    public List<CardPyramid> hiddenBy = new List<CardPyramid>();	
    public int layoutID;	
    public SlotDef slotDef;	

    override public void OnMouseUpAsButton() {	
        Pyramid.S.CardClicked(this);	
        base.OnMouseUpAsButton();	
    }
}
