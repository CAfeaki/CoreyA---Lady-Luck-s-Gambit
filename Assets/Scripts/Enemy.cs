using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Turn Management")]
    public int enemyNumber;
    public TurnManager turnManager;

    [Header("Enemy Stats")]
    public int attackStat;
    public int graceStat;
    public int healthStat;
    public int currentEnemyHealth;
    public Slider healthBar;

    [Header("Spawning")]
    public GameObject enemyPrefab;
    public GameObject selectionArrow;
    //public Array[] enemyTransformsIndex = new Array[] { 1, 2, 3 };
    //public Vector3[] enemyTransformsPositions;

    void Start()
    {
        //Instantiate(enemyPrefab,)
        EnemyStats();
        DisplayEnemyStats();
    }

    public void Update()
    {
        if (turnManager.turnNumber == enemyNumber + 2)
        {
            EnemyMove();
        }
    }

    public void EnemyMove()
    {
        Debug.Log("Enemy move taken.");
        turnManager.turnNumber++;
        turnManager.SetCharTurn();
    }

    public void EnemyStats()
    {
        attackStat = Random.Range(5, 10);
        graceStat = Random.Range(2, 6);
        healthStat = Random.Range(10, 20);
        currentEnemyHealth = healthStat;
    }

    public void DisplayEnemyStats()
    {
        healthBar.maxValue = healthStat;
        healthBar.value = currentEnemyHealth;
    }

}
