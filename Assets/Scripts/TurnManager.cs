using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    public int turnNumber;
    public int roundsHad;
    public UIManager uiManager;
    public CombatManager combatManager;
    public DealerSystem dealerSystem;

    public List<GameObject> enemies = new List<GameObject>();
    public List<Enemy> enemyScripts = new List<Enemy>();
    public GameObject[] characters;
    public Button[] allActionButtons;
    public int deadChar;
    public bool pullReset = true;

    public bool chargeAttackActive = false;
    public int roundToReturn;
    public Enemy targetToReturn;

    public int activePlayers;
    public bool char1Dead;
    public bool char2Dead;
    public GameObject restartButton;

    private CardBuffs cardBuffs;


    void Start()
    {
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        combatManager = GameObject.Find("CombatManager").GetComponent<CombatManager>();
        dealerSystem = GameObject.Find("DealerSystem").GetComponent<DealerSystem>();
        cardBuffs = GameObject.Find("DealerSystem").GetComponent<CardBuffs>();
        foreach (GameObject enemyObject in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemies.Add(enemyObject);
        }
        characters = GameObject.FindGameObjectsWithTag("Playable");
        GameObject[] buttonObjects = GameObject.FindGameObjectsWithTag("ActionButton");
        int i = 0;
        foreach (GameObject buttonObject in buttonObjects)
        {
            allActionButtons[i] = buttonObjects[i].GetComponent<Button>();
            i++;
        }
        i = 0;
        foreach (GameObject enemy in enemies)
        {
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            enemyScripts.Add(enemyScript);
        }
        char1Dead = false;
        char2Dead = false;
        SetCharTurn();
    }

    void Update()
    {
        activePlayers = enemies.Count + characters.Length;

        if (enemies.Count == 0)
        {
            chargeAttackActive = false;
            turnNumber = 0;
            roundsHad = 0;
            Text announcementText = GameObject.Find("announceText").GetComponent<Text>();
            announcementText.text = "You win!";
            announcementText.enabled = true;
            restartButton.SetActive(true);

            foreach (Button actionButton in allActionButtons)
            {
                actionButton.interactable = false;
            }

            GameObject openFightOptions = GameObject.Find("fightOptions");
            if (openFightOptions)
            {
                openFightOptions.SetActive(false);
            }
        }
        if (char1Dead && char2Dead)
        {
            chargeAttackActive = false;
            turnNumber = 0;
            roundsHad = 0;
            Text announcementText = GameObject.Find("announceText").GetComponent<Text>();
            announcementText.text = "Game Over!";
            announcementText.enabled = true;
            restartButton.SetActive(true);

            foreach (Button actionButton in allActionButtons)
            {
                actionButton.interactable = false;
            }

            GameObject openFightOptions = GameObject.Find("fightOptions");
            if (openFightOptions)
            {
                openFightOptions.SetActive(false);
            }
        }

        if (chargeAttackActive)
        {
            ChargeAttack();
        }

        if (deadChar != 0)
        {
            if (turnNumber == deadChar)
            {
                TurnEnd();
            }
        }


        if (turnNumber > activePlayers && enemies.Count > 0)
        {
            SetCharTurn();
        }
    }

    public void SetCharTurn()
    {
        dealerSystem.firstCardPlay = true;

        if (turnNumber == 0)
        {
            foreach (Button actionButton in allActionButtons)
            {
                actionButton.interactable = false;
            }
            roundsHad = 0;
        }

        if (dealerSystem.roundToEnd.Count != 0)
        {
            if (dealerSystem.roundToEnd[0] == roundsHad && dealerSystem.turnToEnd[0] == dealerSystem.activeCharacter.characterNum)
            {
                cardBuffs.ActivateCards(dealerSystem.cardToEnd[0], false, true, false, dealerSystem.activeCharacter);
            }
        }

        if (turnNumber == 2)
        {
            Character characterScript = characters[1].GetComponent<Character>();
            characterScript.char2PassDone = false;
        }

        if (turnNumber > activePlayers)
        {
            roundsHad++;
            turnNumber = 1;
            foreach (Button actionButton in allActionButtons)
            {
                actionButton.interactable = true;
            }
        }
        if (chargeAttackActive)
        {
            if (roundsHad == roundToReturn)
            {
                Character characterScript = characters[0].GetComponent<Character>();
                chargeAttackActive = false;
                characterScript.Char1Move2(targetToReturn);

                Text announcementText = GameObject.Find("announceText").GetComponent<Text>();
                announcementText.text = "Charge attack unleashed!";
                announcementText.enabled = true;
                StartCoroutine(combatManager.DelayHide(announcementText));
            }
        }

        else if (turnNumber == 3)        // disable player buttons once its the enemy's turn
        {
            foreach (Button actionButton in allActionButtons)
            {
                actionButton.interactable = false;
            }

            Text announcementText = GameObject.Find("announceText").GetComponent<Text>();
            announcementText.text = "Enemy turn finished.";
            announcementText.enabled = true;
            StartCoroutine(combatManager.DelayHide(announcementText));
        }
        uiManager.characterNum = turnNumber;
        uiManager.characterMovesetActive = turnNumber;

    }

    public void TurnEnd()
    {
        GameObject openFightOptions = GameObject.Find("fightOptions");
        GameObject openDescWindow = GameObject.Find("descBox");
        if (openFightOptions)
        {
            openFightOptions.SetActive(false);
        }
        if (openDescWindow)
        {
            openDescWindow.SetActive(false);
        }
        turnNumber++;
        combatManager.targetSelect = false;
        pullReset = true;
        SetCharTurn();
    }

    public void ChargeAttack()
    {
        if (turnNumber == 1)
        {
            combatManager.moveButtonNum = 2;
            combatManager.FightAction();
        }
    }
}
