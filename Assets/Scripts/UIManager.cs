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
    public int currHealth;

    public Text attackStatNum;
    public Text graceStatNum;
    public Text healthStatNum;
    public Text handStatNum;

    [Header("Character Moveset")]
    public string[] activeMoveset = new string[3];
    public Text[] fightButtonText; 

    [Header("Button Manager")]
    public GameObject fightOptions;
    public List<Button> cardButtons = new List<Button>();
    public List<Button> enemyCardButtons = new List<Button>();

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
            currHealth = activeCharacter.currHealth;

            attackStatNum.text = attackStat.ToString();
            graceStatNum.text = graceStat.ToString();
            healthStatNum.text = currHealth.ToString() + "/" + healthStat.ToString();
            DealerSystem dealerScript = GameObject.Find("DealerSystem").GetComponent<DealerSystem>();
            handStatNum.text = dealerScript.playerHandValue.ToString();
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
