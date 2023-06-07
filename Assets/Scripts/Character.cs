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

    void Update()
    {
        DisplayCharacterStats();
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
            TurnManager turnManager = GameObject.Find("TurnManager").GetComponent<TurnManager>();
            CombatManager combatManager = GameObject.Find("CombatManager").GetComponent<CombatManager>();

            //turnManager.chargeAttackActive = true;
            //turnManager.TurnEnd();
        }
        if (characterNum == 2)
        {
            Character char1Stats = GameObject.Find("Char1").GetComponent<Character>();
            char1Stats.currHealth += graceStat;
        }
    }

    public void Move3(Enemy enemyScript)
    {
        if (characterNum == 1)
        {
            enemyScript.currentEnemyHealth -= attackStat / 2 * graceStat / 2;
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
