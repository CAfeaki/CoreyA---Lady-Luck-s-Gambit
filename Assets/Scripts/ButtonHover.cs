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
    public DealerSystem dealerScript;
    public Button cardButton;
    public SelectedInfo cardScript;
    public GameObject cardDesc;
    public Text passiveDesc;
    public Text playDesc;
    private List<string> passiveTexts = new List<string>
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
        "All stats become the value of your highest stat."
    };
    private List<string> playTexts = new List<string>
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

    void Start()
    {
        cardScript = this.GetComponent<SelectedInfo>();
        cardType = this.GetComponent<SelectedInfo>().cardType;
        cardButton = gameObject.GetComponent<Button>();
        moveIndex = this.GetComponent<SelectedInfo>().moveType;
        dealerScript = GameObject.Find("DealerSystem").GetComponent<DealerSystem>();
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
            switch (cardType)
            {
                case 1:
                    if (cardButton.interactable)
                    {
                        descriptionBox.SetActive(true);
                        descText.text = "Card: " + cardScript.cardNum.ToString();
                        passiveDesc.text = passiveTexts[cardScript.cardNum - 1];
                        playDesc.text = playTexts[cardScript.cardNum - 1];
                    }
                    break;
                case 2:
                    if (cardButton.interactable)
                    {
                        descriptionBox.SetActive(true);
                        descText.text = "Card: " + cardScript.cardNum.ToString();
                        passiveDesc.text = passiveTexts[cardScript.cardNum - 1];
                        playDesc.text = playTexts[cardScript.cardNum - 1];
                    }
                    break;
                case 3:
                    if (cardButton.interactable)
                    {
                        descriptionBox.SetActive(true);
                        descText.text = "Card: " + cardScript.cardNum.ToString();
                        passiveDesc.text = passiveTexts[cardScript.cardNum - 1];
                        playDesc.text = playTexts[cardScript.cardNum - 1];
                    }
                    break;
            }
        }
        if (moveIndex != 0)
        {
            switch (moveIndex)
            {
                case 1:
                    if (activeCharacter.characterNum == 1)
                    {
                        descriptionBox.SetActive(true);
                        descText.text = "Deals attack stat";
                    }
                    if (activeCharacter.characterNum == 2)
                    {
                        descriptionBox.SetActive(true);
                        descText.text = "Deals attack stat";
                    }
                    break;
                case 2:
                    if (activeCharacter.characterNum == 1)
                    {
                        descriptionBox.SetActive(true);
                        descText.text = "Deals attack stat x 2. Takes a turn to charge";
                    }
                    if (activeCharacter.characterNum == 2)
                    {
                        descriptionBox.SetActive(true);
                        descText.text = "Restores HP equal to grace stat to an ally.";
                    }
                    break;
                case 3:
                    if (activeCharacter.characterNum == 1)
                    {
                        descriptionBox.SetActive(true);
                        descText.text = "Deals attack stat / 2 to all enemies";
                    }
                    if (activeCharacter.characterNum == 2)
                    {
                        descriptionBox.SetActive(true);
                        descText.text = "Deals attack stat and heals equal to the damage dealt";
                    }
                    break;

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
