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


        // Get a pseudo collection of all our playable cards
        PT_XMLHashList xtypes = xmlr.xml["xml"][0]["playableCard"];

        // for each of our defined playable cards, print out their type
        for (int i = 0; i < xtypes.Count; i++)
        {
            string cardName;
            cardName = xtypes[i].att("type");

            // junk code for practice, if our card has a name, add it before the type, I.e Fire King.
            if (!xtypes[i].att("name").Equals(""))
            {
                cardName = xtypes[i].att("name") + " " + cardName;
            }

            // Debug.Log the name of all our playable cards.
            Debug.Log(cardName);
        }

        // Reference Deck.readDeck;
        // cardData.setType (xmlParser.getData[however, that's done].

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
