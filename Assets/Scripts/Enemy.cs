using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("General")]
    public int timesTargeted;
    public List<AudioClip> enemyVO = new List<AudioClip>();

    [Header("Turn Management")]
    public int enemyNumber;
    private TurnManager turnManager;

    [Header("Enemy Stats")]
    public int attackStat;
    public int graceStat;
    public int healthStat;
    public int currentEnemyHealth;
    public Slider healthBar;

    [Header("Enemy Attacking")]
    private List<Character> characterScripts = new List<Character>();
    private Character char1;
    private Character char2;
    public Character enemyTarget;

    [Header("Spawning")]
    public GameObject selectionArrow;

    void Start()
    {
        char1 = GameObject.Find("Char1").GetComponent<Character>();
        char2 = GameObject.Find("Char2").GetComponent<Character>();

        characterScripts.Add(char1);
        characterScripts.Add(char2);

        turnManager = GameObject.Find("TurnManager").GetComponent<TurnManager>();

        EnemyStats();
        DisplayEnemyStats();
    }

    public void Update()
    {
        enemyNumber = turnManager.enemies.IndexOf(gameObject) + 1;
        DisplayEnemyStats();
        if (turnManager.turnNumber == enemyNumber + 2)
        {
            EnemyMove();
        }

        if (currentEnemyHealth <= 0)
        {
            Destroy(gameObject);
            turnManager.enemies.Remove(gameObject);
            turnManager.enemyScripts.Remove(this);
            CombatManager combatManager = GameObject.Find("CombatManager").GetComponent<CombatManager>();
            combatManager.enemies.Remove(gameObject);
        }
    }

    public void EnemyMove()
    {
        int targetNum = Random.Range(0, 2);
        if (targetNum == 0 && turnManager.char1Dead)
        {
            targetNum = 1;
        }
        if (targetNum == 1 && turnManager.char2Dead)
        {
            targetNum = 0;
        }
        targetNum = 1; // REMOVE THIS
        enemyTarget = characterScripts[targetNum];
        enemyTarget.currHealth -= attackStat;
        UIManager uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        //uiManager.SoundDamage(enemyTarget);
        turnManager.DelaySound(enemyNumber - 2);
        turnManager.TurnEnd();
    }

    public void EnemyStats()
    {
        attackStat = Random.Range(3, 10);
        graceStat = Random.Range(2, 6);
        healthStat = Random.Range(50, 90);
        currentEnemyHealth = healthStat;
    }

    public void DisplayEnemyStats()
    {
        healthBar.maxValue = healthStat;
        healthBar.value = currentEnemyHealth;
    }

}
