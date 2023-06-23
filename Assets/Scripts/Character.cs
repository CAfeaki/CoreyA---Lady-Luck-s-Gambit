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

    private GameManager gameManager;


    void Start()
    {
        turnManager = GameObject.Find("TurnManager").GetComponent<TurnManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (characterNum == 1)
        {
            baseAttack = gameManager.char1BaseAttack;
            baseGrace = gameManager.char1BaseGrace;
            baseHealth = gameManager.char1BaseHealth;
            currHealth = gameManager.char1CurrHealth;
        }
        else if (characterNum == 2)
        {
            baseAttack = gameManager.char2BaseAttack;
            baseGrace = gameManager.char2BaseGrace;
            baseHealth = gameManager.char2BaseHealth;
            currHealth = gameManager.char2CurrHealth;
        }
        attackStat = baseAttack;
        graceStat = baseGrace;
        healthStat = baseHealth;
    }

    void UpdateGameManager()
    {
        if (characterNum == 1)
        {
            gameManager.char1Attack = attackStat;
            gameManager.char1Grace = graceStat;
            gameManager.char1Health = healthStat;
            gameManager.char1CurrHealth = currHealth;
        }
        else if (characterNum == 2)
        {
            gameManager.char2Attack = attackStat;
            gameManager.char2Grace = graceStat;
            gameManager.char2Health = healthStat;
            gameManager.char2CurrHealth = currHealth;
        }
    }

    void Update()
    {
        DisplayCharacterStats();
        UpdateGameManager();

        if (currHealth <= 0)
        {
            currHealth = 0;
            deadText.SetActive(true);
            turnManager.deadChar = characterNum;
            if (characterNum == 1)
            {
                turnManager.char1Dead = true;
            }
            if (characterNum == 2)
            {
                turnManager.char2Dead = true;
            }
        }

        if (currHealth > 0 && turnManager.char1Dead && characterNum == 1)
        {
            turnManager.char1Dead = false;
        }
        if (currHealth > 0 && turnManager.char2Dead && characterNum == 2)
        {
            turnManager.char2Dead = false;
        }

        if (turnManager.turnNumber == 2 && !char2PassDone) 
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
            if (!turnManager.char1Dead)
            {
                HealCharacter(1);
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
            enemyScript.currentEnemyHealth -= attackStat / 2;
            HealCharacter(attackStat / 2);
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
