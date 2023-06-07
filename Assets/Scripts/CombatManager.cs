using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    [Header("General")]
    public List<GameObject> enemies = new List<GameObject>();
    public int selectedTargetNum;
    public TurnManager turnManager;
    public Character activeCharacter;

    [Header("Fight")]
    private GameObject fightOptions;
    private GameObject selectedTarget;
    public bool targetSelect = false;
    public int targetNumber = 0;
    public int moveButtonNum;
    //public bool chargeAttackActive = false;

    void Start()
    {
        turnManager = GameObject.Find("TurnManager").GetComponent<TurnManager>();
        foreach (GameObject enemyObject in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemies.Add(enemyObject);
        }
    }

    void Update()
    {
        if (turnManager.turnNumber <= 2)
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
    }

    public void ActivateTargetSelect(int moveNum)
    {

        moveButtonNum = moveNum;
        if (moveNum == 2 && activeCharacter.characterNum == 2)
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
        GameObject selectionArrow;
        foreach (GameObject activeEnemy in enemies)
        {
            GameObject activeArrow = activeEnemy.GetComponent<Enemy>().selectionArrow;
            activeArrow.SetActive(false);
        }
        selectionArrow = enemies[currSelectionNum].GetComponent<Enemy>().selectionArrow; //change the arrow
        selectionArrow.SetActive(true);

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
        if (moveButtonNum == 1) // do the action associated with the button pressed
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
        if (activeCharacter.currHealth < activeCharacter.healthStat)
        {
            activeCharacter.currHealth += activeCharacter.graceStat;
            if (activeCharacter.currHealth > activeCharacter.healthStat)
            {
                activeCharacter.currHealth = activeCharacter.healthStat;
            }
        }
        turnManager.TurnEnd();
    }

    public void PullAction()
    {
        Debug.Log("meow");
    }


}
