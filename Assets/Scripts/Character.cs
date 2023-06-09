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
    public bool char2PassDone = false;
    public int attackCombo = 0;

    public int baseAttack;
    public int baseGrace;
    public int baseHealth;

    void Start()
    {
        turnManager = GameObject.Find("TurnManager").GetComponent<TurnManager>();
        baseAttack = attackStat;
        baseGrace = graceStat;
        baseHealth = healthStat;
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

        if (turnManager.turnNumber == 2 && !char2PassDone) // probably a prpblem rn
        {
            char2PassDone = true;
            CharPassive(2, null);
        }

        if (deadText.activeSelf)
        {
            if (currHealth > 0)
            {
                deadText.SetActive(false);
                turnManager.deadChar = 0;
            }
        }

        if (currHealth > healthStat)
        {
            currHealth = healthStat;
        }
    }

    void CharPassive(int activePass, Enemy enemyScript)
    {
        if (activePass == 1)
        {
            attackStat += 1;
        }
        if (activePass == 2)
        {
            if (currHealth < healthStat)
            {
                currHealth += 1;
                if (currHealth >= healthStat)
                {
                    currHealth = healthStat;
                }
            }
        }
    }

    public void Move1(Enemy enemyScript)
    {
        if (characterNum == 1)
        {
            attackCombo++;
            enemyScript.currentEnemyHealth -= attackStat;
            CharPassive(1, enemyScript);
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
        CharPassive(1, enemyScript);
        enemyScript.currentEnemyHealth -= attackStat * 2 + graceStat;
        attackCombo++;
        turnManager.chargeAttackActive = false;
        turnManager.TurnEnd();
    }

    public void Move3(Enemy enemyScript)
    {
        if (characterNum == 1)
        {
            CharPassive(1, enemyScript);
            foreach (Enemy targetScript in turnManager.enemyScripts)
            {
                targetScript.currentEnemyHealth -= attackStat / 2;
            }
            attackCombo++;
        }
        if (characterNum == 2)
        {
            enemyScript.currentEnemyHealth -= attackStat;
            HealCharacter(attackStat);
        }
    }

    public void DisplayCharacterStats()
    {
        healthBar.maxValue = healthStat;
        healthBar.value = currHealth;
    }

    public void HealCharacter(int amountToHeal)
    {
        if (currHealth < healthStat)
        {
            currHealth += amountToHeal;
            if (currHealth > healthStat)
            {
                currHealth = healthStat;
            }
        }
    }
}
