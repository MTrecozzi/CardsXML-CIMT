using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepingQueens : MonoBehaviour
{
    [Header("Set in Inspector")]
    public TextAsset deckXML;
    public PT_XMLReader xmlr;

    public GameObject queensCardPrefab;
    public GameObject prefabSprite;



    [Header("Sprites")]
    public Sprite knight;
    public Sprite magicWand;
    public Sprite jester;
    public Sprite dragon;
    public Sprite potion;

    public Sprite tempPip;

    public Dictionary<string, Sprite> spritesDict = new Dictionary<string, Sprite>();

    public void initDictionary()
    {
        spritesDict.Add("knight", knight);
        spritesDict.Add("wand", magicWand);
        spritesDict.Add("jester", jester);
        spritesDict.Add("potion", potion);
        spritesDict.Add("dragon", dragon);
    }

    // Start is called before the first frame update
    void Start()
    {
        initDictionary();

        // testing card creation;
        GameObject testCard = Instantiate(queensCardPrefab);
        QueensCard cardData = testCard.GetComponent<QueensCard>();

        xmlr = new PT_XMLReader();
        xmlr.Parse(deckXML.ToString());

        // Get a pseudo collection of all our playable cards
        PT_XMLHashList xtypes = xmlr.xml["xml"][0]["playableCard"];

        

        // do this for each pip or decorator in our list of decorators. Create previous code to get a list of decorators.
        GameObject pip = Instantiate(prefabSprite) as GameObject;
        SpriteRenderer currentRenderer = pip.GetComponent<SpriteRenderer>();

        currentRenderer.sortingOrder = 1;                     // make it render above card
        pip.transform.parent = testCard.transform;     // make deco a child of card GO
        pip.transform.localPosition = Vector3.zero;
        //pip.transform.localPosition = xtypes[0].att("pos");

        cardData.cardName = xtypes[0].att("type");
        cardData.SetBannerText(cardData.cardName);

        Debug.Log(cardData.cardName);

        currentRenderer.sprite = spritesDict[cardData.cardName];





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
