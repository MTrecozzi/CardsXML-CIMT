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

    public List<Decorator> decorators;

    public GameObject queensCardPrefab;
    public GameObject prefabSprite;

    [Header("Rank Sprites")]
    public List<SpriteDefinition> rankSprites;

    [Header("Pips")]
    public Sprite tempPip;


    [Header("Special Card Sprites")]
    public Sprite knight;
    public Sprite magicWand;
    public Sprite jester;
    public Sprite dragon;
    public Sprite potion;

    [Header("Queen Card Sprites")]
    public List<SpriteDefinition> queenSprites;

    [Header("King Card Sprites")]
    public List<SpriteDefinition> kingSprites;


    public List<CardDefinition> cardDefs;

    public Vector2 cardPos;
    int xBorder = 10;



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

    public Vector2 GetNewCardPos()
    {
        cardPos.x += 2.5f;
        if (cardPos.x > xBorder)
        {
            cardPos.y -= 3.5f;
            cardPos.x = -xBorder;
        }

        return cardPos;
    }

    // Start is called before the first frame update
    void Start()
    {
        initDictionary();

        xmlr = new PT_XMLReader();
        xmlr.Parse(deckXML.ToString());

        // Get a pseudo collection of all our playable cards
        PT_XMLHashList xPlayables = xmlr.xml["xml"][0]["playableCard"];

        
        cardPos = Vector2.zero;

        cardPos.x = -xBorder - 2.5f;
        cardPos.y = 8;

        // Create PlayableCards
        for (int i = 0; i <xPlayables.Count; i++)
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

            cardData.cardName = xPlayables[i].att("type");
            cardData.cardType = CardType.PlayableCard;
            cardData.SetBannerText(cardData.cardName);

            Sprite curPip = null;

            if (xPlayables[i].att("type").Equals("king"))
            {
                cardData.SetBannerText(xPlayables[i].att("name") + " King");
                curPip = GetKingSprite(xPlayables[i].att("name"));
                cardData.cardType = CardType.KingCard;
            }

            else  curPip = spritesDict[cardData.cardName];

            testCard.transform.position = GetNewCardPos();

            testCard.gameObject.name = cardData.cardName;

            //Debug.Log(cardData.cardName);

            if (curPip != null)
            {
                currentRenderer.sprite = curPip;
            }
            
        }

        decorators = new List<Decorator>();
        // grab all decorators from the XML file
        PT_XMLHashList xDecos = xmlr.xml["xml"][0]["decorator"];
        Decorator deco;
        for (int i = 0; i < xDecos.Count; i++)
        {
            // for each decorator in the XML, copy attributes and set up location and flip if needed
            deco = new Decorator();
            deco.type = xDecos[i].att("type");
            deco.flip = (xDecos[i].att("flip") == "1");   // too cute by half - if it's 1, set to 1, else set to 0
            deco.scale = float.Parse(xDecos[i].att("scale"));
            deco.loc.x = float.Parse(xDecos[i].att("x"));
            deco.loc.y = float.Parse(xDecos[i].att("y"));
            deco.loc.z = float.Parse(xDecos[i].att("z"));
            decorators.Add(deco);
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

            testCard.transform.position = GetNewCardPos();
            testCard.gameObject.name = cardData.cardName;

            //Debug.Log(cardData.cardName);

            if (curPip != null)
                currentRenderer.sprite = curPip;
        }

        cardDefs = new List<CardDefinition>();
        // first we reference our xml NumberCard Definitions
        PT_XMLHashList xNumberCards = xmlr.xml["xml"][0]["numberCard"];

        // foreach of those definitions

        for (int i = 0; i < xNumberCards.Count; i++)
        {
            CardDefinition cDef = new CardDefinition();
            cDef.rank = int.Parse(xNumberCards[i].att("value"));

            PT_XMLHashList xPips = xNumberCards[i]["pip"];

            if (xPips != null)
            {
                for (int j = 0; j < xPips.Count; j++)
                {
                    deco = new Decorator();
                    deco.type = "pip";
                    deco.flip = (xPips[j].att("flip") == "1");   // too cute by half - if it's 1, set to 1, else set to 0

                    deco.loc.x = float.Parse(xPips[j].att("x"));
                    deco.loc.y = float.Parse(xPips[j].att("y"));
                    deco.loc.z = float.Parse(xPips[j].att("z"));
                    if (xPips[j].HasAtt("scale"))
                    {
                        deco.scale = float.Parse(xPips[j].att("scale"));
                    }
                    cDef.pips.Add(deco);
                }
            }
            cardDefs.Add(cDef);
        }

        for (int i = 0; i <cardDefs.Count; i++)
        {
            GameObject cgo = Instantiate(queensCardPrefab) as GameObject;
            // set cgo transform parent
            QueensCard card = cgo.GetComponent<QueensCard>();

            card.name = cardDefs[i].rank.ToString();
            card.value = cardDefs[i].rank;
            card.cardType = CardType.ValueCard;

            //Add Decorators
            foreach(Decorator decko in decorators)
            {
                GameObject tGO = Instantiate(prefabSprite) as GameObject;
                SpriteRenderer tSR = tGO.GetComponent<SpriteRenderer>();

                tSR.sprite = GetSpriteByRank(card.value);

                tSR.color = Color.black;

                tSR.sortingOrder = 1;                     // make it render above card
                tGO.transform.parent = cgo.transform;     // make deco a child of card GO
                tGO.transform.localPosition = decko.loc;   // set the deco's local position

                if (decko.flip)
                {
                    tGO.transform.rotation = Quaternion.Euler(0, 0, 180);
                }

                if (decko.scale != 1)
                {
                    tGO.transform.localScale = Vector3.one * decko.scale;
                }

                tGO.name = decko.type;

                

                //card.decoGOs.Add(tGO);
            } // end of foreach decorator creation

            cgo.transform.position = GetNewCardPos();
        }



    }

    private Sprite GetSpriteByRank(int spriteNumber)
    {
        Sprite sprit = null;

        foreach (SpriteDefinition def in rankSprites)
        {
            if (def.name.Equals(spriteNumber.ToString()))
            {
                sprit = def.sprite;
            }
        }

        return sprit;
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
