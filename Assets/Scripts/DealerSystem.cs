using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DealerSystem : MonoBehaviour
{
    [Header("Card Tracking")]
    public List<int> cardsInPlay = new List<int>();
    public List<int> cardCounter = new List<int>();
    public List<int> playerCards = new List<int>();
    public int chosenCard;
    public GameObject cardAssign;
    public SelectedInfo selectedInfo;
    public int cardIndex;
    public int playerHandValue;
    public bool firstCardPlay;
    public Character activeCharacter;
    [Header("Buff Tracker")]
    public List<int> roundToEnd = new List<int>();
    public List<int> turnToEnd = new List<int>();
    public List<int> cardToEnd = new List<int>();
    public List<Character> charScripts = new List<Character>();
    private CardBuffs cardBuffs;

    private CombatManager combatManager;
    private UIManager uiManager;
    private TurnManager turnManager;

    void Start()
    {
        for (int i= 0; i < 13; i++) // creating deck
        {
            cardsInPlay.Add(i+1);
        }
        for (int i = 0; i < 13; i++) // card counter : once a card is played 4 times, it's removed from the deck
        { 
            cardCounter.Add(4);
        }
        playerHandValue = 0;
        combatManager = GameObject.Find("CombatManager").GetComponent<CombatManager>();
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        turnManager = GameObject.Find("TurnManager").GetComponent<TurnManager>();
        cardBuffs = GameObject.Find("DealerSystem").GetComponent<CardBuffs>();
    }

    void Update()
    {
        activeCharacter = uiManager.activeCharacter; // assign all changes to the active character

        if (cardsInPlay.Count <= 1) // reset deck once all cards have been played
        {
            cardsInPlay.Clear();
            cardCounter.Clear();
            Start();
        }
    }

    public void CardPull() // "PULL" runs this
    {
        int randomNum = Random.Range(0, cardsInPlay.Count);
        chosenCard = cardsInPlay[randomNum];
        cardCounter[randomNum] -= 1;
        if (cardCounter[randomNum] <= 0) // remove selected card from deck
        {
            cardsInPlay.RemoveAt(randomNum);
            cardCounter.RemoveAt(randomNum);
        }
        playerCards.Add(chosenCard); // give player their card
        PlayerHand(); 
        cardIndex = 0;
        cardBuffs.ActivateCards(chosenCard, false, false, true, activeCharacter);
        if (playerHandValue == 21)
        {
            combatManager.jackpotButton.SetActive(true);
        }
        if (playerHandValue > 21) // reset cards in bust
        {
            playerCards.Clear();
            playerHandValue = 0;
            UIManager uiSystem = GameObject.Find("UIManager").GetComponent<UIManager>();
            foreach (Button cardButton in uiSystem.cardButtons) 
            {
                cardButton.interactable = false;
            }
            foreach (Character CS in uiManager.characterScripts)
            {
                CS.attackStat = CS.baseAttack;
                CS.graceStat = CS.baseGrace;
                CS.healthStat = CS.baseHealth;
                if (CS.currHealth > CS.healthStat)
                {
                    CS.currHealth = CS.healthStat;
                }
            }

        }
    }

    public void PlayerHand() // organising the player's hand in the UI
    {
        int newHandValue = 0;
        int cardIndex = 1;
        int i = 0;

        cardAssign = GameObject.Find("card " + cardIndex);
        selectedInfo = cardAssign.GetComponent<SelectedInfo>();
        Button cardButton = cardAssign.GetComponent<Button>(); 

        while (i < playerCards.Count) // sort cards so that the player hand list doesnt break
        {
            if (!cardButton.interactable)
            {
                break;
            }
            cardIndex++;
            cardAssign = GameObject.Find("card " + cardIndex);
            selectedInfo = cardAssign.GetComponent<SelectedInfo>();
            cardButton = cardAssign.GetComponent<Button>();
        }

        selectedInfo.ActivateCard(chosenCard);

        for (i = 0; i < playerCards.Count; i++)
        {
            newHandValue += playerCards[i];
        }

        playerHandValue = newHandValue;
    }

    public void ActiveCounter(int cardNum, bool addToCounter, bool resetFromCounter) // tracking how long buffs last
    {
        if (addToCounter)
        {
            int roundsActive;
            if (cardNum >= 10)
            {
                roundsActive = 0; // jack - king only last one round
            }
            else
            {
                roundsActive = 1; // other cards last 2
            }
            roundToEnd.Add(turnManager.roundsHad + roundsActive);
            turnToEnd.Add(activeCharacter.characterNum);
            cardToEnd.Add(cardNum);
        }
        else if (resetFromCounter)
        {
            roundToEnd.RemoveAt(0);
            turnToEnd.RemoveAt(0);
            cardToEnd.RemoveAt(0);
            Debug.Log("Effects wore off!");
        }
    }

    public void PlayCard(SelectedInfo cardData) // called when card is pressed. buffs stored in CardBuffs.cs
    {
        if (firstCardPlay)
        {
            if (cardData.cardNum > 3) // reset passive
            {
                cardBuffs.ActivateCards(cardData.cardNum, false, true, true, activeCharacter);
            }
            cardBuffs.ActivateCards(cardData.cardNum, true, false, false, activeCharacter); // activate play effect
            playerHandValue -= cardData.cardNum;
            int cardIndex = playerCards.IndexOf(cardData.cardNum);
            playerCards.RemoveAt(cardIndex);
            UIManager uiSystem = GameObject.Find("UIManager").GetComponent<UIManager>();
            uiSystem.cardButtons[cardData.cardType - 1].interactable = false; // disable card slot
        }
    }

    public int DealCards() // for initial card shuffle
    {
        int randomNum = Random.Range(0, cardsInPlay.Count);
        chosenCard = cardsInPlay[randomNum];
        cardCounter[randomNum] -= 1;
        if (cardCounter[randomNum] == 0)
        {
            cardsInPlay.RemoveAt(randomNum);
            cardCounter.RemoveAt(randomNum);
        }
        return chosenCard;
    }

    public void FirstDeal(SelectedInfo cardInfo) // to add the first card to the player's hand
    {
        playerCards.Add(cardInfo.cardNum);
        PlayerHand();
        selectedInfo.cardNum = cardInfo.cardNum;
        cardBuffs.ActivateCards(cardInfo.cardNum, false, false, true, activeCharacter);
    }


}
