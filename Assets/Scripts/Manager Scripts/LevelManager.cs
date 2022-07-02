using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager :MonoSingleton<LevelManager>
{
    [SerializeField] private GameObject[] levelPrefabs;
    [SerializeField] private int levelIndex = 0;
    [SerializeField] private bool forceLevel = false;


    private int globalLevelIndex = 0;
    private bool inited = false;
    private GameObject currentLevel;

    void Start()
    {
        Init();
        GenerateCurrentLevel();


    }
    public void Init()
    {
        if (inited)
        {
            return;
        }
        inited = true;
        PlayerPrefs.DeleteAll();
        globalLevelIndex = PlayerPrefs.GetInt("Level", 0);

        if (forceLevel)
        {
            globalLevelIndex = levelIndex;
            return;
        }
        levelIndex = globalLevelIndex;

        if (levelIndex >= levelPrefabs.Length)
        {
            levelIndex = 0;
            globalLevelIndex = 0;
            PlayerPrefs.SetInt("Level", globalLevelIndex);
            //  levelIndex= GameUtility.RandomIntExcept(_levelPrefabs.Length, _levelIndex, 0);
        }


    }//init

    public void GenerateCurrentLevel()
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel);
        }
        currentLevel = Instantiate(levelPrefabs[levelIndex]);



    }

    public GameObject GetCurrentLevel()
    {
        return currentLevel;
    }

    public void LevelUp()
    {
        if (forceLevel)
        {
            return;
        }
        globalLevelIndex++;
        PlayerPrefs.SetInt("Level", globalLevelIndex);
        levelIndex = globalLevelIndex;

        if (levelIndex >= levelPrefabs.Length)
        {
            levelIndex = 0;
            globalLevelIndex = 0;
            PlayerPrefs.SetInt("Level", globalLevelIndex);
            //  levelIndex = GameUtility.RandomIntExcept(_levelPrefabs.Length, _levelIndex, 0);
        }

    }
    public int GetGlobalLevelIndex()
    {
        return globalLevelIndex;
    }
}
