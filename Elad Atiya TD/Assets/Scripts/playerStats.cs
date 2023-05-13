using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerStats : MonoBehaviour
{
    public static int money;
    public int moneyStart = 400;

    void Start()
    {
        money = moneyStart;
    }
}
