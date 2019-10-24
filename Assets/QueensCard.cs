using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum CardType
{
    PlayableCard, ValueCard, QueenCard, KingCard
}

public class QueensCard : MonoBehaviour
{
    public CardType cardType;
    public int value = 0;
    public string cardName;

    public bool discarded = false;

    public bool demoMode = false;

    public SpriteRenderer cardBack;

    public List<GameObject> pipGos = new List<GameObject>();
    public List<GameObject> decoratorGos = new List<GameObject>();

    //public string bannerName;

    public TextMeshProUGUI bannerText;
    public TextMeshProUGUI valueText;

    public void SetBannerText(string text)
    {
        bannerText.text = text;
    }

    public void SetValueText(int value)
    {
        this.valueText.text = value.ToString();
    }

    public void Hide()
    {
        SetCardBack(true);
    }

    public void Reveal()
    {
        SetCardBack(false);
    }

    private void SetCardBack(bool revealed)
    {
        cardBack.enabled = revealed;
    }

    public void Initialize()
    {
        SetValueText(value);
    }

    public void OnMouseDown()
    {

        //Debug.Log(transform.name);

        if (!demoMode)
        {
            Reveal();
        }

        if (discarded)
        {
            return;
        }
        

        if (cardType != CardType.QueenCard)
        {
            //Debug.Log("DADASD");
            //tempDraw();
        }
            
        else Reveal();

        
        
    }

    // Start is called before the first frame update
    void Start()
    {

        Hide();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
