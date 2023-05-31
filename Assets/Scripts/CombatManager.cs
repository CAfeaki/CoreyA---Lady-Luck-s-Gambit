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

    void Start()
    {
        turnManager = GameObject.Find("TurnManager").GetComponent<TurnManager>();
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        activeCharacter = GameObject.Find("UIManager").GetComponent<UIManager>().characterScripts[turnManager.turnNumber-1];
    }

    void Update()
    {
        
    }

    public void FightTargetSelect()
    {
        GameObject selectionArrow = enemies[0].GetComponent<Enemy>().selectionArrow;
        selectionArrow.SetActive(true);
        int currSelectionNum = 0;

        if (Input.GetKeyDown(KeyCode.Space)) // set to space, should be changed later 
        {
            selectionArrow.SetActive(false);
            if (currSelectionNum == enemies.Length)
            {
                currSelectionNum = 0;
            }
            else if (currSelectionNum < enemies.Length)
            {
                currSelectionNum++;
            }
            selectionArrow = enemies[currSelectionNum].GetComponent<Enemy>().selectionArrow;
            selectionArrow.SetActive(true);
            Debug.Log(currSelectionNum);
            Debug.Log(enemies.Length);
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            selectionArrow.SetActive(false);
            selectedTargetNum = currSelectionNum;
            FightAction();
        }
    }

    public void FightAction()
    {
        selectedTarget = enemies[selectedTargetNum];
        Enemy targetScript = selectedTarget.GetComponent<Enemy>();
        targetScript.currentEnemyHealth -= activeCharacter.attackStat;
        turnManager.TurnEnd();
    }

    public void HealAction()
    {
        activeCharacter.healthStat += activeCharacter.graceStat;
    }

    public void PullAction()
    {
        Debug.Log("meow");
    }


}
