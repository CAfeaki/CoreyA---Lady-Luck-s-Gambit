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

    void Start()
    {
        cardNumber = this.GetComponent<SelectedInfo>().cardType;
        moveIndex = this.GetComponent<SelectedInfo>().moveType;
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
                    descText.text = "move 1";
                    break;
                case 2:
                    descText.text = "move 2";
                    break;
                case 3:
                    descText.text = "move 3";
                    break;

            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        descriptionBox.SetActive(false);

    }
}
