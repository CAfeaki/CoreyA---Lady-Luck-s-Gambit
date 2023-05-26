using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatDisplay : MonoBehaviour
{
    [Header("Characer Stats")]
    public int characterNum;
    public int characterMovesetActive;
    public int attackStat;
    public int graceStat;
    public int healthStat;

    public Text attackStatText;
    public Text graceStatText;
    public Text healthStatText;

    [Header("Character Moveset")]
    public string[] activeMoveset = new string[3];
    public Text[] fightButtonText; 

    private string[] char1Moveset = new string[] {"Pure Radiance", "Death of a Sun", "Fleeting Light"};
    private string[] char2Moveset = new string[] {"Veil of Darkness", "Moonlight's Comfort", "Mercy of an Eclipse"};

    void Update()
    {
        DisplayStats();
    }

    public void DisplayStats()
    {
        switch (characterNum)
        {
            case 1:
                attackStat = 10;
                graceStat = 4;
                healthStat = 15;
                break;
            case 2:
                attackStat = 4;
                graceStat = 10;
                healthStat = 15;
                break;
        }

        attackStatText.text = attackStat.ToString();
        graceStatText.text = graceStat.ToString();
        healthStatText.text = healthStat.ToString();
    }

    public void DisplayMoves()
    {
        switch (characterMovesetActive)
        {
            case 1:
                AssignMovesetActive(char1Moveset);
                break;
            case 2:
                AssignMovesetActive(char2Moveset);
                break;

        }
    }

    public void AssignMovesetActive(string[] activatedMoveset)
    {
        int i = 0;
        foreach (string moveName in activatedMoveset)
        {
            activeMoveset[i] = activatedMoveset[i];
            fightButtonText[i].text = activeMoveset[i];
            i++;
        }
        
    }
}
