using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Character 1 Stats")]
    public int char1Attack;
    public int char1Grace;
    public int char1Health;
    public int char1CurrHealth;

    public int char1BaseAttack;
    public int char1BaseGrace;
    public int char1BaseHealth;

    [Header("Character 2 Stats")]
    public int char2Attack;
    public int char2Grace;
    public int char2Health;
    public int char2CurrHealth;

    public int char2BaseAttack;
    public int char2BaseGrace;
    public int char2BaseHealth;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

}
