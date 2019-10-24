using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QueensDeck : MonoBehaviour {

    [Header("Dynamically")]

    public List<GameObject> cards;
    public List<GameObject> playableCards;
    public List<GameObject> queensCards;

    public TextAsset layoutXML;
    public PT_XMLReader xmlr;

    public void Start()
    {

        xmlr = new PT_XMLReader();
        xmlr.Parse(layoutXML.ToString());

        PT_XMLHashList slots = xmlr.xml["xml"][0]["slot"];

        Vector3 drawPilePos = new Vector3();

        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].att("type").Equals("drawpile"))
            {
                drawPilePos.x = float.Parse(slots[i].att("x"));
                drawPilePos.y = float.Parse(slots[i].att("y"));
            }
        }
            

        // Parse Draw Pile and Stagger It

        foreach (GameObject card in cards)
        {
            card.transform.position = drawPilePos;
            Debug.Log(drawPilePos);
        }
    }




} // Deck class
