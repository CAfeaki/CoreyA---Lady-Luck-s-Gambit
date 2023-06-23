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
        InitialCard();
    }

    void InitialCard() // first screen the player sees, letting them choose their first card
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

    public void InitialCardChosen(int cardNumIndex) // add first card to hand
    {
        SelectedInfo cardScript = startCards[cardNumIndex - 1].GetComponent<SelectedInfo>();
        cardScript.ActivateCard(cardScript.cardNum);
        dealerSystem.FirstDeal(cardScript);
        GameObject cardScreen = GameObject.Find("firstPickScreen");
        cardScreen.SetActive(false);
    }

    void Update()
    {
        if (turnManager.turnNumber <= 2 && turnManager.turnNumber != 0) // get the active character's script
        {
            activeCharacter = GameObject.Find("UIManager").GetComponent<UIManager>().characterScripts[turnManager.turnNumber - 1];
        }

        if (targetSelect) // activating the target select arrow
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

        if (jackpotButton.activeSelf && dealerSystem.playerHandValue != 21)
        {
            jackpotButton.SetActive(false);
        }

    }

    public void ActivateTargetSelect(int moveNum) // called after a move is picked to trigger target select
    {
        moveButtonNum = moveNum;
        if (moveNum == 2 && activeCharacter.characterNum == 2) // moves that don't require target select, move right to the action
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

    public void FightTargetSelect(int currSelectionNum) // activating the arrow and selecting the target
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

        if (Input.GetKeyDown(KeyCode.Return)) // target selected
        {
            targetSelect = false;
            selectionArrow.SetActive(false);
            selectedTargetNum = currSelectionNum;
            FightAction();
        }
    }

    public void FightAction() // activates the moves/damage
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

    public void HealAction() // restores health to player
    {
        if (activeCharacter.characterNum == 1)
        {
            activeCharacter.attackStat -= activeCharacter.attackCombo;
            activeCharacter.attackCombo = 0;
        }
        activeCharacter.HealCharacter(activeCharacter.graceStat);
        turnManager.TurnEnd();
    }

    public void PullAction() // activates pulling from the deck
    {
        if (activeCharacter.characterNum == 1)
        {
            activeCharacter.attackStat -= activeCharacter.attackCombo;
            activeCharacter.attackCombo = 0;
        }
        if (dealerSystem.playerCards.Count < 3 && turnManager.pullReset)
        {
            dealerSystem.CardPull();
            turnManager.pullReset = false;
        }
    }

    public void TheJackpot() // all out finisher once the player hits 21
    {
        foreach (GameObject enemyObject in enemies) // attacking all enemies
        {
            Enemy enemyScript = enemyObject.GetComponent<Enemy>();
            Character char1Script = GameObject.Find("Char1").GetComponent<Character>();
            Character char2Script = GameObject.Find("Char2").GetComponent<Character>();
            enemyScript.currentEnemyHealth -= char1Script.attackStat + char2Script.graceStat * 2;
        }
        jackpotButton.SetActive(false);
        dealerSystem.playerHandValue = 0;
        foreach (Character character in uiManager.characterScripts)
        {
            character.attackStat = character.baseAttack;
            character.graceStat = character.baseGrace;
            character.healthStat = character.baseHealth;
        }
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

    public void DisplayBust() // announcement text for busting
    {
        Text announcementText = GameObject.Find("announceText").GetComponent<Text>();
        announcementText.text = "Bust!";
        announcementText.enabled = true;
        StartCoroutine(DelayHide(announcementText));
    }

    public IEnumerator DelayHide(Text textToHide) // delay timer
    {
        yield return new WaitForSeconds(3);
        textToHide.enabled = false;
    }

    public void Restart() // reloads the scene after a game is decided
    {
        if (turnManager.char1Dead && turnManager.char2Dead) // resets health if there was a TPK
        {
            Character char1Script = GameObject.Find("Char1").GetComponent<Character>();
            Character char2Script = GameObject.Find("Char2").GetComponent<Character>();
            char1Script.currHealth = char1Script.baseHealth;
            char2Script.currHealth = char2Script.baseHealth;
        }
        SceneManager.LoadScene("CombatScene");
    }
}
