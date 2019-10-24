using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QueensDeck : MonoBehaviour {

    [Header("Dynamically")]

    public List<GameObject> cards;
    public List<GameObject> playableCards;
    public List<GameObject> queensCards;

    public Stack<GameObject> drawStack = new Stack<GameObject>();

    public GameObject discardPile;

    public TextAsset layoutXML;
    public PT_XMLReader xmlr;

    public BoxCollider drawClick;

    public void OnMouseDown()
    {

        if (drawStack.Count > 0)
        {
            tempDiscardPile.discard.AddCard(drawStack.Pop().GetComponent<QueensCard>());
        }
        
    }

    public void Start()
    {

        xmlr = new PT_XMLReader();
        xmlr.Parse(layoutXML.ToString());

        PT_XMLHashList slots = xmlr.xml["xml"][0]["slot"];

        Vector3 drawPilePos = new Vector3();
        Vector3 discardPilePos = new Vector3();

        float drawPileStagger = 0f;

        // Read XML Slots to intialize our draw and discard piles
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].att("type").Equals("drawpile"))
            {
                drawPilePos.x = float.Parse(slots[i].att("x"));
                drawPilePos.y = float.Parse(slots[i].att("y"));
                drawPileStagger = float.Parse(slots[i].att("xstagger"));
            }

            if (slots[i].att("type").Equals("discardpile"))
            {
                discardPilePos.x = float.Parse(slots[i].att("x"));
                discardPilePos.y = float.Parse(slots[i].att("y"));
            }
        }

        tempDiscardPile.discard.transform.position = discardPilePos;

        // Parse Draw Pile and Stagger It

        transform.position = drawPilePos;

        int tempSorting = playableCards.Count + 5;

        for (int i = 0; i < playableCards.Count; i++)
        {

            playableCards[i].transform.position = drawPilePos;
            playableCards[i].GetComponent<QueensCard>().cardBack.sortingOrder = tempSorting;
            drawPilePos.x += drawPileStagger;
            tempSorting--;
        }

        for (int i = 0; i < playableCards.Count; i++)
        {
            drawStack.Push(playableCards[i]);
        }
    }




} // Deck class
