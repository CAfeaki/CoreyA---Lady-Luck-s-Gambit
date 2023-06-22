using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBuffs : MonoBehaviour
{
    private DealerSystem dealerSystem;

    void Start()
    {
        dealerSystem = GameObject.Find("DealerSystem").GetComponent<DealerSystem>();
    }

    public void ActivateCards(int chosenCardNum, bool beingPlayed, bool resetBuff, bool activatePassive, Character activeCharacter)
    {
        if (activatePassive)
        {

            Debug.Log(chosenCardNum);
        }
        switch (chosenCardNum)
        {
            case 1:
                if (beingPlayed)
                {
                    activeCharacter.attackStat += 3;
                    activeCharacter.graceStat += 3;
                    activeCharacter.healthStat += 3;
                    activeCharacter.HealCharacter(3);
                    dealerSystem.ActiveCounter(chosenCardNum, true, false);
                }
                else if (resetBuff)
                {
                    activeCharacter.attackStat -= 3;
                    activeCharacter.graceStat -= 3;
                    activeCharacter.healthStat -= 3;
                    dealerSystem.ActiveCounter(0, false, true);
                }

                break;
            case 2:
                if (beingPlayed)
                {
                    activeCharacter.attackStat += 4;
                    activeCharacter.graceStat += 4;
                    activeCharacter.healthStat += 4;
                    activeCharacter.HealCharacter(4);
                    dealerSystem.ActiveCounter(chosenCardNum, true, false);
                }
                else if (resetBuff)
                {
                    activeCharacter.attackStat -= 4;
                    activeCharacter.graceStat -= 4;
                    activeCharacter.healthStat -= 4;
                    dealerSystem.ActiveCounter(0, false, true);
                }
                break;
            case 3:
                if (beingPlayed)
                {
                    activeCharacter.attackStat += 5;
                    activeCharacter.graceStat += 5;
                    activeCharacter.healthStat += 5;
                    activeCharacter.HealCharacter(5);
                    dealerSystem.ActiveCounter(chosenCardNum, true, false);
                }
                else if (resetBuff)
                {
                    activeCharacter.attackStat -= 5;
                    activeCharacter.graceStat -= 5;
                    activeCharacter.healthStat -= 5;
                    dealerSystem.ActiveCounter(0, false, true);
                }
                break;
            case 4:
                if (activatePassive && !resetBuff)
                {
                    foreach (Character charToBuff in dealerSystem.charScripts)
                    {
                        charToBuff.attackStat += 2;
                        charToBuff.healthStat += 2;
                        charToBuff.HealCharacter(2);
                    }
                }
                else if (activatePassive && resetBuff)
                {
                    foreach (Character charToBuff in dealerSystem.charScripts)
                    {
                        charToBuff.attackStat -= 2;
                        charToBuff.healthStat -= 2;
                    }
                }
                else if (beingPlayed)
                {
                    activeCharacter.attackStat += 4;
                    activeCharacter.healthStat += 4;
                    activeCharacter.HealCharacter(4);
                    dealerSystem.ActiveCounter(chosenCardNum, true, false);
                }
                else if (resetBuff)
                {
                    activeCharacter.attackStat -= 4;
                    activeCharacter.healthStat -= 4;
                    dealerSystem.ActiveCounter(0, false, true);
                }
                break;
            case 5:
                if (activatePassive && !resetBuff)
                {
                    foreach (Character charToBuff in dealerSystem.charScripts)
                    {
                        charToBuff.graceStat += 2;
                        charToBuff.healthStat += 2;
                        charToBuff.HealCharacter(2);
                    }
                }
                else if (activatePassive && resetBuff)
                {
                    foreach (Character charToBuff in dealerSystem.charScripts)
                    {
                        charToBuff.graceStat -= 2;
                        charToBuff.healthStat -= 2;
                    }
                }
                else if (beingPlayed)
                {
                    activeCharacter.graceStat += 4;
                    activeCharacter.healthStat += 4;
                    activeCharacter.HealCharacter(4);
                    dealerSystem.ActiveCounter(chosenCardNum, true, false);
                }
                else if (resetBuff)
                {
                    activeCharacter.graceStat -= 4;
                    activeCharacter.healthStat -= 4;
                    dealerSystem.ActiveCounter(0, false, true);
                }
                break;
            case 6:
                if (activatePassive && !resetBuff)
                {
                    foreach (Character charToBuff in dealerSystem.charScripts)
                    {
                        charToBuff.attackStat += 2;
                        charToBuff.graceStat += 2;
                    }
                }
                else if (activatePassive && resetBuff)
                {
                    foreach (Character charToBuff in dealerSystem.charScripts)
                    {
                        charToBuff.attackStat -= 2;
                        charToBuff.graceStat -= 2;
                    }
                }
                else if (beingPlayed)
                {
                    activeCharacter.attackStat += 4;
                    activeCharacter.graceStat += 4;
                    dealerSystem.ActiveCounter(chosenCardNum, true, false);
                }
                else if (resetBuff)
                {
                    activeCharacter.attackStat -= 4;
                    activeCharacter.graceStat -= 4;
                    dealerSystem.ActiveCounter(0, false, true);
                }
                break;
            case 7:
                if (activatePassive && !resetBuff)
                {
                    foreach (Character charToBuff in dealerSystem.charScripts)
                    {
                        charToBuff.attackStat += 3;
                        charToBuff.graceStat += 3;
                        charToBuff.healthStat += 3;
                        charToBuff.HealCharacter(3);
                    }
                }
                else if (activatePassive && resetBuff)
                {
                    foreach (Character charToBuff in dealerSystem.charScripts)
                    {
                        charToBuff.attackStat -= 3;
                        charToBuff.graceStat -= 3;
                        charToBuff.healthStat -= 3;
                    }
                }
                else if (beingPlayed)
                {
                    activeCharacter.attackStat += 7;
                    activeCharacter.healthStat += 7;
                    activeCharacter.HealCharacter(7);
                    activeCharacter.graceStat += 3;
                    dealerSystem.ActiveCounter(chosenCardNum, true, false);
                }
                else if (resetBuff)
                {
                    activeCharacter.attackStat -= 7;
                    activeCharacter.healthStat -= 7;
                    activeCharacter.graceStat -= 3;
                    dealerSystem.ActiveCounter(0, false, true);
                }
                break;
            case 8:
                if (activatePassive && !resetBuff)
                {
                    foreach (Character charToBuff in dealerSystem.charScripts)
                    {
                        charToBuff.attackStat += 4;
                        charToBuff.graceStat += 4;
                        charToBuff.healthStat += 4;
                        charToBuff.HealCharacter(4);
                    }
                }
                else if (activatePassive && resetBuff)
                {
                    foreach (Character charToBuff in dealerSystem.charScripts)
                    {
                        charToBuff.attackStat -= 4;
                        charToBuff.graceStat -= 4;
                        charToBuff.healthStat -= 4;
                    }
                }
                else if (beingPlayed)
                {
                    activeCharacter.attackStat += 4;
                    activeCharacter.healthStat += 8;
                    activeCharacter.HealCharacter(8);
                    activeCharacter.graceStat += 8;
                    dealerSystem.ActiveCounter(chosenCardNum, true, false);
                }
                else if (resetBuff)
                {
                    activeCharacter.attackStat -= 4;
                    activeCharacter.healthStat -= 8;
                    activeCharacter.graceStat -= 8;
                    dealerSystem.ActiveCounter(0, false, true);
                }
                break;
            case 9:
                if (activatePassive && !resetBuff)
                {
                    foreach (Character charToBuff in dealerSystem.charScripts)
                    {
                        charToBuff.attackStat += 5;
                        charToBuff.graceStat += 5;
                        charToBuff.healthStat += 5;
                        charToBuff.HealCharacter(5);
                    }
                }
                else if (activatePassive && resetBuff)
                {
                    foreach (Character charToBuff in dealerSystem.charScripts)
                    {
                        charToBuff.attackStat -= 5;
                        charToBuff.graceStat -= 5;
                        charToBuff.healthStat -= 5;
                    }
                }
                else if (beingPlayed && !resetBuff)
                {
                    activeCharacter.attackStat += 9;
                    activeCharacter.healthStat += 5;
                    activeCharacter.HealCharacter(5);
                    activeCharacter.graceStat += 9;
                    dealerSystem.ActiveCounter(chosenCardNum, true, false);
                }
                else if (resetBuff)
                {
                    activeCharacter.attackStat -= 9;
                    activeCharacter.healthStat -= 5;
                    activeCharacter.graceStat -= 9;
                    dealerSystem.ActiveCounter(0, false, true);
                }
                break;
            case 10:
                if (activatePassive && !resetBuff)
                {
                    foreach (Character charToBuff in dealerSystem.charScripts)
                    {
                        charToBuff.healthStat += 10;
                        charToBuff.HealCharacter(10);
                    }
                }
                else if (activatePassive && resetBuff)
                {
                    foreach (Character charToBuff in dealerSystem.charScripts)
                    {
                        charToBuff.healthStat -= 10;
                    }
                }
                else if (beingPlayed)
                {
                    activeCharacter.attackStat += 10;
                    activeCharacter.graceStat += 10;
                    activeCharacter.healthStat += 10;
                    activeCharacter.HealCharacter(10);
                }
                else if (resetBuff)
                {
                    activeCharacter.attackStat -= 10;
                    activeCharacter.graceStat -= 10;
                    activeCharacter.healthStat -= 10;
                }
                break;
            case 11:
                if (activatePassive && !resetBuff)
                {
                    foreach (Character charToBuff in dealerSystem.charScripts)
                    {
                        charToBuff.graceStat += 10;
                    }
                }
                else if (activatePassive && resetBuff)
                {
                    foreach (Character charToBuff in dealerSystem.charScripts)
                    {
                        charToBuff.graceStat -= 10;
                    }
                }
                else if (beingPlayed)
                {
                    activeCharacter.attackStat += 10;
                    activeCharacter.graceStat += 10;
                    activeCharacter.healthStat += 10;
                    activeCharacter.HealCharacter(10);
                }
                else if (resetBuff)
                {
                    activeCharacter.attackStat -= 10;
                    activeCharacter.graceStat -= 10;
                    activeCharacter.healthStat -= 10;
                }
                break;
            case 12:
                if (activatePassive && !resetBuff)
                {
                    foreach (Character charToBuff in dealerSystem.charScripts)
                    {
                        charToBuff.attackStat += 10;
                    }
                }
                else if (activatePassive && resetBuff)
                {
                    foreach (Character charToBuff in dealerSystem.charScripts)
                    {
                        charToBuff.attackStat -= 10;
                    }
                }
                else if (beingPlayed)
                {
                    activeCharacter.attackStat += 10;
                    activeCharacter.graceStat += 10;
                    activeCharacter.healthStat += 10;
                    activeCharacter.HealCharacter(10);
                }
                else if (resetBuff)
                {
                    activeCharacter.attackStat -= 10;
                    activeCharacter.graceStat -= 10;
                    activeCharacter.healthStat -= 10;
                }
                break;
            case 13:
                int highestStat = (Mathf.Max(Mathf.Max(activeCharacter.attackStat, activeCharacter.healthStat), activeCharacter.graceStat));
                if (activatePassive && !resetBuff)
                {
                    activeCharacter.attackStat = highestStat + 20;
                    activeCharacter.graceStat = highestStat + 20;
                    activeCharacter.healthStat = highestStat + 20;
                    activeCharacter.HealCharacter(highestStat + 20);
                }
                else if (activatePassive && resetBuff)
                {
                    activeCharacter.attackStat = activeCharacter.baseAttack;
                    activeCharacter.graceStat = activeCharacter.baseGrace;
                    activeCharacter.healthStat = activeCharacter.baseHealth;
                }
                break;
        }
    }

}
