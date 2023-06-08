using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DealerSystem : MonoBehaviour
{
    [SerializeField] private List<int> cardsInPlay = new List<int>();
    private List<int> cardCounter = new List<int>();
    public List<int> playerCards = new List<int>();
    public int chosenCard;
    public SelectedInfo cardAssign;
    public int playerHandValue;
    public bool firstCardPlay;
    private CombatManager combatManager;

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
        playerHandValue = 0;
        combatManager = GameObject.Find("CombatManager").GetComponent<CombatManager>();
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
        if (playerHandValue == 21)
        {
            combatManager.jackpotButton.SetActive(true);
        }
        if (playerHandValue > 21)
        {
            Debug.Log("bust!");
            playerCards.Clear();
            playerHandValue = 0;
            UIManager uiSystem = GameObject.Find("UIManager").GetComponent<UIManager>();
            foreach(Button cardButton in uiSystem.cardButtons)
            {
                cardButton.interactable = false;
            }
        }
        ActivateCards(chosenCard);
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

    public void ActivateCards(int chosenCardNum)
    {
        switch (chosenCardNum)
        {
            case 1:
                Debug.Log(chosenCardNum);
                break;
            case 2:
                Debug.Log(chosenCardNum);
                break;
            case 3:
                Debug.Log(chosenCardNum);
                break;
            case 4:
                Debug.Log(chosenCardNum);
                break;
        }
    }

    public void PlayCard(int cardNum)
    {
        if (firstCardPlay)
        {
            firstCardPlay = false;
            playerHandValue -= playerCards[cardNum - 1];
            playerCards.RemoveAt(cardNum - 1);
            UIManager uiSystem = GameObject.Find("UIManager").GetComponent<UIManager>();
            uiSystem.cardButtons[cardNum - 1].interactable = false;
        }
    }
}
