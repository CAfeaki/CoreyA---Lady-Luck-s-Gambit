using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform enemyParent;
    public List<Transform> enemyPositions = new List<Transform>();
    private Transform enemyPos;
    private Transform enemyRot;
    public int enemyNum;

    void Start()
    {
        enemyNum = Random.Range(1, 3);
        for(int i = 0; i <= enemyNum; i++)
        {
            enemyPos = enemyPositions[i];
            enemyRot = enemyPositions[i];
            Instantiate(enemyPrefab, enemyPos.position, enemyRot.rotation, enemyParent);
        }
    }
}
