using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject descriptionBox;
    public Text descText;
    public int cardType;
    public int cardNum;
    public int moveIndex;
    public Character activeCharacter;
    public DealerSystem dealerSystem;
    public Button cardButton;
    public SelectedInfo cardScript;
    public GameObject cardDesc;
    public Text passiveDesc;
    public Text playDesc;
    public List<string> passiveTexts = new List<string>
    {
        "No passive ability.",
        "No passive ability.",
        "No passive ability.",
        "+2 to <b>Attack</b> and <b>Health</b>.",
        "+2 to <b>Grace</b> and <b>Health</b>.",
        "+2 to <b>Attack</b> and <b>Grace</b>.",
        "+3 to all stats.",
        "+4 to all stats.",
        "+5 to all stats.",
        "+10 to <b>Health</b>.",
        "+10 to <b>Grace</b>.",
        "+10 to <b>Attack</b>.",
        "All stats become the value of your highest stat + 20."
    };
    public List<string> playTexts = new List<string>
    {
        "+3 to <b>Attack</b>, <b>Grace</b> and <b>Health</b> for 2 rounds.",
        "+4 to <b>Attack</b>, <b>Grace</b> and <b>Health</b> for 2 rounds.",
        "+5 to <b>Attack</b>, <b>Grace</b> and <b>Health</b> for 2 rounds.",
        "+4 to <b>Attack</b> and <b>Health</b> for 2 rounds.",
        "+4 to <b>Grace</b> and <b>Health</b> for 2 rounds.",
        "+4 to <b>Attack</b> and <b>Grace</b> for 2 rounds.",
        "+7 to <b>Attack</b> and <b>Health</b> and +3 to <b>Grace</b> for 2 rounds.",
        "+8 to <b>Grace</b> and <b>Health</b> and +4 to <b>Attack</b> for 2 rounds.",
        "+9 to <b>Attack</b> and <b>Grace</b> and +5 to <b>Health</b> for 2 rounds.",
        "+10 to all stats for 1 round.",
        "+10 to all stats for 1 round.",
        "+10 to all stats for 1 round.",
        "No play ability."
    };
    private string cardName;
    private int charNum;

    void Start()
    {
        cardScript = this.GetComponent<SelectedInfo>();
        cardType = this.GetComponent<SelectedInfo>().cardType;
        cardButton = gameObject.GetComponent<Button>();
        moveIndex = this.GetComponent<SelectedInfo>().moveType;
        charNum = this.GetComponent<SelectedInfo>().characterNum;
        dealerSystem = GameObject.Find("DealerSystem").GetComponent<DealerSystem>();
    }

    void Update()
    {
        activeCharacter = GameObject.Find("UIManager").GetComponent<UIManager>().activeCharacter;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (cardType != 0)
        {
            cardDesc.SetActive(true);
            if (cardScript.cardNum > 10)
            {
                switch (cardScript.cardNum)
                {
                    case 11:
                        cardName = "Jack";
                        break;
                    case 12:
                        cardName = "Queen";
                        break;
                    case 13:
                        cardName = "King";
                        break;
                }
            }
            else
            {
                cardName = cardScript.cardNum.ToString();
            }
            switch (cardType)
            {
                case 1:
                    if (cardButton.interactable)
                    {
                        descriptionBox.SetActive(true);
                        descText.text = "Card: " + cardName;
                        passiveDesc.text = passiveTexts[cardScript.cardNum - 1];
                        playDesc.text = playTexts[cardScript.cardNum - 1];
                    }
                    break;
                case 2:
                    if (cardButton.interactable)
                    {
                        descriptionBox.SetActive(true);
                        descText.text = "Card: " + cardName;
                        passiveDesc.text = passiveTexts[cardScript.cardNum - 1];
                        playDesc.text = playTexts[cardScript.cardNum - 1];
                    }
                    break;
                case 3:
                    if (cardButton.interactable)
                    {
                        descriptionBox.SetActive(true);
                        descText.text = "Card: " + cardName;
                        passiveDesc.text = passiveTexts[cardScript.cardNum - 1];
                        playDesc.text = playTexts[cardScript.cardNum - 1];
                    }
                    break;
            }
        }
        else if (moveIndex != 0)
        {
            switch (moveIndex)
            {
                case 1:
                    if (activeCharacter.characterNum == 1)
                    {
                        descriptionBox.SetActive(true);
                        descText.text = "Deals <b>Attack</b> stat to enemy target.";
                    }
                    if (activeCharacter.characterNum == 2)
                    {
                        descriptionBox.SetActive(true);
                        descText.text = "Deals <b>Attack</b> stat to enemy target.";
                    }
                    break;
                case 2:
                    if (activeCharacter.characterNum == 1)
                    {
                        descriptionBox.SetActive(true);
                        descText.text = "Deals double your <b>Attack</b> to enemy target. Takes a turn to charge.";
                    }
                    if (activeCharacter.characterNum == 2)
                    {
                        descriptionBox.SetActive(true);
                        descText.text = "Restores <b>Health</b> to your ally equal to your <b>Grace</b>.";
                    }
                    break;
                case 3:
                    if (activeCharacter.characterNum == 1)
                    {
                        descriptionBox.SetActive(true);
                        descText.text = "Deals half of your <b>Attack</b> to all enemies.";
                    }
                    if (activeCharacter.characterNum == 2)
                    {
                        descriptionBox.SetActive(true);
                        descText.text = "Deals your <b>Attack</b> to enemy target and restores the amount of damage done to your <b>Health</b>.";
                    }
                    break;

            }
        }
        else if (charNum != 0)
        {
            if (cardDesc.activeSelf)
            {
                cardDesc.SetActive(false);
            }
            if (charNum == 1)
            {
                descriptionBox.SetActive(true);
                descText.text = "<b>Spark to a Flame</b>\n<size=36>For every consecutive turn Sun spends attacking an enemy, they get +1 to their <b>Attack</b> stat. This resets when Sun takes a different move.</size>";
            }
            else if (charNum == 2)
            {
                descriptionBox.SetActive(true);
                descText.text = "<b>Rises the Moon</b>\n<size=36>At the start of each turn, Moon regains +1 to their <b>Health</b>, regardless of whether they’re conscious or not.</size>";
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (cardDesc.activeSelf)
        {
            cardDesc.SetActive(false);
        }
        descriptionBox.SetActive(false);

    }
}
