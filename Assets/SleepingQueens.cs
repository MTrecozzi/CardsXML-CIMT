using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpriteDefinition
{
    public string name;
    public Sprite sprite;

}

public class SleepingQueens : MonoBehaviour
{
    [Header("Set in Inspector")]
    public TextAsset deckXML;
    public PT_XMLReader xmlr;

    public GameObject queensCardPrefab;
    public GameObject prefabSprite;



    [Header("Special Card Sprites")]
    public Sprite knight;
    public Sprite magicWand;
    public Sprite jester;
    public Sprite dragon;
    public Sprite potion;

    [Header ("King Card Sprites")]
    public Sprite tempPip;

    public Sprite tempKingSprite;

    [Header("Queen Card Sprites")]
    public List<SpriteDefinition> queenSprites;

    [Header("King Card Sprites")]
    public List<SpriteDefinition> kingSprites;



    public Dictionary<string, Sprite> spritesDict = new Dictionary<string, Sprite>();

    public Dictionary<string, CardType> playableCards = new Dictionary<string, CardType>();

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

        xmlr = new PT_XMLReader();
        xmlr.Parse(deckXML.ToString());

        // Get a pseudo collection of all our playable cards
        PT_XMLHashList xtypes = xmlr.xml["xml"][0]["playableCard"];

        int xBorder = 10;
        Vector2 cardPos = Vector2.zero;

        cardPos.x = -xBorder;
        cardPos.y = 8;

        for (int i = 0; i <xtypes.Count; i++)
        {
            // testing card creation;
            GameObject testCard = Instantiate(queensCardPrefab);
            QueensCard cardData = testCard.GetComponent<QueensCard>();

            // do this for each pip or decorator in our list of decorators. Create previous code to get a list of decorators.
            GameObject pip = Instantiate(prefabSprite) as GameObject;
            SpriteRenderer currentRenderer = pip.GetComponent<SpriteRenderer>();

            currentRenderer.sortingOrder = 1;                     // make it render above card
            pip.transform.parent = testCard.transform;     // make deco a child of card GO
            pip.transform.localPosition = Vector3.zero;
            //pip.transform.localPosition = xtypes[0].att("pos");

            cardData.cardName = xtypes[i].att("type");
            cardData.cardType = CardType.PlayableCard;
            cardData.SetBannerText(cardData.cardName);

            Sprite curPip = null;

            if (xtypes[i].att("type").Equals("king"))
            {
                cardData.SetBannerText(xtypes[i].att("name") + " King");
                curPip = GetKingSprite(xtypes[i].att("name"));
                cardData.cardType = CardType.KingCard;
            }

            else  curPip = spritesDict[cardData.cardName];

            testCard.transform.position = cardPos;

            testCard.gameObject.name = cardData.cardName;

            //Debug.Log(cardData.cardName);

            if (curPip != null)
            {
                currentRenderer.sprite = curPip;
            }
            

            cardPos.x += 2.5f;
            if (cardPos.x > xBorder)
            {
                cardPos.y -= 3.5f;
                cardPos.x = -xBorder;
            }
        }

        PT_XMLHashList xQueens = xmlr.xml["xml"][0]["queenCard"];

        // Rough, way too much duplicate code, could be cleaned with definittions and more organization
        for (int i = 0; i < xQueens.Count; i++)
        {
            // testing card creation;
            GameObject testCard = Instantiate(queensCardPrefab);
            QueensCard cardData = testCard.GetComponent<QueensCard>();

            // do this for each pip or decorator in our list of decorators. Create previous code to get a list of decorators.
            GameObject pip = Instantiate(prefabSprite) as GameObject;
            SpriteRenderer currentRenderer = pip.GetComponent<SpriteRenderer>();

            currentRenderer.sortingOrder = 1;                     // make it render above card
            pip.transform.parent = testCard.transform;     // make deco a child of card GO
            pip.transform.localPosition = Vector3.zero;
            //pip.transform.localPosition = xtypes[0].att("pos");

            cardData.cardName = xQueens[i].att("name");
            cardData.value = int.Parse( xQueens[i].att("value"));
            cardData.cardType = CardType.QueenCard;
            

            Sprite curPip = null;

            cardData.SetBannerText(xQueens[i].att("name") + " Queen");
            curPip = GetQueenSprite(xQueens[i].att("name"));

            cardData.Initialize();

            testCard.transform.position = cardPos;
            testCard.gameObject.name = cardData.cardName;

            //Debug.Log(cardData.cardName);

            if (curPip != null)
                currentRenderer.sprite = curPip;

            cardPos.x += 2.5f;
            if (cardPos.x > xBorder)
            {
                cardPos.y -= 3.5f;
                cardPos.x = -xBorder;
            }
        }

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

    private Sprite GetKingSprite(string _name)
    {
        Sprite sprit = null;

        foreach (SpriteDefinition def in kingSprites)
        {
            if (def.name.Equals(_name))
            {
                sprit = def.sprite;
            }
        }

        return sprit;
    }

    private Sprite GetQueenSprite(string name)
    {
        Sprite sprit = null;

        foreach(SpriteDefinition def in queenSprites)
        {
            if (def.name.Equals(name))
            {
                sprit = def.sprite;
            }
        }

        return sprit;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
