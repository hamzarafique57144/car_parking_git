using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CashManager 
{
    public static int defualtCash = 7500;

    public static void AddCash(int newCash)
    {
        int cash = GetSavedCash();
        cash += newCash;
        SetCashData(cash);
    }

    public static void DeductCash(int newCash)
    {
        int cash = GetSavedCash();
        if(cash >= newCash)
        {
            cash -= newCash;
            SetCashData(cash);
        }
       
    }

    public static void SetCashData(int money)
    {
        PlayerPrefs.SetInt(PLAYER_PREFS.Cash, money);
        PlayerPrefs.Save();
    }

    public static int GetSavedCash()
    {
       return PlayerPrefs.GetInt(PLAYER_PREFS.Cash);
       
    }

    public static void ClearCash()
    {
        SetCashData(0);
    }
}
