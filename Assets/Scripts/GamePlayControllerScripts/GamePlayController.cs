using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayController : MonoBehaviour
{
    public static GamePlayController instance;
    [HideInInspector]
    public bool gamePlaying=false;
    [SerializeField]
    private GameObject tile;
    private Vector3 currentTilePosition;
    private AudioSource audioSource;
    [SerializeField]
    private Material tileMaterial;
    [SerializeField]
    private Light DayLight;
    [SerializeField] private BallScript ball;

    private Camera mainCamera;

    private bool camColorLerp;

    private Color cameraColor;

    private Color[] tileColor_Day;
    private Color tileColor_Night;
    private int tileColor_index;

    private Color tileTrueColor;

    private float timer;
    private float timerInterval = 5f;

    private float camLerpTimer;
    private float camLerpInterval = 1f;
    public int collectedDiamondAmount = 0;

    private bool startGame = false;

    private bool isStarted = false;

    private int direction = 1;
    void Awake()
    {
        MakeSingleton();
        currentTilePosition = new Vector3(-2, 0, 2);
        audioSource = GetComponent<AudioSource>();
        mainCamera = Camera.main;
        cameraColor = mainCamera.backgroundColor;
         tileTrueColor = tileMaterial.color;
        tileColor_index = 0;
        tileColor_Day = new Color[3];
        tileColor_Day[0] = new Color(10 / 256f, 139 / 256f, 203 / 256f);
        tileColor_Day[1] = new Color(10 / 256f, 200 / 256f, 20 / 256f);
        tileColor_Day[2] = new Color(220 / 256f, 170 / 256f, 45 / 256f);
        tileColor_Night=new Color(0,8/256f,11/256f);
        tileMaterial.color = tileColor_Day[0];


    }
     void OnDisable()
    {
        instance = null;
        tileMaterial.color = tileTrueColor;
    }
     void Start()
    {
        //for (int i = 0; i < 20; i++)
        //{
        //    CreateTiles();
        //}
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && startGame) {
           
            startGame = false;
            isStarted = true;
            
            ball.StartGame();
            
            gamePlaying = false;
        }
        if (gamePlaying) {
            CheckLerpTimer();
        }
       
    }
    public void StartGame() {
        startGame = true;
    }
    void MakeSingleton() {
        if (instance == null) {
            instance = this;
        }
    }
    void CreateTiles() {
        Vector3 newTilePosition = currentTilePosition;
        int rnd = Random.Range(0, 100);

        if (rnd < 50) {
            newTilePosition.x -= 1f;
        }
        else {
            newTilePosition.z += 1f;
        }
        currentTilePosition = newTilePosition;
        Instantiate(tile, currentTilePosition, Quaternion.identity);
    }
    void CheckLerpTimer() {
        timer += Time.deltaTime;
        if(timer> timerInterval) {
            timer -= timerInterval;
            camColorLerp = true;
            camLerpTimer = 0f;
        }
        if (camColorLerp) {
            camLerpTimer += Time.deltaTime;
            float percent = camLerpTimer / camLerpInterval;
            if(direction == 1) {
                mainCamera.backgroundColor = Color.Lerp(cameraColor, Color.black,percent);
                tileMaterial.color = Color.Lerp(tileColor_Day[tileColor_index], tileColor_Night, percent);
                DayLight.intensity = 1f - percent;
            }
            else {
                mainCamera.backgroundColor = Color.Lerp(Color.black, cameraColor, percent);
                tileMaterial.color = Color.Lerp(tileColor_Night, tileColor_Day[tileColor_index], percent);
                DayLight.intensity = percent;
            }
            if(percent > 0.98f) {
                camLerpTimer = 1f;
                direction *= -1;
                camColorLerp = false;
                if (direction == -1) {
                    tileColor_index = Random.Range(0, tileColor_Day.Length);
                }
            }

        }

    }
    public void ActiveTileSpawner() {
        StartCoroutine(SpawnNewTiles());
    }
    public void PlayCollectableSound() {
        audioSource.Play();
    }
    IEnumerator SpawnNewTiles() {
        yield return new WaitForSeconds(0.3f);
        CreateTiles();
        if (gamePlaying) {
            StartCoroutine(SpawnNewTiles());
        }
    }

    public void addDiamond(int value)
    {
        collectedDiamondAmount += value;
        UIManager.Instance.SetCollectedDiamond();
    }
    public void LevelCompleted() {
       
        ball.GameFinished();
        UIManager.Instance.NextLvlUI();
        
    }
    public void FailedLvl() {
        gamePlaying = false;
        
        UIManager.Instance.RestartButtonUI();
    }
    public void CharacterReset()
    {
        
        isStarted = false;
        collectedDiamondAmount = 0;
        

        ball.transform.position = new Vector3(-0.28f, 1.83f, 0f);

        ball.GameFinished();

       
        UIManager.Instance.SetCollectedDiamond();

    }



}
