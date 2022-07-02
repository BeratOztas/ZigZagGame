using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;


public class UIManager : MonoSingleton<UIManager>
{


    [Header("UIS")]
    [SerializeField] private GameObject tapToPlayUI;
    [SerializeField] private GameObject nextLvlUI;
    [SerializeField] private GameObject restartLvlUI;
    [SerializeField] private GameObject pauseLvlUI;

    [Header("Buttons")]
    [SerializeField] private GameObject pausedText;
    [SerializeField] private Toggle soundToggle;
    [SerializeField] private Toggle vibrationToggle;

    [Header("Texts")]
    [SerializeField] private TMP_Text currentLvl;
    [SerializeField] private TMP_Text totalDiamondText;
    [SerializeField] private TMP_Text collectedDiamondText;
    [SerializeField] private TMP_Text nextLvlButtonText;
    [SerializeField] private GameObject nextLevelButton;
    [SerializeField] private Button multiplierButton;
    [SerializeField] private TMP_Text multiplierButtonText;
    [SerializeField] private TMP_Text multiplierGameText;
    [SerializeField] private Image multiplierImage;
    [SerializeField] private TMP_Text collectedLvlDiamondText;
    private int multiplier;
    private int multiplierCounter = 0;

    [Header("Status Texts")]
    [SerializeField] private Image progressBarImage;
    [SerializeField] private GameObject poorText;
    [SerializeField] private GameObject averageText;
    [SerializeField] private GameObject richText;

    [SerializeField] private RectTransform arrowImage;
    private bool clickBonusCheck = false;

    public bool isPaused;

    void Awake()
    {
        if (PlayerPrefs.GetInt("vibrationOnOff") == 0)
        {
            vibrationToggle.GetComponent<Toggle>().isOn = false;
        }
        if (PlayerPrefs.GetInt("soundOnOff") == 0)
        {
            soundToggle.GetComponent<Toggle>().isOn = false;
        }
    }
    private void Start()
    {
        isPaused = true;
        DOTween.Init();
        LevelText();
    }
    public void PlayResButton()
    {
        if (tapToPlayUI.activeSelf)
        {
            tapToPlayUI.SetActive(false);
            isPaused = false;
            GamePlayController.instance.StartGame();
            
        }
        if (nextLvlUI.activeSelf)
        {
            nextLvlUI.SetActive(false);
            isPaused = false;
            

            LevelManager.Instance.LevelUp();
            LevelText();
            GamePlayController.instance.CharacterReset();
            LevelManager.Instance.GenerateCurrentLevel();
        }
        if (restartLvlUI.activeSelf)
        {
            restartLvlUI.SetActive(false);
            isPaused = false;
            Debug.Log("REstarted");
            //PlayerPrefs.SetInt("TotalPhone", PlayerPrefs.GetInt("TotalPhone")
            //   + PlayerManagement.Instance.collectedLvlPhoneAmount);
            GamePlayController.instance.CharacterReset();
            LevelManager.Instance.GenerateCurrentLevel();
            
            SetCollectedDiamond();
        }
        if (pauseLvlUI.activeSelf)
        {
            pauseLvlUI.SetActive(false);
            isPaused = false;
            ResumeGame();
        }


    }//PlayResButton
    
    public void NextLvlUI()
    {
        if (!isPaused)
        {
            tapToPlayUI.SetActive(false);
            nextLvlUI.SetActive(true);
            isPaused = true;
            NextLvl();
            //   nextLvlButtonText.text = "" + PlayerManagement.Instance.currentLvlMoneyAmount + "$";
        }
       
    }//NextLvlUI

    public void RestartButtonUI()
    {
        if (!isPaused)
        {
            restartLvlUI.SetActive(true);
            isPaused = true;
        }
    }//restartButton
    public void PauseButtonUI()
    {
        if (!isPaused)
        {
            pauseLvlUI.SetActive(true);
            
            isPaused = true;
            PauseGame();
        }
    }//pauseButton
    public void TapToPlay()
    {
        if (!isPaused)
        {
            tapToPlayUI.SetActive(true);
            isPaused = true;
        }
    }

