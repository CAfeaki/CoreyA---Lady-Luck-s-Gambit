using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedInfo : MonoBehaviour
{
    public int cardType;
    public int cardNum;
    public int moveType;
    public int characterNum;
    public Button thisCard;

    [Header("Initial Cards")]
    public Text cardName;
    public Text passiveText;
    public Text playText;

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

    public void DeActivateCard()
    {
        thisCard.interactable = false;
    }
}
