using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedInfo : MonoBehaviour
{
    public int cardType;
    public int cardNum;
    public int moveType;
    public Button thisCard;

    private DealerSystem dealerScript;

    void Start()
    {
        dealerScript = GameObject.Find("DealerSystem").GetComponent<DealerSystem>();
        thisCard = gameObject.GetComponent<Button>();
    }

    public void ActivateCard(int chosenCardNum)
    {
        cardNum = chosenCardNum;
        thisCard.interactable = true;
    }
}
