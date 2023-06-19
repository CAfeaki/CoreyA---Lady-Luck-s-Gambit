using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DealerSystem : MonoBehaviour
{
    [Header("Card Tracking")]
    private List<int> cardsInPlay = new List<int>();
    private List<int> cardCounter = new List<int>();
    public List<int> playerCards = new List<int>();
    public int chosenCard;
    public GameObject cardAssign;
    public SelectedInfo cardScript;
    public int cardIndex;
    public int playerHandValue;
    public bool firstCardPlay;
    public Character activeCharacter;
    [Header("Buff Tracker")]
    public List<int> roundToEnd = new List<int>();
    public List<int> turnToEnd = new List<int>();
    public List<int> cardToEnd = new List<int>();
    public List<Character> charScripts = new List<Character>();

    private CombatManager combatManager;
    private UIManager uiManager;
    private TurnManager turnManager;

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
        int randomNum = Random.Range(0, cardsInPlay.Count - 1);
        chosenCard = cardsInPlay[randomNum];
        cardCounter[randomNum] -= 1;
        if (cardCounter[randomNum] == 0)
        {
            cardsInPlay.RemoveAt(randomNum+1);
            cardCounter.RemoveAt(randomNum+1);
        }
        if (isPlayer)
        {
            playerCards.Add(chosenCard);
            PlayerHand();
            cardIndex = 0;
            ActivateCards(chosenCard, false, false, true);
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
                foreach (Button cardButton in uiSystem.cardButtons) // reset cards in bust
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


    void ActiveCounter(int cardNum, bool addToCounter, bool resetFromCounter)
    {
        if (addToCounter)
        {
            roundToEnd.Add(turnManager.roundsHad + 2);
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

    public void ActivateCards(int chosenCardNum, bool beingPlayed, bool resetBuff, bool activatePassive)
    {
        switch (chosenCardNum) 
        {
            case 1:
                if (beingPlayed)
                {
                    activeCharacter.attackStat += 3;
                    activeCharacter.graceStat += 3;
                    activeCharacter.healthStat += 3;
                    activeCharacter.HealCharacter(3);
                    ActiveCounter(chosenCardNum, true, false);
                }
                else if (resetBuff)
                {
                    activeCharacter.attackStat -= 3;
                    activeCharacter.graceStat -= 3;
                    activeCharacter.healthStat -= 3;
                    ActiveCounter(0, false, true);
                }

                break;
            case 2:
                if (beingPlayed)
                {
                    activeCharacter.attackStat += 4;
                    activeCharacter.graceStat += 4;
                    activeCharacter.healthStat += 4;
                    activeCharacter.HealCharacter(4);
                    ActiveCounter(chosenCardNum, true, false);
                }
                else if (resetBuff)
                {
                    activeCharacter.attackStat -= 4;
                    activeCharacter.graceStat -= 4;
                    activeCharacter.healthStat -= 4;
                    ActiveCounter(0, false, true);
                }
                break;
            case 3:
                if (beingPlayed)
                {
                    activeCharacter.attackStat += 5;
                    activeCharacter.graceStat += 5;
                    activeCharacter.healthStat += 5;
                    activeCharacter.HealCharacter(5);
                    ActiveCounter(chosenCardNum, true, false);
                }
                else if (resetBuff)
                {
                    activeCharacter.attackStat -= 5;
                    activeCharacter.graceStat -= 5;
                    activeCharacter.healthStat -= 5;
                    ActiveCounter(0, false, true);
                }
                break;
            case 4:
                if (activatePassive && !resetBuff)
                {
                    foreach (Character charToBuff in charScripts)
                    {
                        charToBuff.attackStat += 2;
                        charToBuff.healthStat += 2;
                        charToBuff.HealCharacter(2);
                    }
                }
                else if (activatePassive && resetBuff)
                {
                    foreach (Character charToBuff in charScripts)
                    {
                        charToBuff.attackStat -= 2;
                        charToBuff.healthStat -= 2;
                    }
                }
                else if (beingPlayed)
                {
                    activeCharacter.attackStat += 4;
                    activeCharacter.healthStat += 4;
                    activeCharacter.HealCharacter(4);
                    ActiveCounter(chosenCardNum, true, false);
                }
                else if (resetBuff)
                {
                    activeCharacter.attackStat -= 4;
                    activeCharacter.healthStat -= 4;
                    ActiveCounter(0, false, true);
                }
                break;
            case 5:
                if (activatePassive && !resetBuff)
                {
                    foreach (Character charToBuff in charScripts)
                    {
                        charToBuff.graceStat += 2;
                        charToBuff.healthStat += 2;
                        charToBuff.HealCharacter(2);
                    }
                }
                else if (activatePassive && resetBuff)
                {
                    foreach (Character charToBuff in charScripts)
                    {
                        charToBuff.graceStat -= 2;
                        charToBuff.healthStat -= 2;
                    }
                }
                else if (beingPlayed)
                {
                    activeCharacter.graceStat += 4;
                    activeCharacter.healthStat += 4;
                    activeCharacter.HealCharacter(4);
                    ActiveCounter(chosenCardNum, true, false);
                }
                else if (resetBuff)
                {
                    activeCharacter.graceStat -= 4;
                    activeCharacter.healthStat -= 4;
                    ActiveCounter(0, false, true);
                }
                break;
            case 6:
                if (activatePassive && !resetBuff)
                {
                    foreach (Character charToBuff in charScripts)
                    {
                        charToBuff.attackStat += 2;
                        charToBuff.graceStat += 2;
                    }
                }
                else if (activatePassive && resetBuff)
                {
                    foreach (Character charToBuff in charScripts)
                    {
                        charToBuff.attackStat -= 2;
                        charToBuff.graceStat -= 2;
                    }
                }
                else if (beingPlayed)
                {
                    activeCharacter.attackStat += 4;
                    activeCharacter.graceStat += 4;
                    ActiveCounter(chosenCardNum, true, false);
                }
                else if (resetBuff)
                {
                    activeCharacter.attackStat -= 4;
                    activeCharacter.graceStat -= 4;
                    ActiveCounter(0, false, true);
                }
                break;
            case 7:
                if (activatePassive && !resetBuff)
                {
                    foreach (Character charToBuff in charScripts)
                    {
                        charToBuff.attackStat += 3;
                        charToBuff.graceStat += 3;
                        charToBuff.healthStat += 3;
                        charToBuff.HealCharacter(3);
                    }
                }
                else if (activatePassive && resetBuff)
                {
                    foreach (Character charToBuff in charScripts)
                    {
                        charToBuff.attackStat -= 3;
                        charToBuff.graceStat -= 3;
                        charToBuff.healthStat -= 3;
                    }
                }
                else if (beingPlayed)
                {
                    activeCharacter.attackStat += 7;
                    activeCharacter.healthStat += 7;
                    activeCharacter.HealCharacter(7);
                    activeCharacter.graceStat += 3;
                    ActiveCounter(chosenCardNum, true, false);
                }
                else if (resetBuff)
                {
                    activeCharacter.attackStat -= 7;
                    activeCharacter.healthStat -= 7;
                    activeCharacter.graceStat -= 3;
                    ActiveCounter(0, false, true);
                }
                break;
            case 8:
                if (activatePassive && !resetBuff)
                {
                    foreach (Character charToBuff in charScripts)
                    {
                        charToBuff.attackStat += 4;
                        charToBuff.graceStat += 4;
                        charToBuff.healthStat += 4;
                        charToBuff.HealCharacter(4);
                    }
                }
                else if (activatePassive && resetBuff)
                {
                    foreach (Character charToBuff in charScripts)
                    {
                        charToBuff.attackStat -= 4;
                        charToBuff.graceStat -= 4;
                        charToBuff.healthStat -= 4;
                    }
                }
                else if (beingPlayed)
                {
                    activeCharacter.attackStat += 4;
                    activeCharacter.healthStat += 8;
                    activeCharacter.HealCharacter(8);
                    activeCharacter.graceStat += 8;
                    ActiveCounter(chosenCardNum, true, false);
                }
                else if (resetBuff)
                {
                    activeCharacter.attackStat -= 4;
                    activeCharacter.healthStat -= 8;
                    activeCharacter.graceStat -= 8;
                    ActiveCounter(0, false, true);
                }
                break;
            case 9:
                if (activatePassive && !resetBuff)
                {
                    foreach (Character charToBuff in charScripts)
                    {
                        charToBuff.attackStat += 5;
                        charToBuff.graceStat += 5;
                        charToBuff.healthStat += 5;
                        charToBuff.HealCharacter(5);
                    }
                }
                else if (activatePassive && resetBuff)
                {
                    foreach (Character charToBuff in charScripts)
                    {
                        charToBuff.attackStat -= 5;
                        charToBuff.graceStat -= 5;
                        charToBuff.healthStat -= 5;
                    }
                }
                else if (beingPlayed && !resetBuff)
                {
                    activeCharacter.attackStat += 9;
                    activeCharacter.healthStat += 5;
                    activeCharacter.HealCharacter(5);
                    activeCharacter.graceStat += 9;
                    ActiveCounter(chosenCardNum, true, false);
                }
                else if (resetBuff)
                {
                    activeCharacter.attackStat -= 9;
                    activeCharacter.healthStat -= 5;
                    activeCharacter.graceStat -= 9;
                    ActiveCounter(0, false, true);
                }
                break;
            case 10:
                if (activatePassive && !resetBuff)
                {
                    foreach (Character charToBuff in charScripts)
                    {
                        charToBuff.healthStat += 10;
                        charToBuff.HealCharacter(10);
                    }
                }
                else if (activatePassive && resetBuff)
                {
                    foreach (Character charToBuff in charScripts)
                    {
                        charToBuff.healthStat -= 10;
                    }
                }
                else if (beingPlayed)
                {
                    activeCharacter.attackStat += 10;
                    activeCharacter.graceStat += 10;
                    activeCharacter.healthStat += 10;
                    activeCharacter.HealCharacter(10);
                }
                else if (resetBuff)
                {
                    activeCharacter.attackStat -= 10;
                    activeCharacter.graceStat -= 10;
                    activeCharacter.healthStat -= 10;
                }
                break;
            case 11:
                if (activatePassive && !resetBuff)
                {
                    foreach (Character charToBuff in charScripts)
                    {
                        charToBuff.graceStat += 10;
                    }
                }
                else if (activatePassive && resetBuff)
                {
                    foreach (Character charToBuff in charScripts)
                    {
                        charToBuff.graceStat -= 10;
                    }
                }
                else if (beingPlayed)
                {
                    activeCharacter.attackStat += 10;
                    activeCharacter.graceStat += 10;
                    activeCharacter.healthStat += 10;
                    activeCharacter.HealCharacter(10);
                }
                else if (resetBuff)
                {
                    activeCharacter.attackStat -= 10;
                    activeCharacter.graceStat -= 10;
                    activeCharacter.healthStat -= 10;
                }
                break;
            case 12:
                if (activatePassive && !resetBuff)
                {
                    foreach (Character charToBuff in charScripts)
                    {
                        charToBuff.attackStat += 10;
                    }
                }
                else if (activatePassive && resetBuff)
                {
                    foreach (Character charToBuff in charScripts)
                    {
                        charToBuff.attackStat -= 10;
                    }
                }
                else if (beingPlayed)
                {
                    activeCharacter.attackStat += 10;
                    activeCharacter.graceStat += 10;
                    activeCharacter.healthStat += 10;
                    activeCharacter.HealCharacter(10);
                }
                else if (resetBuff)
                {
                    activeCharacter.attackStat -= 10;
                    activeCharacter.graceStat -= 10;
                    activeCharacter.healthStat -= 10;
                }
                break;
            case 13:
                int highestStat = (Mathf.Max(Mathf.Max(activeCharacter.attackStat, activeCharacter.healthStat), activeCharacter.graceStat));
                if (activatePassive && !resetBuff)
                {
                    activeCharacter.attackStat = highestStat;
                    activeCharacter.graceStat = highestStat;
                    activeCharacter.healthStat = highestStat;
                    activeCharacter.HealCharacter(highestStat);
                }
                else if (activatePassive && resetBuff) 
                {
                    activeCharacter.attackStat = activeCharacter.baseAttack;
                    activeCharacter.graceStat = activeCharacter.baseGrace;
                    activeCharacter.healthStat = activeCharacter.baseHealth;
                }
                break;
        }
    }

    public void PlayCard(SelectedInfo cardData)
    {
        if (firstCardPlay)
        {
            //firstCardPlay = false;
            if (cardData.cardNum > 3)
            {
                ActivateCards(cardData.cardNum, false, true, true);
            }
            ActivateCards(cardData.cardNum, true, false, false);
            playerHandValue -= cardData.cardNum;
            int cardIndex = playerCards.IndexOf(cardData.cardNum);
            playerCards.RemoveAt(cardIndex);
            UIManager uiSystem = GameObject.Find("UIManager").GetComponent<UIManager>();
            uiSystem.cardButtons[cardData.cardType - 1].interactable = false;
        }
    }


}
