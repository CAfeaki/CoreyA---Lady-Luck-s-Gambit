using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CombatManager : MonoBehaviour
{
    [Header("General")]
    public List<GameObject> enemies = new List<GameObject>();
    public int selectedTargetNum;
    public List<GameObject> startCards = new List<GameObject>();
    private TurnManager turnManager;
    private Character activeCharacter;
    private DealerSystem dealerSystem;
    private UIManager uiManager;
    private ButtonHover buttonHover;
    public int cardNum;

    [Header("Fight")]
    private GameObject fightOptions;
    private GameObject selectedTarget;
    public bool targetSelect = false;
    public int targetNumber = 0;
    public int moveButtonNum;
    public GameObject jackpotButton;

    void Start()
    {
        dealerSystem = GameObject.Find("DealerSystem").GetComponent<DealerSystem>();
        turnManager = GameObject.Find("TurnManager").GetComponent<TurnManager>();
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        buttonHover = this.GetComponent<ButtonHover>();
        foreach (GameObject enemyObject in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemies.Add(enemyObject);
        }
        foreach (GameObject initialCard in GameObject.FindGameObjectsWithTag("Initial Card"))
        {
            startCards.Add(initialCard);
        }
        InitialCard();
    }

    void InitialCard()
    {
        foreach (GameObject initialCard in startCards)
        {
            cardNum = dealerSystem.DealCards();
            SelectedInfo cardInfo = initialCard.GetComponent<SelectedInfo>();
            cardInfo.cardNum = cardNum;
            if (cardInfo.cardNum > 10)
            {
                switch (cardInfo.cardNum)
                {
                    case 11:
                        cardInfo.cardName.text = "Jack";
                        break;
                    case 12:
                        cardInfo.cardName.text = "Queen";
                        break;
                    case 13:
                        cardInfo.cardName.text = "King";
                        break;
                }
            }
            else
            {
                cardInfo.cardName.text = cardNum.ToString();
            }
            cardInfo.passiveText.text = buttonHover.passiveTexts[cardNum - 1];
            cardInfo.playText.text = buttonHover.playTexts[cardNum - 1];
        }
        dealerSystem.chosenCard = 0;
    }

    public void InitialCardChosen(int cardNumIndex)
    {
        SelectedInfo cardScript = startCards[cardNumIndex - 1].GetComponent<SelectedInfo>();
        cardScript.ActivateCard(cardScript.cardNum);
        dealerSystem.FirstDeal(cardScript);
        GameObject cardScreen = GameObject.Find("firstPickScreen");
        cardScreen.SetActive(false);
    }

    void Update()
    {
        if (turnManager.turnNumber <= 2 && turnManager.turnNumber != 0)
        {
            activeCharacter = GameObject.Find("UIManager").GetComponent<UIManager>().characterScripts[turnManager.turnNumber - 1];
        }

        if (targetSelect)
        {
            FightTargetSelect(targetNumber);
            if (targetNumber < enemies.Count - 1 && Input.GetKeyDown(KeyCode.RightArrow))
            { 
                targetNumber++;
                FightTargetSelect(targetNumber);
            }
            if (targetNumber != 0 && Input.GetKeyDown(KeyCode.LeftArrow))
            {
                targetNumber--;
                FightTargetSelect(targetNumber);
            }
        }
        else if (!targetSelect)
        {
            FightTargetSelect(targetNumber);
        }

    }

    public void ActivateTargetSelect(int moveNum)
    {
        moveButtonNum = moveNum;
        if (moveNum == 2 && activeCharacter.characterNum == 2)
        {
            FightAction();
        }
        else if (moveNum == 3 && activeCharacter.characterNum == 1)
        {
            FightAction();
        }
        else
        {
            targetSelect = true;
        }
    }

    public void FightTargetSelect(int currSelectionNum)
    {
        if (enemies.Count == 0)
        {
            targetSelect = false;
            return;
        }
        GameObject selectionArrow;
        foreach (GameObject activeEnemy in enemies)
        {
            GameObject activeArrow = activeEnemy.GetComponent<Enemy>().selectionArrow;
            activeArrow.SetActive(false);
        }
        selectionArrow = enemies[currSelectionNum].GetComponent<Enemy>().selectionArrow; 
        if (targetSelect)
        {
            selectionArrow.SetActive(true);
        }
        else 
        {
            selectionArrow.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            targetSelect = false;
            selectionArrow.SetActive(false);
            selectedTargetNum = currSelectionNum;
            FightAction();
        }
    }

    public void FightAction()
    {
        selectedTarget = enemies[selectedTargetNum];
        Enemy targetScript = selectedTarget.GetComponent<Enemy>();
        if (moveButtonNum == 1) 
        {
            activeCharacter.Move1(targetScript);
        }
        else if (moveButtonNum == 2)
        {
            activeCharacter.Move2(targetScript);
        }
        else if (moveButtonNum == 3)
        {
            activeCharacter.Move3(targetScript);
        }
        targetNumber = 0;
        turnManager.TurnEnd();
    }

    public void HealAction()
    {
        if (activeCharacter.characterNum == 1)
        {
            activeCharacter.attackStat -= activeCharacter.attackCombo;
            activeCharacter.attackCombo = 0;
        }
        activeCharacter.HealCharacter(activeCharacter.graceStat);
        turnManager.TurnEnd();
    }

    public void PullAction()
    {
        if (activeCharacter.characterNum == 1)
        {
            activeCharacter.attackStat -= activeCharacter.attackCombo;
            activeCharacter.attackCombo = 0;
        }
        if (dealerSystem.playerCards.Count < 3 && turnManager.pullReset)
        {
            dealerSystem.CardPull();
            //turnManager.pullReset = false;
        }
    }

    public void TheJackpot()
    {
        foreach (GameObject enemyObject in enemies)
        {
            Enemy enemyScript = enemyObject.GetComponent<Enemy>();
            Character char1Script = GameObject.Find("Char1").GetComponent<Character>();
            Character char2Script = GameObject.Find("Char2").GetComponent<Character>();
            enemyScript.currentEnemyHealth -= char1Script.attackStat + char2Script.graceStat * 2;
        }
        jackpotButton.SetActive(false);
        dealerSystem.playerHandValue = 0;
        dealerSystem.playerCards.Clear();
        foreach (Button cardButton in uiManager.cardButtons)
        {
            cardButton.interactable = false;
        }

        GameObject openFightOptions = GameObject.Find("fightOptions");
        if (openFightOptions)
        {
            openFightOptions.SetActive(false);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene("CombatScene");
    }


}
