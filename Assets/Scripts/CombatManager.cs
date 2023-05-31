using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    [Header("General")]
    private GameObject[] enemies;
    public int selectedTargetNum;

    [Header("Fight")]
    private GameObject fightOptions;
    private GameObject selectedTarget;

    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    public void FightAction()
    {
        TargetSelect();
        selectedTarget = enemies[selectedTargetNum];
        Enemy targetScript = selectedTarget.GetComponent<Enemy>();

    }

    public void TargetSelect()
    {
        int currSelectionNum = 0;
        if (Input.GetKeyDown(KeyCode.Space)) // set to space, should be changed later 
        {
            while (currSelectionNum >= enemies.Length)
            {
                GameObject selectionArrow = enemies[currSelectionNum].GetComponent<Enemy>().selectionArrow;
                selectionArrow.SetActive(true);
                currSelectionNum++;
            }
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            selectedTargetNum = currSelectionNum;
        }
    }

    public void HealAction()
    {
        Debug.Log("meow");
    }

    public void PullAction()
    {
        Debug.Log("meow");
    }


}
