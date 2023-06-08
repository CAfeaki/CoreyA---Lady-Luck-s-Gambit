using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealerSystem : MonoBehaviour
{
    [SerializeField] private List<int> cardsInPlay = new List<int>();
    private List<int> cardCounter = new List<int>();
    public List<int> playerCards = new List<int>();
    public int chosenCard;
    public SelectedInfo cardAssign;
    public int playerHandValue = 0;

    void Start()
    {
        for (int i= 0; i < 13; i++)
        {
            cardsInPlay.Add(i+1);
        }
        for (int i = 0; i < 13; i++)
        {
            cardCounter.Add(4);
        }
    }

    void Update()
    {
        if (cardsInPlay.Count <= 1)
        {
            cardsInPlay.Clear();
            cardCounter.Clear();
            Start();
        }
    }

    public void CardPull()
    {
        int randomNum = Random.Range(0, cardsInPlay.Count-1);
        chosenCard = cardsInPlay[randomNum+1];
        playerCards.Add(chosenCard);
        cardCounter[randomNum+1] -= 1;
        if (cardCounter[randomNum+1] == 0)
        {
            cardsInPlay.RemoveAt(randomNum+1);
            cardCounter.RemoveAt(randomNum+1);
        }
        PlayerHand(playerCards.IndexOf(chosenCard));
    }

    public void PlayerHand(int cardIndex)
    {
        int newHandValue = 0;

        cardIndex++;
        cardAssign = GameObject.Find("card " + cardIndex).GetComponent<SelectedInfo>();
        cardAssign.ActivateCard(chosenCard);

        for (int i = 0; i < playerCards.Count; i++)
        {
            newHandValue += playerCards[i];
        }

        playerHandValue = newHandValue;
    }

    public void ActivateCards()
    {

    }
}
