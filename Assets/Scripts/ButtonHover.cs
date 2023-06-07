using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject descriptionBox;
    public Text descText;
    public int cardNumber;
    public int moveIndex;
    public Character activeCharacter;

    void Start()
    {
        cardNumber = this.GetComponent<SelectedInfo>().cardType;
        moveIndex = this.GetComponent<SelectedInfo>().moveType;
    }

    void Update()
    {
        activeCharacter = GameObject.Find("UIManager").GetComponent<UIManager>().activeCharacter;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        descriptionBox.SetActive(true);
        if (cardNumber != 0)
        {
            switch (cardNumber)
            {
                case 1:
                    descText.text = "card 1";
                    break;
                case 2:
                    descText.text = "card 2";
                    break;
                case 3:
                    descText.text = "card 3";
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
                        descText.text = "Deals attack stat / 2 x Grace stat / 2";
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
