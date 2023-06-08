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
            switch (cardType)
            {
                case 1:
                    if (cardButton.interactable)
                    {
                        descriptionBox.SetActive(true);
                        descText.text = cardScript.cardNum.ToString();
                    }
                    break;
                case 2:
                    if (cardButton.interactable)
                    {
                        descriptionBox.SetActive(true);
                        descText.text = cardScript.cardNum.ToString();
                    }
                    break;
                case 3:
                    if (cardButton.interactable)
                    {
                        descriptionBox.SetActive(true);
                        descText.text = cardScript.cardNum.ToString();
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
                        descText.text = "Deals attack stat";
                    }
                    if (activeCharacter.characterNum == 2)
                    {
                        descText.text = "Deals attack stat";
                    }
                    break;
                case 2:
                    if (activeCharacter.characterNum == 1)
                    {
                        descText.text = "Deals attack stat x 2. Takes a turn to charge";
                    }
                    if (activeCharacter.characterNum == 2)
                    {
                        descText.text = "Restores HP equal to grace stat to an ally.";
                    }
                    break;
                case 3:
                    if (activeCharacter.characterNum == 1)
                    {
                        descText.text = "Deals attack stat / 2 to all enemies";
                    }
                    if (activeCharacter.characterNum == 2)
                    {
                        descText.text = "Deals attack stat and heals equal to the damage dealt";
                    }
                    break;

            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        descriptionBox.SetActive(false);

    }
}
