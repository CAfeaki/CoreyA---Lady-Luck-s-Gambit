using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int enemyNumber;
    public TurnManager turnManager;

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

}
