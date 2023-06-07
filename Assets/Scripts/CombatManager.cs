using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    [Header("General")]
    private GameObject[] enemies;
    public int selectedTargetNum;
    public TurnManager turnManager;
    public Character activeCharacter;

    [Header("Fight")]
    private GameObject fightOptions;
    private GameObject selectedTarget;
    public bool targetSelect = false;
    public int targetNumber = 0;
    public int moveButtonNum;

    void Start()
    {
        turnManager = GameObject.Find("TurnManager").GetComponent<TurnManager>();
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    void Update()
    {
        activeCharacter = GameObject.Find("UIManager").GetComponent<UIManager>().characterScripts[turnManager.turnNumber - 1];

        if (targetSelect)
        {
            FightTargetSelect(targetNumber);
            if (targetNumber < enemies.Length - 1 && Input.GetKeyDown(KeyCode.RightArrow))
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

    public void ActivateTargetSelect()
    {
        targetSelect = true;
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
        /*if ()
        {
            activeCharacter.Move1(targetScript);
        }*/
        //targetScript.currentEnemyHealth -= activeCharacter.attackStat;
        targetNumber = 0;
        turnManager.TurnEnd();
    }

    public void HealAction()
    {
        if (activeCharacter.currHealth < activeCharacter.healthStat)
        {
            activeCharacter.currHealth += activeCharacter.graceStat;
        }
        turnManager.TurnEnd();
    }

    public void PullAction()
    {
        Debug.Log("meow");
    }


}
