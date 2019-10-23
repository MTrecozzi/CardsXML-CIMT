using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempDiscardPile : MonoBehaviour
{
    public static tempDiscardPile discard;

    public Stack<GameObject> cards = new Stack<GameObject>();


    public void AddCard(QueensCard card)
    {
        card.gameObject.transform.position = transform.position;

        if (cards.Count > 0)
        {
            cards.Peek().gameObject.SetActive(false);
        }

        Debug.Log("card Added");

        card.Reveal();

        card.discarded = true;
        cards.Push(card.gameObject);
        
    }

    // Start is called before the first frame update
    void Awake()
    {
        if (discard == null)
        {
            discard = this;
        }
        else Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
