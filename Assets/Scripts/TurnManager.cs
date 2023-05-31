using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    public int turnNumber;
    public int roundsHad;
    public UIManager statInformation;

    public GameObject[] enemies;
    public GameObject[] characters;
    public Button[] allActionButtons;
    public Enemy[] enemyScripts;

    public int activePlayers;

    void Start()
    {
        statInformation = GameObject.Find("UIManager").GetComponent<UIManager>();
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        characters = GameObject.FindGameObjectsWithTag("Playable");
        GameObject[] buttonObjects = GameObject.FindGameObjectsWithTag("ActionButton");
        enemyScripts = new Enemy[enemies.Length];
        int i = 0;
        foreach (GameObject buttonObject in buttonObjects)
        {
            allActionButtons[i] = buttonObjects[i].GetComponent<Button>();
            i++;
        }
        i = 0;
        foreach (GameObject enemy in enemies)
        {
            enemyScripts[i] = enemies[i].GetComponent<Enemy>();
            i++;
        }
        activePlayers = enemies.Length + characters.Length;
        SetCharTurn();
    }

    public void SetCharTurn()
    {
        if (turnNumber > activePlayers)
        {
            turnNumber = 1;
            foreach (Button actionButton in allActionButtons)
            {
                actionButton.interactable = true;
            }
        }
        else if (turnNumber >= 3)        // disable player buttons once its the enemy's turn
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
}
