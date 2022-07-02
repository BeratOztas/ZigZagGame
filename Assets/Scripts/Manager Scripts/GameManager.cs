using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int totalDiamond;
    void Start()
    {
        if (LevelManager.Instance.GetGlobalLevelIndex() == 0)
        {//new game
            totalDiamond = 0;
            PlayerPrefs.SetInt("TotalDiamond", totalDiamond);
        }
        if (PlayerPrefs.GetInt("TotalDiamond") >= 0)
        {

            SetTotalDiamond(0);
        }
        
    }
    void SetTotalDiamond(int collectedAmount)
    {
        totalDiamond = PlayerPrefs.GetInt("TotalDiamond", 0) + collectedAmount;
        PlayerPrefs.SetInt("TotalDiamond", totalDiamond);
        UIManager.Instance.SetTotalDiamond();

        totalDiamond = 0;
    }
}
