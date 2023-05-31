using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Character[] characterScripts;
    public Character activeCharacter;

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

    [Header("Button Manager")]
    //public GameObject fightButton;
    public GameObject fightOptions;

    void Start()
    {
        fightOptions = GameObject.Find("fightOptions");
        fightOptions.SetActive(false);
    }

    void Update()
    {
        DisplayStats();
    }

    public void DisplayStats()
    {
        if (characterNum <= 2)
        {
            activeCharacter = characterScripts[characterNum - 1];

            attackStat = activeCharacter.attackStat;
            graceStat = activeCharacter.graceStat;
            healthStat = activeCharacter.healthStat;

            attackStatText.text = attackStat.ToString();
            graceStatText.text = graceStat.ToString();
            healthStatText.text = healthStat.ToString();
        }
    }

    public void DisplayMoves()
    {
        switch (characterMovesetActive)
        {
            case 1:
                AssignMovesetActive();
                break;
            case 2:
                AssignMovesetActive();
                break;

        }
    }

    public void AssignMovesetActive()
    {
        
        int i = 0;
        foreach (string moveName in activeCharacter.charMoveset)
        {
            activeMoveset[i] = activeCharacter.charMoveset[i];
            fightButtonText[i].text = activeMoveset[i];
            i++;
        }
        
    }

    public void ShowFightOptions()
    {
        if (fightOptions.activeSelf)
        {
            fightOptions.SetActive(false);
        }
        else
        {
            fightOptions.SetActive(true);
        }
    }
}
