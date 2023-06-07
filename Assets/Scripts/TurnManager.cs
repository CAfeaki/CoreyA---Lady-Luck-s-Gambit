using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    public int turnNumber;
    public int roundsHad;
    public UIManager statInformation;
    public CombatManager combatManager;

    public List<GameObject> enemies = new List<GameObject>();
    public List<Enemy> enemyScripts = new List<Enemy>();
    public GameObject[] characters;
    public Button[] allActionButtons;

    public bool chargeAttackActive = false;

    public int activePlayers;

    void Start()
    {
        statInformation = GameObject.Find("UIManager").GetComponent<UIManager>();
        combatManager = GameObject.Find("CombatManager").GetComponent<CombatManager>();
        foreach (GameObject enemyObject in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemies.Add(enemyObject);
        }
        characters = GameObject.FindGameObjectsWithTag("Playable");
        GameObject[] buttonObjects = GameObject.FindGameObjectsWithTag("ActionButton");
        //enemyScripts = new Enemy[enemies.Count];
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
        SetCharTurn();
    }

    void Update()
    {
        activePlayers = enemies.Count + characters.Length;
        if (chargeAttackActive)
        {
            ChargeAttack();
        }
    }

    public void SetCharTurn()
    {

        if (turnNumber > activePlayers)
        {
            roundsHad++;
            turnNumber = 1;
            foreach (Button actionButton in allActionButtons)
            {
                actionButton.interactable = true;
            }
        }
        else if (turnNumber == 3)        // disable player buttons once its the enemy's turn
        {
            foreach (Button actionButton in allActionButtons)
            {
                actionButton.interactable = false;
            }
        }
        statInformation.characterNum = turnNumber;
        statInformation.characterMovesetActive = turnNumber;

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
