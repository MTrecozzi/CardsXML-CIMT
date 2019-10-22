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

    public void Initialize()
    {
        SetValueText(value);

    }

    public void OnMouseDown()
    {
        Debug.Log(this.bannerText.text);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
