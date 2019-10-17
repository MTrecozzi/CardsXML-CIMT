using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepingQueens : MonoBehaviour
{
    [Header("Set in Inspector")]
    public TextAsset deckXML;

    public PT_XMLReader xmlr;

    public GameObject queensCardPrefab;

    // Start is called before the first frame update
    void Start()
    {
        GameObject testCard = Instantiate(queensCardPrefab);

        QueensCard cardData = testCard.GetComponent<QueensCard>();

        xmlr = new PT_XMLReader();
        xmlr.Parse(deckXML.ToString());


        // Reference Deck.readDeck;
        // cardData.setType (xmlParser.getData[however, that's done].

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
