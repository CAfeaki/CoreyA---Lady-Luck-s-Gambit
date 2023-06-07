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

        }
    }

    public void DisplayCharacterStats()
    {
        healthBar.maxValue = healthStat;
        healthBar.value = currHealth;
    }
}
