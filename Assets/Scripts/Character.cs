using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public int characterNum;
    public int attackStat;
    public int graceStat;
    public int healthStat;
    public int currHealth;
    public string[] charMoveset;
    public Slider healthBar;
    private TurnManager turnManager;
    public GameObject deadText;

    void Start()
    {
        turnManager = GameObject.Find("TurnManager").GetComponent<TurnManager>();
    }

    void Update()
    {
        DisplayCharacterStats();

        if (currHealth <= 0)
        {
            currHealth = 0;
            deadText.SetActive(true);
            turnManager.deadChar = characterNum;
        }

        if (deadText.activeSelf)
        {
            if (currHealth > 0)
            {
                deadText.SetActive(false);
                turnManager.deadChar = 0;
            }
        }
    }

    public void Move1(Enemy enemyScript)
    {
        if (characterNum == 1)
        {
            enemyScript.currentEnemyHealth -= attackStat;
        }
        if (characterNum == 2)
        {
            enemyScript.currentEnemyHealth -= attackStat;
        }
    }

    public void Move2(Enemy enemyScript)
    {
        if (characterNum == 1)
        {
            turnManager.roundToReturn = turnManager.roundsHad + 1;
            turnManager.targetToReturn = enemyScript;
            turnManager.chargeAttackActive = true;
        }
        if (characterNum == 2)
        {
            Character char1Stats = GameObject.Find("Char1").GetComponent<Character>();
            if (char1Stats.currHealth < char1Stats.healthStat)
            {
                char1Stats.currHealth += graceStat;
                if (char1Stats.currHealth > char1Stats.healthStat)
                {
                    char1Stats.currHealth = char1Stats.healthStat;
                }
            }
        }
    }

    public void Char1Move2(Enemy enemyScript)
    {
        enemyScript.currentEnemyHealth -= attackStat * 2 + graceStat;
        turnManager.chargeAttackActive = false;
        turnManager.TurnEnd();
    }

    public void Move3(Enemy enemyScript)
    {
        if (characterNum == 1)
        {
            foreach (Enemy targetScript in turnManager.enemyScripts)
            {
                targetScript.currentEnemyHealth -= attackStat / 2;
            }
        }
        if (characterNum == 2)
        {
            enemyScript.currentEnemyHealth -= attackStat;
            if (currHealth < healthStat)
            {
                currHealth += attackStat;
                if (currHealth > healthStat)
                {
                    currHealth = healthStat;
                }
            }
        }
    }

    public void DisplayCharacterStats()
    {
        healthBar.maxValue = healthStat;
        healthBar.value = currHealth;
    }
}
