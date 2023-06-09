using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DealerSystem : MonoBehaviour
{
    private List<int> cardsInPlay = new List<int>();
    private List<int> cardCounter = new List<int>();
    public List<int> playerCards = new List<int>();
    public int chosenCard;
    public GameObject cardAssign;
    public SelectedInfo cardScript;
    public int cardIndex;
    public int playerHandValue;
    public bool firstCardPlay;
    private CombatManager combatManager;
    private UIManager uiManager;
    private TurnManager turnManager;
    public Character activeCharacter;
    public List<int> roundToEnd = new List<int>();
    public List<int> turnToEnd = new List<int>();
    public List<int> cardToEnd = new List<int>();

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
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        turnManager = GameObject.Find("TurnManager").GetComponent<TurnManager>();
    }

    void Update()
    {
        activeCharacter = uiManager.activeCharacter;


        if (cardsInPlay.Count <= 1)
        {
            cardsInPlay.Clear();
            cardCounter.Clear();
            Start();
        }
    }

    public void CardPull(bool isPlayer, Enemy enemyScript)
    {
        int randomNum = 0;// = Random.Range(0, cardsInPlay.Count - 1);
        chosenCard = cardsInPlay[randomNum];
        cardCounter[randomNum+1] -= 1;
        if (cardCounter[randomNum+1] == 0)
        {
            cardsInPlay.RemoveAt(randomNum+1);
            cardCounter.RemoveAt(randomNum+1);
        }
        if (isPlayer)
        {
            playerCards.Add(chosenCard);
            PlayerHand();
            cardIndex = 0;
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
                foreach (Button cardButton in uiSystem.cardButtons)
                {
                    cardButton.interactable = false;
                }
            }
            ActivateCards(chosenCard, false, false);
        }
        else if (!isPlayer)
        {
            enemyScript.enemyCards.Add(chosenCard);
            GameObject cardToActivate = uiManager.enemyCardButtons[enemyScript.enemyCards.Count];
            cardToActivate.SetActive(true);
        }
    }

    public void PlayerHand()
    {
        int newHandValue = 0;
        int cardIndex = 1;
        int i = 0;

        cardAssign = GameObject.Find("card " + cardIndex);
        cardScript = cardAssign.GetComponent<SelectedInfo>();
        Button cardButton = cardAssign.GetComponent<Button>(); 

        while (i < playerCards.Count)
        {
            if (!cardButton.interactable)
            {
                break;
            }
            cardIndex++;
            cardAssign = GameObject.Find("card " + cardIndex);
            cardScript = cardAssign.GetComponent<SelectedInfo>();
            cardButton = cardAssign.GetComponent<Button>();
        }

        cardScript.ActivateCard(chosenCard);

        for (i = 0; i < playerCards.Count; i++)
        {
            newHandValue += playerCards[i];
        }

        playerHandValue = newHandValue;
    }









    public void ActivateCards(int chosenCardNum, bool beingPlayed, bool resetBuff)
    {
        switch (chosenCardNum) 
        {
            case 1:
                if (beingPlayed)
                {
                    activeCharacter.attackStat += 3;
                    activeCharacter.graceStat += 3;
                    activeCharacter.HealCharacter(3);

                    roundToEnd.Add(turnManager.roundsHad + 2);
                    turnToEnd.Add(activeCharacter.characterNum);
                    cardToEnd.Add(chosenCardNum);
                }
                else if (resetBuff)
                {
                    activeCharacter.attackStat -= 3;
                    activeCharacter.graceStat -= 3;

                    roundToEnd.RemoveAt(0);
                    turnToEnd.RemoveAt(0);
                    cardToEnd.RemoveAt(0);

                    Debug.Log("card 1 ended!");
                }

                break;
            case 2:
                if (beingPlayed)
                {
                    activeCharacter.attackStat += 4;
                    activeCharacter.graceStat += 4;
                    activeCharacter.HealCharacter(4);
                }
                break;
            case 3:
                if (beingPlayed)
                {
                    activeCharacter.attackStat += 4;
                    activeCharacter.graceStat += 4;
                    activeCharacter.HealCharacter(4);
                }
                break;
            case 4:
                break;
            case 5:
                Debug.Log(chosenCardNum);
                break;
            case 6:
                Debug.Log(chosenCardNum);
                break;
            case 7:
                Debug.Log(chosenCardNum);
                break;
            case 8:
                Debug.Log(chosenCardNum);
                break;
            case 9:
                Debug.Log(chosenCardNum);
                break;
            case 10:
                Debug.Log(chosenCardNum);
                break;
            case 11:
                Debug.Log(chosenCardNum);
                break;
            case 12:
                Debug.Log(chosenCardNum);
                break;
            case 13:
                Debug.Log(chosenCardNum);
                break;
        }
    }


    public void PlayCard(int cardNum)
    {
        if (firstCardPlay)
        {
            firstCardPlay = false;
            ActivateCards(playerCards[cardNum-1], true, false);
            playerHandValue -= playerCards[cardNum - 1];
            playerCards.RemoveAt(cardNum - 1);
            UIManager uiSystem = GameObject.Find("UIManager").GetComponent<UIManager>();
            uiSystem.cardButtons[cardNum - 1].interactable = false;
        }
    }


}
