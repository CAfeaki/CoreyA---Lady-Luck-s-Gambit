using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("General")]
    private DealerSystem dealerSystem;
    public int timesTargeted;

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
    private Character enemyTarget;

    [Header("Enemy Pulling")]
    public List<int> enemyCards = new List<int>();

    [Header("Spawning")]
    public GameObject selectionArrow;

    void Start()
    {
        char1 = GameObject.Find("Char1").GetComponent<Character>();
        char2 = GameObject.Find("Char2").GetComponent<Character>();

        characterScripts.Add(char1);
        characterScripts.Add(char2);

        dealerSystem = GameObject.Find("DealerSystem").GetComponent<DealerSystem>();
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
        enemyTarget = characterScripts[targetNum];
        enemyTarget.currHealth -= attackStat;

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
