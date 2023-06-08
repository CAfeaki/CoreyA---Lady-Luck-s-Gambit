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

    public void CardPull(bool isPlayer, Enemy enemyScript)
    {
        int randomNum = Random.Range(0, cardsInPlay.Count-1);
        chosenCard = cardsInPlay[randomNum+1];
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
            ActivateCards(chosenCard);
        }
        else
        {
            enemyScript.enemyCards.Add(chosenCard);

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

    public void ActivateCards(int chosenCardNum)
    {
        switch (chosenCardNum) // imagine having to make content lmaooo
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