    public void UIVibrationToggle(bool checkOnOff)
    {
        if (checkOnOff)
        {
            vibrationToggle.GetComponent<Toggle>().isOn = true;
            PlayerPrefs.SetInt("vibrationOnOff", 1);
        }
        else
        {
            vibrationToggle.GetComponent<Toggle>().isOn = false;
            PlayerPrefs.SetInt("vibrationOnOff", 0);
        }
    }//vibrationToggle
    public void UISoundToggle(bool checkOnOff)
    {
        if (checkOnOff)
        {
            soundToggle.GetComponent<Toggle>().isOn = true;
            PlayerPrefs.SetInt("soundOnOff", 1);
        }
        else
        {
            soundToggle.GetComponent<Toggle>().isOn = false;
            PlayerPrefs.SetInt("soundOnOff", 0);
        }
    }
    void NextLvl()
    {
        
        multiplierGameText.text = "You Won";
        collectedLvlDiamondText.text = GamePlayController.instance.collectedDiamondAmount + " ";
        nextLevelButton.SetActive(true);
        PlayerPrefs.SetInt("TotalDiamond", PlayerPrefs.GetInt("TotalDiamond")
                 + GamePlayController.instance.collectedDiamondAmount);
        SetTotalDiamond();
    }


    public void LevelText()
    {
        int LevelInt = LevelManager.Instance.GetGlobalLevelIndex() + 1;
        currentLvl.text = "Level " + LevelInt;
    }

    
    public void SetCollectedDiamond()
    {
        collectedDiamondText.text = "" + GamePlayController.instance.collectedDiamondAmount;
    }
    public void SetTotalDiamond()
    {
        totalDiamondText.text = "" + PlayerPrefs.GetInt("TotalDiamond", 0) + "";
    }
    void PauseGame()
    {
        Time.timeScale = 0;
    }
    void ResumeGame()
    {
        Time.timeScale = 1;
    }


    //void MoveMultiplierArrow()
    //{
    //    arrowImage.DORotate(arrowImage.forward * 90, 0.01f);
    //    arrowImage.DORotate(arrowImage.forward * -90, 1f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    //    StartCoroutine(HandTransform());
    //}
    //IEnumerator HandTransform()
    //{
    //    float arrowAngle = arrowImage.eulerAngles.z;
    //    if (arrowAngle > 45 && arrowAngle < 90)
    //        SetMultiplier("Get 1X", 1);
    //    if (arrowAngle > 0 && arrowAngle < 45)
    //        SetMultiplier("Get 2X", 2);
    //    if (arrowAngle > 315 && arrowAngle < 360)
    //        SetMultiplier("Get 3X", 3);
    //    if (arrowAngle > 270 && arrowAngle < 315)
    //        SetMultiplier("Get 4X", 4);
    //    if (!clickBonusCheck)
    //    {
    //        yield return new WaitForFixedUpdate();
    //        StartCoroutine(HandTransform());
    //    }
    //    else
    //    {
    //        MultiplierButton(true);
    //    }
    //}

    //void SetMultiplier(string textString, int multiplierInt)
    //{
    //    multiplierButtonText.text = textString;
    //    multiplier = multiplierInt;

    //    if (multiplierInt == 1)
    //        multiplierImage.color = new Color32(241, 12, 12, 255);
    //    if (multiplierInt == 2)
    //        multiplierImage.color = new Color32(255, 153, 21, 255);
    //    if (multiplierInt == 3)
    //        multiplierImage.color = new Color32(250, 205, 51, 255);
    //    if (multiplierInt == 4)
    //        multiplierImage.color = new Color32(105, 179, 76, 255);

    //}
    //public void MultiplierButton(bool coroutineCheck)
    //{
    //    clickBonusCheck = true;
    //    StopCoroutine(HandTransform());
    //    DOTween.Kill(arrowImage.transform);
    //    multiplierButton.interactable = false;

    //    if (coroutineCheck)
    //    {
    //        multiplierGameText.text = "You Won";
    //        multiplierButtonText.text = PlayerManagement.Instance.currentLvlMoneyAmount * multiplier + "$";
    //        PlayerManagement.Instance.currentLvlMoneyAmount *= multiplier;
    //        nextLevelButton.SetActive(true);
    //        PlayerPrefs.SetInt("TotalMoney", PlayerPrefs.GetInt("TotalMoney")
    //            + PlayerManagement.Instance.currentLvlMoneyAmount);
    //        SetTotalMoney();
    //    }

    //}//multiplierbutton

    //public void ResMultiplierButton()
    //{
    //    clickBonusCheck = false;
    //    nextLevelButton.SetActive(false);
    //    multiplierGameText.text = "Tap to Win";
    //    multiplierButtonText.text = "Get 1X";
    //    multiplierButton.interactable = true;
    //}//resmultiplierButton
    public void UIQuitGame()
    {
        Application.Quit();
    }
}
